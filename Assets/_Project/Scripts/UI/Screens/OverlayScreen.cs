using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI.Screens
{
    public class OverlayScreen : Screen
    {
        [SerializeField] private float hideDelay;
        
        
        protected override Sequence ShowCallback(Sequence showSequence)
        {
            showSequence.AppendInterval(hideDelay);
            showSequence.AppendCallback(()=>Hide());
            return showSequence;
        }
    }
}