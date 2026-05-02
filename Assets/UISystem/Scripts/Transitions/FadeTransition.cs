using System.Threading.Tasks;
using PrimeTween;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    /// <summary>
    /// Fades using canvas group.
    /// </summary>
    public class FadeTransition : IViewTransition
    {
        private const float Duration = 0.15f;

        private readonly CanvasGroup _target;

        /// <summary>
        /// Initializes a new instance of the <see cref="FadeTransition"/> class.
        /// </summary>
        /// <param name="target">Target canvas group.</param>
        public FadeTransition(CanvasGroup target)
        {
            _target = target;
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
