using DG.Tweening;
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

        private bool _initializedParameters;
        private Vector3 _mainElementPosition;

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

        public async void Hide(Action onHidden, bool instant)
        {
            if (instant)
            {
                _fadeObjectsContainer.alpha = 0;
                onHidden?.Invoke();
                return;
            }

            //var tasks = new Task[_secondaryElements.Length + 1];
            //for (int i = 0; i < _secondaryElements.Length; i++)
            //{
            //    tasks[i] = _secondaryElements[i].ResetHover();
            //}
            //tasks[_secondaryElements.Length] = _mainElement.ResetHover();
            //await Task.WhenAll(tasks);

            Sequence sequence = DOTween.Sequence();
            sequence.SetEase(Ease.Linear);
            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                //_secondaryElements[i].Resizable.position = _mainElement.Resizable.position;
                Tween tween = _secondaryElements[i].Resizable.DOMove(_mainElementPosition, _secondaryElementDuration);
                if (i == _secondaryElements.Length - 1)
                    tween.OnComplete(() => SwitchSecondaryButtonsVisibility(false));
                sequence.Join(tween.SetEase(Ease.InBack));
            }

            sequence.Append(_mainElement.Resizable.DOSizeDelta(Vector2.left * _mainElement.ButtonTransform.sizeDelta.x, _mainElementDuration))
                .Join(_mainElement.Resizable.DOAnchorPos(Vector2.left * _mainElement.ButtonTransform.sizeDelta.x * 0.5f, _mainElementDuration));

            sequence.Append(_fadeObjectsContainer.DOFade(0, FadeDuration));
            sequence.Play().OnComplete(() => onHidden?.Invoke());
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

            if (!_initializedParameters)
            {
                // maybe use UniTask to delay a frame?
                await InitElementParameters();
            }

            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                _secondaryElements[i].Resizable.position = _mainElementPosition;
            }

            _mainElement.Resizable.sizeDelta = Vector2.left * _mainElement.ButtonTransform.sizeDelta.x;
            _mainElement.Resizable.anchoredPosition = Vector2.left * _mainElement.ButtonTransform.sizeDelta.x * 0.5f;
            _mainElement.Resizable.gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();
            sequence.SetEase(Ease.Linear)
                .Join(_fadeObjectsContainer.DOFade(1, FadeDuration))
                .Append(_mainElement.Resizable.DOSizeDelta(Vector2.zero, _mainElementDuration))
                .Join(_mainElement.Resizable.DOAnchorPos(Vector2.zero, _mainElementDuration)
                    .OnComplete(() => SwitchSecondaryButtonsVisibility(true)));

            Ease secondaryEase = Ease.OutBack;
            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                Tween tween = _secondaryElements[i].Resizable.DOAnchorPos(Vector2.zero, _secondaryElementDuration)
                    .SetEase(secondaryEase);
                if (i == 0)
                    sequence.Append(tween);
                else
                    sequence.Join(tween);
            }

            sequence.Play().OnComplete(() => onShown?.Invoke());
        }

        private async Task InitElementParameters()
        {
            // maybe use UniTask to delay a frame?
            await Task.Delay(100);
            _mainElementPosition = _mainElement.Resizable.position;
            _initializedParameters = true;
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