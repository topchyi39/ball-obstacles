using System.Collections.Generic;
using _Project.Scripts.GameContext;
using _Project.Scripts.GameContext.Internal;
using _Project.Scripts.GameLauncher;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Input
{
    public sealed class InputInstaller : Installer
    {
        private readonly PlayerTapInput _input = new();

        public override IEnumerable<object> GetServices()
        {
            yield return _input;
        }

        public override IEnumerable<object> GetListeners()
        {
            yield return _input;
        }

        public override void ConstructGame(IGameLocator serviceLocator)
        {
            _input.Construct();
        }
    }
}