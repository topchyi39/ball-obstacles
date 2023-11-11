using System;
using UnityEngine;

namespace _Project.Scripts.Player.Shooting
{
    [Serializable]
    public class ShootDirectionRender
    {
        [SerializeField] private LineRenderer lineRenderer;

        private readonly Vector3 _offset = new (0, 0.01f, 0);
        
        public void UpdateDirection(Vector3 startPosition, Vector3 direction, float separatorSeparatingRadius)
        {
            if (!lineRenderer.enabled) lineRenderer.enabled = true;
            
            direction.y = 0;
            startPosition += _offset;
            var endPosition = startPosition + direction * 10f;
            lineRenderer.SetPositions(new[] { startPosition, endPosition });
            lineRenderer.widthMultiplier = separatorSeparatingRadius;
        }

        public void Hide()
        {
            lineRenderer.enabled = false;
        }
    }
}