using System;
using _Project.Scripts.World;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class FinishRoad : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        
        private Player _player;
        private Finish _finish;
        
        private readonly Vector3 Offset = new Vector3(0, 0.01f, 0);
        
        public void Construct(Player player, Finish finish)
        {
            _player = player;
            _finish = finish;

            var playerTransform = _player.transform;
            lineRenderer.SetPositions(new [] {
                playerTransform.position + Offset - playerTransform.forward * 5f, 
                finish.FinishPosition + Offset});
        }

        private void Update()
        {
            lineRenderer.widthMultiplier = _player.CurrentRadius * 2;
        }
    }
}