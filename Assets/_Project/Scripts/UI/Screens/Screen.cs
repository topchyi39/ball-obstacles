using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.UI.Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Screen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration;
        
        private void OnValidate()
        {
            canvasGroup ??= GetComponent<CanvasGroup>();
        }

        public Sequence Show()
        {
            var sequence = DOTween.Sequence();;
            sequence.Append(canvasGroup.DOFade(1f, fadeDuration));
            sequence.AppendCallback(()=>canvasGroup.blocksRaycasts = true);
            return ShowCallback(sequence);
        }

        public Sequence Hide()
        {
            var sequence = DOTween.Sequence();;
            sequence.AppendCallback(()=>canvasGroup.blocksRaycasts = false);
            sequence.Append(canvasGroup.DOFade(0f, fadeDuration));
            return HideCallback(sequence);
        }

        protected virtual Sequence ShowCallback(Sequence showSequence) => showSequence;
        protected virtual Sequence HideCallback(Sequence hideSequence) => hideSequence;
    }
}