using System.Collections.Generic;
using _Project.Scripts.GameContext;
using _Project.Scripts.World.SFX;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.World.Obstacles
{
    public class ObstaclesController : MonoBehaviour, IStartGameListener, IFinishGameListener
    {
        public Collider[] ReusableColliders { get; set; }

        [SerializeField] private Vector3 spawnBounds;
        [SerializeField] private float step = 1.5f;
        [SerializeField] private Obstacle obstaclePrefab;
        
        private List<Obstacle> _obstacles;
        private Particles _particles;
        private SfxController _sfx;
        private bool _needSpawnOnStart;
        
        public void Construct(Particles particles, SfxController sfx)
        {
            _particles = particles;
            _sfx = sfx;
            SpawnObstacles();
            ReusableColliders = new Collider[_obstacles.Count];
        }

        public void OnStartGame()
        {
            if (!_needSpawnOnStart) return;
            
            DestroyAllObstacles();
            SpawnObstacles();
        }
        

        public void OnFinishGame()
        {
            _needSpawnOnStart = true;
        }

        private void SpawnObstacles()
        {
            _obstacles = new List<Obstacle>(100);
            var bounds = spawnBounds * 0.5f;
            for (var x = -bounds.x; x < bounds.x; x+=step)
            {
                for (var z = -bounds.z; z < bounds.z; z+=step)
                {
                    var obstacle = CreateObstacle(x, z);
                    _obstacles.Add(obstacle);
                }
            }
        }

        private Obstacle CreateObstacle(float x, float z)
        {
            var randomPosition = Random.insideUnitSphere;
            
            var position = new Vector3(x, 0, z);
            position += randomPosition;
            position.y = 0;
            
            var randomEuler = new Vector3(0, Random.Range(0, 360), 0);
            
            var obstacle = Instantiate(obstaclePrefab, position, Quaternion.Euler(randomEuler), transform);
            obstacle.Init(this, _particles, _sfx);
            obstacle.Destroyed += OnObstacleDestroyed;
            return obstacle;
        }

        private void DestroyAllObstacles()
        {
            foreach (var obstacle in _obstacles)
            {
                Destroy(obstacle.gameObject);
            }
            
            _obstacles.Clear();
        }

        private void OnObstacleDestroyed()
        {
            _needSpawnOnStart = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, spawnBounds);
        }
    }
}