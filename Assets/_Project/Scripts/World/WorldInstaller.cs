using System.Collections.Generic;
using _Project.Scripts.GameContext.Internal;
using _Project.Scripts.GameLauncher;
using _Project.Scripts.Other;
using _Project.Scripts.Player;
using _Project.Scripts.World.Obstacles;
using _Project.Scripts.World.SFX;
using UnityEngine;

namespace _Project.Scripts.World
{
    public class WorldInstaller : Installer
    {
        [SerializeField] private ObstaclesController obstaclesController;
        [Header("Finish")]
        [SerializeField] private Finish finish;
        [SerializeField] private float distanceToFinishDoorOpen;
        [SerializeField] private FinishRoad finishRoad;
        [Space]
        [SerializeField] private Particles particles;
        [SerializeField] private SfxController sfxController;
        
        public override IEnumerable<object> GetServices()
        {
            yield return finish;
            yield return particles;
            yield return sfxController;
        }

        public override IEnumerable<object> GetListeners()
        {
            yield return obstaclesController;
            yield return sfxController;
        }

        public override void ConstructGame(IGameLocator serviceLocator)
        {
            var player = serviceLocator.GetService<Player.Player>();
            var gameContext = serviceLocator.GetService<GameContext.GameContext>();
            
            particles.Construct();
            sfxController.Construct();
            obstaclesController.Construct(particles, sfxController);
            finishRoad.Construct(player, finish);
            finish.Construct(player.transform, gameContext, distanceToFinishDoorOpen);
        }
    }
}