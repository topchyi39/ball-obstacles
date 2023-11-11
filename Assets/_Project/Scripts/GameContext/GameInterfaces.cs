using System.Collections.Generic;
using _Project.Scripts.GameContext.Internal;

namespace _Project.Scripts.GameContext
{
    public interface IStartGameListener
    {
        void OnStartGame();
    }

    public interface IPauseGameListener
    {
        void OnPauseGame();
    }

    public interface IResumeGameListener
    {
        void OnResumeGame();
    }

    public interface IUpdateGameListener
    {
        void OnUpdate(float deltaTime);
    }
    
    public interface IFinishGameListener
    {
        void OnFinishGame();
    }

    public interface IVictoryGameListener
    {
        void OnVictoryGame();
    }

    public interface IDefeatGameListener
    {
        void OnDefeatGame();
    }

    public interface IRestartGameListener
    {
        void OnRestartGame();
    }

    public interface IGameServiceProvider
    {
        IEnumerable<object> GetServices();
    }

    public interface IGameListenerProvider
    {
        IEnumerable<object> GetListeners();
    }

    public interface IGameConstructor
    {
        void ConstructGame(IGameLocator serviceLocator);
    }
}