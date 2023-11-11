using System;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerSphereVisual : SphereVisual, IPlayerRadiusUpdater
    {
        public PlayerSphereVisual(Transform target, float defaultRadius, float minRadius, float maxRadius)
            : base(target, null, defaultRadius, minRadius, maxRadius) { }

        public float CriticalValue => _minRadius;
        public float DefaultValue => _defaultRadius;
        public event Action<float> ValueChanged;

        public override float ChangeRadius(float radiusDelta)
        {
            var newRadius = base.ChangeRadius(radiusDelta);
            ValueChanged?.Invoke(newRadius);
            return newRadius;
        }
    }
}