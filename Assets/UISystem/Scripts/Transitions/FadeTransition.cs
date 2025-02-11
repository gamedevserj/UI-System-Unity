using DG.Tweening;
using System;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    public class FadeTransition : IViewTransition
    {

        private const float Duration = 0.15f;

        private readonly CanvasGroup _target;

        public FadeTransition(CanvasGroup target)
        {
            _target = target;
        }

        public void Hide(Action onHidden, bool instant)
        {
            void FinishedHiding()
            {
                _target.alpha = 0;
                onHidden?.Invoke();
            }

            if (instant)
            {
                FinishedHiding();
                return;
            }
            _target.DOFade(0, Duration).SetEase(Ease.Linear).OnComplete(()=>
            {
                FinishedHiding();
            }).Play();
        }

        public void Show(Action onShown, bool instant)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            _target.alpha = 0;

            void FinishedShowing()
            {
                _target.alpha = 1;
                onShown?.Invoke();
            }

            if (instant)
            {
                FinishedShowing();
                return;
            }

            _target.DOFade(1, Duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                FinishedShowing();
            }).Play();
        }
    }
}