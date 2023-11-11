using System.Collections.Generic;
using System.Threading.Tasks;
using _Project.Scripts.GameLauncher.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameLauncher
{
    public sealed class GameLauncher : MonoBehaviour
    {
        [SerializeField] private bool autoRun = true;
        [SerializeField] private List<GameTask> taskList;

        private async void Start()
        {
            if (autoRun)
            {
                await LaunchGame();
            }
        }

        [ContextMenu("Launch Game")]
        public async Task LaunchGame()
        {
            foreach (var task in taskList)
            {
                await task.Run();
            }
        }
    }
}