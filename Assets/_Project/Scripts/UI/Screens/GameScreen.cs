using UnityEngine;

namespace _Project.Scripts.UI.Screens
{
    public class GameScreen : Screen
    {
        [SerializeField] private PlayerRadiusPercentage radiusPercentage;
        
        public void Construct(Player.Player player)
        {
            radiusPercentage.Construct(player.SphereVisual);
        }
    }
}