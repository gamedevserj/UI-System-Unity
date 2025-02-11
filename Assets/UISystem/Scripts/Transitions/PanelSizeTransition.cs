using DG.Tweening;
using System;
using System.Threading.Tasks;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    public class PanelSizeTransition : IViewTransition
    {

        private const float FadeDuration = 0.1f;
        protected const float PanelDuration = 0.2f;
        protected const float ElementsDuration = 0.1f;

        private Vector2 _panelSize;

        private readonly CanvasGroup _fadeObjectsContainer;
        private readonly RectTransform _panel;
        private readonly float _panelDuration;

        public PanelSizeTransition(CanvasGroup fadeObjectsContainer, RectTransform panel, float panelDuration = PanelDuration)
        {
            _fadeObjectsContainer = fadeObjectsContainer;
            _panel = panel;
            _panelDuration = panelDuration;
            
        }

        public void Hide(Action onHidden, bool instant)
        {
            if (instant)
            {
                _fadeObjectsContainer.alpha = 0;
                onHidden?.Invoke();
                return;
            }

            Sequence sequence = DOTween.Sequence();
            sequence.Join(_panel.DOSizeDelta(-_panelSize, _panelDuration))
                .Append(_fadeObjectsContainer.DOFade(1, FadeDuration))
                .OnComplete(() => onHidden?.Invoke())
                .Play();
        }

        public async void Show(Action onShown, bool instant)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            _fadeObjectsContainer.alpha = 0;

            await InitElementParameters();

            if (instant)
            {
                _panel.sizeDelta = Vector2.zero;
                _fadeObjectsContainer.alpha = 1;
                onShown?.Invoke();
                return;
            }

            _panel.sizeDelta = -new Vector2(_panelSize.x, _panelSize.y);

            Sequence sequence = DOTween.Sequence();
            sequence.Join(_fadeObjectsContainer.DOFade(1, FadeDuration))
                .Append(_panel.DOSizeDelta(Vector2.zero, _panelDuration))
                .OnComplete(() => onShown?.Invoke())
                .Play();
        }

        private async Task InitElementParameters()
        {
            await Task.Delay(100);
            _panelSize = new Vector2(_panel.rect.width, _panel.rect.height);
        }
    }
}