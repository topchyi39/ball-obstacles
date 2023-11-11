using UnityEngine;

namespace _Project.Scripts.Player
{
    public class SphereVisual
    {
        public bool IsMax => Mathf.Abs(_currentRadius - _maxRadius) < 0.01f;

        private readonly Transform _transform;
        private readonly SphereCollider _collider;
        private readonly float _maxRadius;
        protected readonly float _minRadius;
        protected readonly float _defaultRadius;
        
        private float _currentRadius;

        private bool _isDirty = true;
        
        public SphereVisual(Transform target, SphereCollider sphereCollider, float defaultRadius, float minRadius,
            float maxRadius)
        {
            _transform = target;
            _collider = sphereCollider;
            
            _minRadius = minRadius;
            _maxRadius = maxRadius;
            
            _defaultRadius = defaultRadius;
            _currentRadius = defaultRadius;
            
            UpdateVisual();
        }


        public void Update(float deltaTime)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (!_isDirty) return;
            
            var center = new Vector3(0, _currentRadius, 0);
            _transform.localScale = Vector3.one * (_currentRadius * 2);
            _transform.localPosition = center;
            _isDirty = false;

            if (!_collider) return;
            
            _collider.radius = _currentRadius;
            _collider.center = center;
        }

        public virtual float ChangeRadius(float radiusDelta)
        {
            _currentRadius += radiusDelta;
            _currentRadius = Mathf.Clamp(_currentRadius, _minRadius, _maxRadius);
            
            _isDirty = true;

            return _currentRadius;
        }
    }
}