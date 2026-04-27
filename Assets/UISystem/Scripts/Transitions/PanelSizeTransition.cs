using System.Threading.Tasks;
using PrimeTween;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    /// <summary>
    /// Transition where panel is grows/shrinks in size.
    /// </summary>
    public class PanelSizeTransition : IViewTransition
    {
        private const float FadeDuration = 0.1f;
        private const float PanelDuration = 0.2f;

        private readonly CanvasGroup _fadeObjectsContainer;
        private readonly RectTransform _panel;
        private readonly float _panelDuration;

        private Vector2 _panelSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelSizeTransition"/> class.
        /// </summary>
        /// <param name="fadeObjectsContainer">Canvas group that contains all objects.</param>
        /// <param name="panel">Panel that will be resized.</param>
        /// <param name="duration">Duration to resize panel.</param>
        public PanelSizeTransition(CanvasGroup fadeObjectsContainer, RectTransform panel, float duration = PanelDuration)
        {
            _fadeObjectsContainer = fadeObjectsContainer;
            _panel = panel;
            _panelDuration = duration;
        }

        /// <inheritdoc/>
        public async Task Hide(bool instant = false)
        {
            if (instant)
            {
                _fadeObjectsContainer.alpha = 0;
                return;
            }

            var sequence = Sequence.Create();
            await sequence
                .Group(Tween.UISizeDelta(_panel, -_panelSize, _panelDuration))
                .Chain(Tween.Alpha(_fadeObjectsContainer, 0, FadeDuration));
        }

        /// <inheritdoc/>
        public async Task Show(bool instant = false)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            _fadeObjectsContainer.alpha = 0;

            await InitElementParameters();

            if (instant)
            {
                _panel.sizeDelta = Vector2.zero;
                _fadeObjectsContainer.alpha = 1;
                return;
            }

            _panel.sizeDelta = -new Vector2(_panelSize.x, _panelSize.y);

            var sequence = Sequence.Create();
            await sequence
                .Group(Tween.Alpha(_fadeObjectsContainer, 1, FadeDuration))
                .Chain(Tween.UISizeDelta(_panel, Vector2.zero, _panelDuration));
        }

        private async Task InitElementParameters()
        {
            await Task.Delay(100);
            _panelSize = new Vector2(_panel.rect.width, _panel.rect.height);
        }
    }
}
