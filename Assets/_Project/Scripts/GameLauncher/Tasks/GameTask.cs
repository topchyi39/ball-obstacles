using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.GameLauncher.Tasks
{
    public abstract class GameTask : ScriptableObject
    {
        public abstract Task Run();
    }
}