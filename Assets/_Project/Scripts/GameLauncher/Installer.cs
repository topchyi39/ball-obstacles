using System.Collections.Generic;
using _Project.Scripts.GameContext;
using _Project.Scripts.GameContext.Internal;
using _Project.Scripts.Other;
using UnityEngine;

namespace _Project.Scripts.GameLauncher
{
    public abstract class Installer : MonoBehaviour, IGameServiceProvider,
        IGameListenerProvider,
        IGameConstructor
    {
        public virtual IEnumerable<object> GetServices(){ yield return null; }
        public virtual IEnumerable<object> GetListeners() { yield return null; }

        public virtual void ConstructGame(IGameLocator serviceLocator) { }
    }
}