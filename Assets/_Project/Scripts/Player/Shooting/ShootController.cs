using System;
using System.Collections;
using _Project.Scripts.GameContext;
using _Project.Scripts.Input;
using _Project.Scripts.ObjectPooling;
using _Project.Scripts.World.SFX;
using UnityEngine;

namespace _Project.Scripts.Player.Shooting
{
    [Serializable]
    public class ShootController : IUpdateGameListener
    {
        public Action TargetDestroyed;
        
        private bool _canFire = true;
        private bool _prepared;

        private readonly ShootDirectionRender _shootDirectionRender;
        private readonly ShootConfigs _configs;
        private readonly Camera _mainCamera;
        private readonly Player _player;
        private readonly PlayerTapInput _input;
        private readonly SfxController _sfx;
        private readonly ObjectPool _projectilePool;
        
        
        public ShootController(PlayerTapInput input, Player player, Camera camera,
            ShootDirectionRender shootDirectionRender, SfxController sfx, ShootConfigs configs)
        {
            _configs = configs;

            _projectilePool = new ObjectPool(_configs.ProjectilePrefab, true, _configs.DefaultPoolCapacity,
                _configs.MaxPoolSize);
            
            _input = input;
            _player = player;
            _mainCamera = camera;
            _shootDirectionRender = shootDirectionRender;
            _sfx = sfx;
            _input.TouchStarted += PrepareForFire;
            _input.TouchCanceled += Fire;
        }

        public void Enable()
        {
            _canFire = true;
        }
        
        public void Disable()
        {
            _canFire = false;
            _prepared = false;        
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (!_prepared || !_player.Separator.IsSeparating) return;

            var separatedObjectPosition = _player.Separator.GetSeparatedObjectPosition();
            _shootDirectionRender.UpdateDirection(separatedObjectPosition, (GetTargetPoint() - separatedObjectPosition).normalized, _player.Separator.SeparatingRadius);
        }
        
        private void PrepareForFire()
        {
            if (!_canFire) return;
            
            _player.Separator.SeparateProjectile(_projectilePool, _configs.ProjectileConfig, Fire);
            _prepared = true;
        }

        private void Fire()
        {
            if (!_prepared) return;
            
            _sfx.PlaySfx(SfxType.Fire).SetVolume(1.5f);
            _shootDirectionRender.Hide();
            var projectile = _player.Separator.GetSeparateObject();
            projectile.Fire(GetTargetPoint(), TargetDestroyed);
            _prepared = false;
            _canFire = false;
        }
        
        private Vector3 GetTargetPoint()
        {
            var screenPosition = _input.ScreenPosition;
            
            var plane = new Plane(Vector3.up, _player.transform.position);
            var ray = _mainCamera.ScreenPointToRay(screenPosition);
            plane.Raycast(ray, out var enter);
            
            return ray.GetPoint(enter);
        }
    }
}