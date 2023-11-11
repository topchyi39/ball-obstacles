using System.Threading.Tasks;
using _Project.Scripts.Other;
using UnityEngine;

namespace _Project.Scripts.GameLauncher.Tasks
{
    [CreateAssetMenu(
        fileName = "Task «Start Game»",
        menuName = "GameTasks/Task «Start Game»"
    )]
    public sealed class StartGame : GameTask
    {
        public override Task Run()
        {
            GameObject
                .FindGameObjectWithTag(TagManager.GameContext)
                .GetComponent<GameContext.GameContext>()
                .StartGame();

            return Task.CompletedTask;
        }
    }
}