using UnityEngine;

namespace _Project.Scripts.Player.Moving
{
    public interface IFinishTarget
    {
        Vector3 FinishPosition { get; }
    }
}