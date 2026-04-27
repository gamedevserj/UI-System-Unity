using PrimeTween;
using System.Threading.Tasks;
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

        public async Task Hide(bool instant = false)
        {
            if (instant)
            {
                _target.alpha = 0;
                return;
            }

            await Tween.Alpha(_target, 0, Duration, Ease.Linear);
            _target.alpha = 0;
        }

        public async Task Show(bool instant = false)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            _target.alpha = 0;

            if (instant)
            {
                _target.alpha = 1;
                return;
            }

            await Tween.Alpha(_target, 1, Duration, Ease.Linear);
            _target.alpha = 1;
        }
    }
}