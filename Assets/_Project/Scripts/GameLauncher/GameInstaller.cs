using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameLauncher
{
    public class GameInstaller : Installer
    {
        [SerializeField] private GameContext.GameContext gameContext;

        public override IEnumerable<object> GetServices()
        {
            yield return gameContext;
        }
    }
}