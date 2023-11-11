using UnityEngine;

namespace _Project.Scripts.Other
{
    public class CameraUtils : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        public const float Threshold = 0.1f;
        
        public bool ObjectOutCameraBounds(Vector3 objectPosition)
        {
            var viewportPoint = mainCamera.WorldToViewportPoint(objectPosition);
            return viewportPoint.x - Threshold > 1 
                   || viewportPoint.x + Threshold < 0
                   || viewportPoint.y - Threshold > 1 
                   || viewportPoint.y + Threshold < 0;
        }
    }
}