using System;
using _Project.Scripts.ObjectPooling;
using UnityEngine;

namespace _Project.Scripts.Player.Shooting
{
    [CreateAssetMenu(fileName = "ShootConfigs", menuName = "Project/Shoot/Configs")]
    public class ShootConfigs : ScriptableObject
    {
        [field: SerializeField] public PooledObject ProjectilePrefab { get; private set; }
        [field: SerializeField] public int DefaultPoolCapacity { get; private set; }
        [field: SerializeField] public int MaxPoolSize { get; private set; }
        [field: SerializeField] public ProjectileConfig ProjectileConfig { get; private set; }
    }
}