using _Project.Scripts.GameContext;
using _Project.Scripts.World.SFX;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI.Screens
{
    public class GameResultScreen : Screen
    {
        [SerializeField] private GameResult resultType;
        
        private SfxController _sfx;
        
        public void Construct(SfxController sfx)
        {
            _sfx = sfx;
        }

        protected override Sequence ShowCallback(Sequence showSequence)
        {
            base.ShowCallback(showSequence);
            _sfx.PlaySfx(resultType == GameResult.Victory ? SfxType.Victory : SfxType.Defeat );
            return showSequence;
        }
    }
}