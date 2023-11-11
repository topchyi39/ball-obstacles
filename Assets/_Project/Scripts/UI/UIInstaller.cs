using System.Collections.Generic;
using _Project.Scripts.GameContext.Internal;
using _Project.Scripts.GameLauncher;
using _Project.Scripts.Input;
using _Project.Scripts.UI.Screens;
using _Project.Scripts.World.SFX;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class UIInstaller : Installer
    {
        [SerializeField] private UIController uiController;
        [SerializeField] private GameScreen gameScreen;
        [SerializeField] private GameResultScreen[] resultScreens;
        [SerializeField] private UIInteraction interaction;
        
        
        public override IEnumerable<object> GetListeners()
        {
            yield return uiController;
        }

        public override void ConstructGame(IGameLocator serviceLocator)
        {
            var gameContext = serviceLocator.GetService<GameContext.GameContext>();
            var player = serviceLocator.GetService<Player.Player>();
            var sfx = serviceLocator.GetService<SfxController>();
            var input = serviceLocator.GetService<PlayerTapInput>();
            
            uiController.Construct(gameContext);
            gameScreen.Construct(player);
            interaction.Construct(sfx);
            
            foreach (var resultScreen in resultScreens)
            {
                resultScreen.Construct(sfx);
            }
        }
    }
}