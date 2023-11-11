using System.Threading.Tasks;
using _Project.Scripts.GameContext;
using _Project.Scripts.Other;
using UnityEngine;

namespace _Project.Scripts.GameLauncher.Tasks
{
    [CreateAssetMenu(fileName = "Task «Construct Game»", menuName = "GameTasks/Task «Construct Game»")]
    public sealed class ConstructGame : GameTask
    {
        public override Task Run()
        {
            var gameContext = GameObject
                .FindGameObjectWithTag(TagManager.GameContext)
                .GetComponent<GameContext.GameContext>();
            
            var installers = GameObject
                .FindGameObjectsWithTag(TagManager.GameInstaller);   
            
            foreach (var installer in installers)
            {
                if (installer.TryGetComponent(out IGameServiceProvider serviceProvider))
                {
                    gameContext.AddServices(serviceProvider.GetServices());
                }

                if (installer.TryGetComponent(out IGameListenerProvider listenerProvider))
                {
                    gameContext.AddListeners(listenerProvider.GetListeners());
                }
            }
            
            foreach (var installer in installers)
            {
                if (installer.TryGetComponent(out IGameConstructor constructor))
                {
                    constructor.ConstructGame(gameContext);
                }
            }
            
            return Task.CompletedTask;
        }
    }
}