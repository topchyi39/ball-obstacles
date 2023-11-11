using System.Collections;
using _Project.Scripts.GameContext;
using _Project.Scripts.Player.Moving;
using UnityEngine;

namespace _Project.Scripts.World
{
    public class Finish : MonoBehaviour, IFinishTarget
    {
        public Vector3 FinishPosition => transform.position + new Vector3(0, 0, 2f);

        [SerializeField] private Animator doorAnimator;

        private Transform _playerTransform;
        private GameContext.GameContext _gameContext;
        private float _distanceToDoorOpen;

        private IEnumerator _waitForPlayerRoutine;

        private const float DistanceThreshold = 0.01f;

        public void Construct(Transform playerTransform, GameContext.GameContext gameContext, float distanceToDoorOpen)
        {
            _playerTransform = playerTransform;
            _gameContext = gameContext;
            _distanceToDoorOpen = distanceToDoorOpen;
        }
        
        public void WaitForPlayer()
        {
            _waitForPlayerRoutine = WaitForPlayerRoutine();
            StartCoroutine(_waitForPlayerRoutine);
        }

        private IEnumerator WaitForPlayerRoutine()
        {
            while (Vector3.Distance(_playerTransform.position, transform.position) > _distanceToDoorOpen)
            {
                yield return new WaitForEndOfFrame();
            }
            
            doorAnimator.SetTrigger("Open");
            doorAnimator.SetTrigger("Close");
            
            while (Vector3.Distance(_playerTransform.position, transform.position) < DistanceThreshold)
            {
                yield return new WaitForEndOfFrame();
            }
            
            _gameContext.FinishGame(GameResult.Victory);
        }

        public void OnFinishGame(GameResult gameResult)
        {
            StopCoroutine(_waitForPlayerRoutine);
        }
    }
}