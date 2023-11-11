using System;
using UnityEngine;

namespace _Project.Scripts.Player.Shooting
{
    [Serializable]
    public class ProjectileConfig
    {
        [field: SerializeField] public float Speed { get; private set; } = 2f;
        [field: SerializeField] public float MaxLifeTime { get; private set; } = 5f;
        [field: SerializeField] public float MinRadius { get; private set; } = 0.1f;
    }
}