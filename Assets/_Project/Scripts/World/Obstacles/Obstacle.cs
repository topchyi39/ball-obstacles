using System;
using System.Collections;
using _Project.Scripts.Player.Shooting;
using _Project.Scripts.World.SFX;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.World.Obstacles
{
    public class Obstacle : MonoBehaviour, ITarget
    {
        public event Action Destroyed;
        
        [SerializeField] private AnimationCurve destroyCurve;
        
        [SerializeField] private MeshRenderer mainRenderer;
        [SerializeField] private Material infectedMaterial;
        [SerializeField] private Collider collider;
        
        [SerializeField] private float destructionDelay = 0.5f;
        
        private ObstaclesController _obstaclesController;
        private Particles _particles;
        private SfxController _sfx;
        private bool _infected;
        private float _infectedRadius;

        private readonly string EmissionKeyWord = "_EMISSION";
        
        public void Init(ObstaclesController obstaclesController, Particles particles, SfxController sfx)
        {
            _obstaclesController = obstaclesController;
            _particles = particles;
            _sfx = sfx;
        }
        
        public void Hit(float radius)
        {
            _infected = true;
            _infectedRadius = radius;
            Infect();
            InfectOthers();
        }
        
        private void Infect()
        {
            collider.enabled = false;
            
            mainRenderer.material = infectedMaterial;
            
            StartCoroutine(DelayedDestruction());
        }

        private IEnumerator DelayedDestruction()
        {
            DestroyEffect();

            yield return new WaitForSeconds(destructionDelay);
            Destroyed?.Invoke();
            
            gameObject.SetActive(false);
        }

        private void InfectOthers()
        {
            var otherColliders = _obstaclesController.ReusableColliders;
            var size = Physics.OverlapSphereNonAlloc(transform.position, _infectedRadius, otherColliders);
            
            for (var i = 0; i < size; i++)
            {
                if (!otherColliders[i].TryGetComponent(out Obstacle otherObstacle)) continue;
                
                if (otherObstacle != this) otherObstacle.Infect();
            }
        }

        private void DestroyEffect()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(transform.localScale * Random.Range(0.2f, 0.6f), destructionDelay).SetEase(destroyCurve));
            sequence.AppendCallback(() => _particles.PlayDestroyParticle(transform.position));

            if (!_infected) return;
            
            var sfx = _sfx.PlaySfx(SfxType.Blinking, true);
            sequence.AppendCallback(() => sfx.Stop());
            sequence.AppendCallback(() => _sfx.PlaySfx(SfxType.Destroy));
            var emissionSequence = DOTween.Sequence();
            emissionSequence.AppendCallback(() => mainRenderer.sharedMaterial.EnableKeyword(EmissionKeyWord));
            emissionSequence.AppendInterval(destructionDelay / 4f);
            emissionSequence.AppendCallback(() => mainRenderer.sharedMaterial.DisableKeyword(EmissionKeyWord));
            emissionSequence.AppendInterval(destructionDelay / 4f);
            emissionSequence.SetLoops(2);
        }

        private void OnDrawGizmos()
        {
            if (!_infected) return;
            
            Gizmos.DrawWireSphere(transform.position, _infectedRadius);
        }
    }
}