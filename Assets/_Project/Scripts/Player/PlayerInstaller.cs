using System.Collections.Generic;
using _Project.Scripts.GameContext.Internal;
using _Project.Scripts.GameLauncher;
using _Project.Scripts.Input;
using _Project.Scripts.Other;
using _Project.Scripts.Player.Moving;
using _Project.Scripts.Player.Shooting;
using _Project.Scripts.World;
using _Project.Scripts.World.SFX;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public sealed class PlayerInstaller : Installer
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CameraUtils cameraUtils;
        [SerializeField] private Player player;
        [SerializeField] private PlayerSeparator separator;
        [SerializeField] private Transform playerPoint;
        [SerializeField] private ShootDirectionRender shootDirectionRender;
        
        [SerializeField] private ShootConfigs shootConfigs;
        
        private ShootController _shootController;
        private MoveController _moveController;
        

        public override IEnumerable<object> GetServices()
        {
            yield return player;
        }

        public override IEnumerable<object> GetListeners()
        {
            yield return player;
        }

        public override void ConstructGame(IGameLocator serviceLocator)
        {
            var gameContext = serviceLocator.GetService<GameContext.GameContext>();
            var input = serviceLocator.GetService<PlayerTapInput>();
            var finish = serviceLocator.GetService<Finish>();
            var sfx = serviceLocator.GetService<SfxController>();
            
            _shootController = new ShootController(input, player, mainCamera, shootDirectionRender, sfx, shootConfigs);
            _moveController = new MoveController(player);
            
            player.Construct(_shootController, _moveController, separator, finish, gameContext, playerPoint.position);
            separator.Construct(player, sfx, cameraUtils);
            gameContext.AddListener(_shootController);
        }
    }
}