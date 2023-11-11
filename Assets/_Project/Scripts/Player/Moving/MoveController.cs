using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player.Moving
{
    public class MoveController
    {
        private readonly Player _player;

        public MoveController(Player player)
        {
            _player = player;
        }

        public bool TryMoveToFinish(float radius)
        {
            var playerTransform = _player.transform;
            var result = Physics.SphereCast(playerTransform.position, radius, playerTransform.forward,
                out var hitInfo);
            
            if (!result || !hitInfo.collider.TryGetComponent(out IFinishTarget finishTarget)) return false;
            
            MoveTo(finishTarget.FinishPosition);
            return true;
        }

        private void MoveTo(Vector3 target)
        {
            _player.transform.DOJump(target, 2f, 5, 5f).SetEase(Ease.Linear);
        }
    }
}