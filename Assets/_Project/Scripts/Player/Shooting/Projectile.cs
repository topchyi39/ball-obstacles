using System;
using _Project.Scripts.ObjectPooling;
using _Project.Scripts.Other;
using UnityEngine;

namespace _Project.Scripts.Player.Shooting
{
    public class Projectile : PooledObject
    {
        [SerializeField] private Transform visual;
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private SphereCollider sphereCollider;
        
        private ProjectileConfig _config;
        private SphereVisual _sphereVisual;
        private CameraUtils _cameraUtils;
        
        private Vector3 _direction;
        private Action _destroyCallback;
        
        private float _lifeTime;
        private bool _launched;

        public bool IsMaxRadius => _sphereVisual.IsMax;
        public float CurrentRadius { get; private set; }
        

        private void Update()
        {
            _sphereVisual?.Update(Time.deltaTime);
            if (_cameraUtils.ObjectOutCameraBounds(transform.position))
            {
                _destroyCallback?.Invoke();
                Kill();
            }
            LifeTime();
        }

        private void LifeTime()
        {
            if (!_launched) return;
            
            _lifeTime += Time.deltaTime;

            if (_lifeTime > _config.MaxLifeTime)
            {
                _destroyCallback?.Invoke();
                Kill();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITarget target))
            {
                target.Destroyed += _destroyCallback;
                target.Hit(CurrentRadius * 5f);
                Kill();
            }
            else
            {
                var callback = _destroyCallback;
                Kill();
                callback?.Invoke();
            }
        }

        public void Init(ProjectileConfig config, CameraUtils cameraUtils)
        {
            _config = config;
            _cameraUtils = cameraUtils;
            _sphereVisual = new SphereVisual(visual, sphereCollider, config.MinRadius, config.MinRadius, float.MaxValue);
        }

        public void ChangeRadius(float delta)
        {
            CurrentRadius = _sphereVisual.ChangeRadius(delta);
        }
        
        public void Fire(Vector3 targetPoint, Action destroyCallback)
        {
            _destroyCallback = destroyCallback;
            _direction = (targetPoint - transform.position).normalized;
            _lifeTime = 0f;
            _launched = true;
        }

        public void Kill()
        {
            _launched = false;
            _destroyCallback = null;
            ReturnToPool();
        }

        public override void TakeFromPool()
        {
            _launched = false;
            _destroyCallback = null;
        }

        private void Move()
        {
            if (!_launched) return;

            transform.position += _direction * (_config.Speed * Time.fixedDeltaTime);
        }
    }
}