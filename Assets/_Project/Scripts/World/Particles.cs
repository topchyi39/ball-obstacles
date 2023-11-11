using _Project.Scripts.ObjectPooling;
using UnityEngine;

namespace _Project.Scripts.World
{
    public class Particles : MonoBehaviour
    {
        [SerializeField] private PooledParticles destroyParticlesPrefab;
        
        private ObjectPool _destroyParticlesPool;

        public void Construct()
        {
            _destroyParticlesPool = new ObjectPool(destroyParticlesPrefab, true, 10, 20);
        }

        public void PlayDestroyParticle(Vector3 position)
        {
            var particles = _destroyParticlesPool.Get() as PooledParticles;
            if (particles != null) particles.Play(position);
        }
    }
}