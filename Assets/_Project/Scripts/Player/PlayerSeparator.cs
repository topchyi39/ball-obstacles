using System;
using System.Collections;
using _Project.Scripts.ObjectPooling;
using _Project.Scripts.Other;
using _Project.Scripts.Player.Shooting;
using _Project.Scripts.World.SFX;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerSeparator : MonoBehaviour
    {
        public bool IsSeparating { get; private set; }
        public float SeparatingRadius { get; private set; }
        
        [SerializeField] private float separateRadiusPerSecond;

        private Action _stopCallback;
        private Player _player;
        private SfxController _sfx;
        private CameraUtils _cameraUtils;
        private Transform _playerTransform;
        private ISfx _separatingSfx;
        private Projectile _currentSeparatedProjectile;
        private IEnumerator _separateRoutine;

        public void Construct(Player player, SfxController sfx, CameraUtils cameraUtils)
        {
            _player = player;
            _sfx = sfx;
            _cameraUtils = cameraUtils;
            
            _playerTransform = _player.transform;
        }
        
        public void SeparateProjectile(ObjectPool projectilePool, ProjectileConfig projectileConfig, Action stopCallback)
        {
            _currentSeparatedProjectile = CreateProjectile(projectilePool, projectileConfig);
            _separateRoutine = SeparateRoutine(_currentSeparatedProjectile, projectileConfig);
            IsSeparating = true;
            _stopCallback = stopCallback;
            _separatingSfx = _sfx.PlaySfx(SfxType.Separate, true);
            //_separatingSfx.SetPitch(_separatingSfx.Pitch / 2f);
            StartCoroutine(_separateRoutine);
        }

        public Projectile GetSeparateObject()
        {
            IsSeparating = false;
            StopCoroutine(_separateRoutine);
            var separatedProjectile = _currentSeparatedProjectile;
            _currentSeparatedProjectile = null;
            _stopCallback = null;
            _separatingSfx?.Stop();
            _separatingSfx = null;
            return separatedProjectile;
        }

        public Vector3 GetSeparatedObjectPosition()
        {
            return _currentSeparatedProjectile ? _currentSeparatedProjectile.transform.position : Vector3.zero;
        }

        public void StopSeparating()
        {
            if (_separateRoutine == null) return;
            
            IsSeparating = false;
            _stopCallback?.Invoke();
            _separatingSfx?.Stop();
            _separatingSfx = null;
            StopCoroutine(_separateRoutine);
        }

        public void Reset()
        {
            if (!_currentSeparatedProjectile) return;
            
            IsSeparating = false;
            _separatingSfx?.Stop();
            _currentSeparatedProjectile.Kill();
        }

        private IEnumerator SeparateRoutine(Projectile projectile, ProjectileConfig projectileConfig)
        {
            projectile.ChangeRadius(projectileConfig.MinRadius);
            
            while (projectile)
            {
                var deltaTime = Time.deltaTime;
                var deltaRadius = separateRadiusPerSecond * deltaTime;
                _player.ChangeRadius(-deltaRadius);
                projectile.ChangeRadius(deltaRadius);
                ChangeSeparatedPosition(projectile);
                SeparatingRadius = projectile.CurrentRadius;
                yield return new WaitForEndOfFrame();
            }
        }

        private void ChangeSeparatedPosition(Projectile projectile)
        {
            var position = _playerTransform.forward *
                           (_player.CurrentRadius + projectile.CurrentRadius);

            projectile.transform.position = _playerTransform.TransformPoint(position);
        }

        private Projectile CreateProjectile(ObjectPool projectilePool, ProjectileConfig projectileConfig)
        {
            var projectile = projectilePool.Get() as Projectile;

            if (projectile == null) return null;
            
            projectile.Init(projectileConfig, _cameraUtils);
            projectile.transform.position = _player.transform.position;

            return projectile;
        }
    }
}