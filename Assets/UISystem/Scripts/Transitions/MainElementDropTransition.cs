using PrimeTween;
using System;
using System.Linq;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    public class MainElementDropTransition : IViewTransition
    {

        private const float FadeDuration = 0.1f;
        private const float MainElementAnimationDuration = 0.2f;
        private const float SecondaryElementAnimationDuration = 0.2f;

        private readonly CanvasGroup _fadeObjectsContainer;
        private readonly IResizableElement _mainElement;
        private readonly IResizableElement[] _secondaryElements;
        private readonly float _mainElementDuration;
        private readonly float _secondaryElementDuration;

        public MainElementDropTransition(CanvasGroup fadeObjectsContainer, IResizableElement mainResizableControl,
            IResizableElement[] secondaryElements, float mainElementDuration = MainElementAnimationDuration,
            float secondaryElementDuration = SecondaryElementAnimationDuration)
        {
            _fadeObjectsContainer = fadeObjectsContainer;
            _mainElement = mainResizableControl;
            _secondaryElements = secondaryElements;
            _mainElementDuration = mainElementDuration;
            _secondaryElementDuration = secondaryElementDuration;
        }

        public void Hide(Action onHidden, bool instant)
        {
            if (instant)
            {
                _fadeObjectsContainer.alpha = 0;
                onHidden?.Invoke();
                return;
            }

            var sequence = Sequence.Create();
            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                sequence
                    .Group(Tween.Position(_secondaryElements[i].Resizable, _mainElement.Reference.position, _secondaryElementDuration, Ease.InBack));
            }
            sequence
                .ChainCallback(target: this, target => target.SwitchSecondaryButtonsVisibility(false))
                .Chain(Tween.UISizeDelta(_mainElement.Resizable, _mainElement.Reference.sizeDelta.x * Vector2.left, _mainElementDuration))
                .Group(Tween.UIAnchoredPosition(_mainElement.Resizable, _mainElement.Reference.sizeDelta.x * 0.5f * Vector2.left, _mainElementDuration))
                .Chain(Tween.Alpha(_fadeObjectsContainer, 0, FadeDuration)
                .OnComplete(() => onHidden?.Invoke()));
        }

        public async void Show(Action onShown, bool instant)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            _mainElement.Resizable.gameObject.SetActive(false);
            _fadeObjectsContainer.alpha = 0;
            SwitchSecondaryButtonsVisibility(false);

            if (instant)
            {
                _mainElement.Resizable.sizeDelta = Vector2.zero;
                _mainElement.Resizable.anchoredPosition = Vector2.zero;
                _mainElement.Resizable.gameObject.SetActive(true);
                for (int i = 0; i < _secondaryElements.Length; i++)
                {
                    _secondaryElements[i].Resizable.anchoredPosition = Vector2.zero;
                }
                SwitchSecondaryButtonsVisibility(true);
                _fadeObjectsContainer.alpha = 1;
                onShown?.Invoke();
                return;
            }

            await Task.Delay(100);

            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                _secondaryElements[i].Resizable.position = _mainElement.Reference.position;
            }

            _mainElement.Resizable.sizeDelta = _mainElement.Reference.sizeDelta.x * Vector2.left ;
            _mainElement.Resizable.anchoredPosition = _mainElement.Reference.sizeDelta.x * 0.5f * Vector2.left;
            _mainElement.Resizable.gameObject.SetActive(true);

            var sequence = Sequence.Create();
            sequence
                .Group(Tween.Alpha(_fadeObjectsContainer, 1, FadeDuration))
                .Chain(Tween.UISizeDelta(_mainElement.Resizable, Vector2.zero, _mainElementDuration))
                .Group(
                    Tween.UIAnchoredPosition(_mainElement.Resizable, Vector2.zero, _mainElementDuration)
                    .OnComplete(target: this, target => target.SwitchSecondaryButtonsVisibility(true)))
                .Chain(
                    Tween.UIAnchoredPosition(_secondaryElements[0].Resizable, Vector2.zero, _secondaryElementDuration, Ease.OutBack));

            for (int i = 1; i < _secondaryElements.Length; i++)
            {
                sequence.Group(Tween.UIAnchoredPosition(_secondaryElements[i].Resizable, Vector2.zero, _secondaryElementDuration, Ease.OutBack));
            }

            sequence.OnComplete(() => onShown?.Invoke());
        }

        private void SwitchSecondaryButtonsVisibility(bool show)
        {
            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                _secondaryElements[i].Resizable.gameObject.SetActive(show);
            }
        }
    }
}