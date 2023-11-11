using System;
using _Project.Scripts.GameContext;
using _Project.Scripts.Player.Moving;
using _Project.Scripts.Player.Shooting;
using _Project.Scripts.World;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour, IStartGameListener, IRestartGameListener
    {
        public float CurrentRadius { get; private set; }
        public PlayerSeparator Separator { get; private set; }
        public PlayerSphereVisual SphereVisual { get; private set; }

        [SerializeField] private Transform visual;
        [SerializeField] private float defaultRadius;
        [SerializeField] private float minRadius;

        private ShootController _shootController;
        private MoveController _moveController;
        private Finish _finish;

        private Vector3 _startPosition;
        private bool _lastTry;
        
        private GameContext.GameContext _gameContext;

        public void Construct(ShootController shootController, MoveController moveController,
            PlayerSeparator separator, Finish finish, GameContext.GameContext gameContext, Vector3 startPosition)
        {
            CurrentRadius = defaultRadius;
            
            _shootController = shootController;
            _moveController = moveController;
            _gameContext = gameContext;
            _finish = finish;
            Separator = separator;
            SphereVisual = new PlayerSphereVisual(visual, defaultRadius, minRadius, defaultRadius);
            _startPosition = startPosition;
            _shootController.TargetDestroyed = OnTargetDestroyed;
            transform.position = _startPosition;
        }

        public void ChangeRadius(float radiusDelta)
        {
            CurrentRadius = SphereVisual.ChangeRadius(radiusDelta);

            if (!(CurrentRadius <= minRadius)) return;
            
            Separator.StopSeparating();
            _shootController.Disable();
            _lastTry = true;
        }

        public void OnStartGame()
        {
            _lastTry = false;
            transform.position = _startPosition;
            _shootController.Enable();
        }

        public void OnRestartGame()
        {
            Separator.Reset();
            ChangeRadius(defaultRadius);
        }
        
        private void Update()
        {
            SphereVisual.Update(Time.deltaTime);
        }

        private void OnTargetDestroyed()
        {
            _shootController.Disable();
            
            if (!_moveController.TryMoveToFinish(CurrentRadius))
            {
                if (_lastTry)
                {
                    _gameContext.FinishGame(GameResult.Defeat);
                    return;
                }
                

                _shootController.Enable();
            }
            
            _finish.WaitForPlayer();
        }
    }
}
