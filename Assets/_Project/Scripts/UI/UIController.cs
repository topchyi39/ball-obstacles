using _Project.Scripts.GameContext;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Screen = _Project.Scripts.UI.Screens.Screen;

namespace _Project.Scripts.UI
{
    public class UIController : MonoBehaviour, IVictoryGameListener, IDefeatGameListener
    {
        [SerializeField] private Screen overlayScreen;
        [SerializeField] private Screen startScreen;
        [SerializeField] private Button startButton;
        
        [SerializeField] private Screen gameScreen;
        [SerializeField] private Screen victoryScreen;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Screen defeatScreen;
        [SerializeField] private Button tryAgainButton;

        private GameContext.GameContext _gameContext;

        private Screen _currentScreen;
        
        public void Construct(GameContext.GameContext gameContext)
        {
            _gameContext = gameContext;
            overlayScreen.Show();
            startScreen.Show();
            
            startButton.onClick.AddListener(() =>
            {
                _gameContext.StartGame();
                startScreen.Hide();
                gameScreen.Show();
            });
            
            playAgainButton.onClick.AddListener(Restart);
            tryAgainButton.onClick.AddListener(Restart);
        }

        public void OnVictoryGame()
        {
            victoryScreen.Show();
            _currentScreen = victoryScreen;
        }

        public void OnDefeatGame()
        {
            defeatScreen.Show();
            _currentScreen = defeatScreen;
        }

        private void Restart()
        {
            overlayScreen.Show().AppendCallback(() =>
            {
                _gameContext.RestartGame();
                _currentScreen.Hide();
                gameScreen.Show();
                _currentScreen = gameScreen;
                _gameContext.StartGame();
            });
            
        }
    }
}