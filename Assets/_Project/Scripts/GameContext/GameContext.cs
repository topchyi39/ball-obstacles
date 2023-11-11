using System.Collections.Generic;
using _Project.Scripts.GameContext.Internal;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Project.Scripts.GameContext
{
    public enum GameResult
    {
        Defeat,
        Victory
    }
    
    public sealed class GameContext : MonoBehaviour, IGameLocator, IGameMachine
    {
        private readonly GameMachine _gameMachine = new();
        private readonly GameLocator _serviceLocator = new();

        private readonly List<IUpdateGameListener> _updateListeners = new();

        public GameState GameState => _gameMachine.GameState;

        private void Awake()
        {
            enabled = false;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = _updateListeners.Count; i < count; i++)
            {
                var listener = _updateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        public void StartGame()
        {
            _gameMachine.StartGame();
            enabled = true;
        }

        public void PauseGame()
        {
            _gameMachine.PauseGame();
            enabled = false;
        }

        public void ResumeGame()
        {
            _gameMachine.ResumeGame();
            enabled = true;
        }

        public void FinishGame(GameResult result)
        {
            _gameMachine.FinishGame(result);
            enabled = false;
        }

        public void RestartGame()
        {
            _gameMachine.RestartGame();
            enabled = true;
        }

        public void AddListener(object listener)
        {
            _gameMachine.AddListener(listener);

            if (listener is IUpdateGameListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }
        }

        public void RemoveListener(object listener)
        {
            _gameMachine.RemoveListener(listener);

            if (listener is IUpdateGameListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }
        }

        public void AddListeners(IEnumerable<object> listeners)
        {
            foreach (var listener in listeners)
            {
                AddListener(listener);
            }
        }

        public void AddService(object service)
        {
            _serviceLocator.AddService(service);
        }

        public void AddServices(IEnumerable<object> services)
        {
            _serviceLocator.AddServices(services);
        }

        public void RemoveService(object service)
        {
            _serviceLocator.RemoveService(service);
        }

        public T GetService<T>()
        {
            return _serviceLocator.GetService<T>();
        }
    }
}