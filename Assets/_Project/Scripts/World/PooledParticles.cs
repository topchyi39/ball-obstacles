using System.Collections;
using _Project.Scripts.ObjectPooling;
using UnityEngine;

namespace _Project.Scripts.World
{
    public class PooledParticles : PooledObject
    {
        [SerializeField] private ParticleSystem particleSystem;

        public void Play(Vector3 playPosition)
        {
            particleSystem.transform.position = playPosition;
            particleSystem.Play();
            StartCoroutine(PlayRoutine());
        }

        private IEnumerator PlayRoutine()
        {
            while (particleSystem.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }
            
            ReturnToPool();
        }
    }
}