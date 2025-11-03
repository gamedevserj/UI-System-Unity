using PrimeTween;
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
            void Finished()
            {
                _target.alpha = 0;
                onHidden?.Invoke();
            }

            if (instant)
            {
                Finished();
                return;
            }

            Tween.Alpha(_target, 0, Duration, Ease.Linear).OnComplete(Finished);
        }

        public void Show(Action onShown, bool instant)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            _target.alpha = 0;

            void Finished()
            {
                _target.alpha = 1;
                onShown?.Invoke();
            }

            if (instant)
            {
                Finished();
                return;
            }

            Tween.Alpha(_target, 1, Duration, Ease.Linear).OnComplete(Finished);
        }
    }
}