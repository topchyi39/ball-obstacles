using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameContext.Internal
{
    public enum GameState
    {
        OFF = 0,
        PLAY = 1,
        PAUSE = 2,
        FINISH = 3,
    }

    public interface IGameMachine
    {
        GameState GameState { get; }

        void StartGame();

        void PauseGame();

        void ResumeGame();

        void FinishGame(GameResult gameResult);
        void RestartGame();

        void AddListener(object listener);

        void AddListeners(IEnumerable<object> listeners);

        void RemoveListener(object listener);
    }

    internal sealed class GameMachine : IGameMachine
    {
        public GameState GameState => _gameState;

        private readonly List<object> _listeners = new();

        private GameState _gameState = GameState.OFF;

        public void StartGame()
        {
            if (_gameState != GameState.OFF)
            {
                Debug.LogWarning($"You can start game only from {GameState.OFF} state!");
                return;
            }

            _gameState = GameState.PLAY;

            foreach (var listener in _listeners)
            {
                if (listener is IStartGameListener startListener)
                {
                    startListener.OnStartGame();
                }
            }
        }

        public void PauseGame()
        {
            if (_gameState != GameState.PLAY)
            {
                Debug.LogWarning($"You can pause game only from {GameState.PLAY} state!");
                return;
            }

            _gameState = GameState.PAUSE;

            foreach (var listener in _listeners)
            {
                if (listener is IPauseGameListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
        }

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            if (_gameState != GameState.PAUSE)
            {
                Debug.LogWarning($"You can resume game only from {GameState.PAUSE} state!");
                return;
            }

            _gameState = GameState.PLAY;

            foreach (var listener in _listeners)
            {
                if (listener is IResumeGameListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
        }

        [ContextMenu("Finish Game")]
        public void FinishGame(GameResult gameResult)
        {
            if (_gameState != GameState.PLAY)
            {
                Debug.LogWarning($"You can finish game only from {GameState.PLAY} state!");
                return;
            }

            _gameState = GameState.FINISH;

            foreach (var listener in _listeners)
            {
                if (listener is IFinishGameListener finishListener) finishListener.OnFinishGame();
                if (gameResult == GameResult.Victory && listener is IVictoryGameListener victoryListener) victoryListener.OnVictoryGame();
                if (gameResult == GameResult.Defeat && listener is IDefeatGameListener defeatListener) defeatListener.OnDefeatGame();
            }
        }

        public void RestartGame()
        {
            if (_gameState != GameState.FINISH)
            {
                Debug.LogWarning($"You can Restart game only from {GameState.FINISH} state!");
                return;
            }

            _gameState = GameState.OFF;
            
            foreach (var listener in _listeners)
            {
                if (listener is IRestartGameListener restartListener) restartListener.OnRestartGame();
            }
        }
        
        public void AddListeners(IEnumerable<object> listeners)
        {
            _listeners.AddRange(listeners);
        }

        public void AddListener(object listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(object listener)
        {
            _listeners.Remove(listener);
        }
    }
}