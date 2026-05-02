using System.Threading.Tasks;
using PrimeTween;
using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    /// <summary>
    /// Transition where elements fall down from the main one.
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MainElementDropTransition"/> class.
        /// </summary>
        /// <param name="fadeObjectsContainer">Canvas group that contains all objects.</param>
        /// <param name="mainResizableControl">Main resizable element.</param>
        /// <param name="secondaryElements">Secondary elements.</param>
        /// <param name="mainElementDuration">Duration for the main element to resize.</param>
        /// <param name="secondaryElementDuration">Duration for the secondary elements to drop.</param>
        public MainElementDropTransition(
            CanvasGroup fadeObjectsContainer,
            IResizableElement mainResizableControl,
            IResizableElement[] secondaryElements,
            float mainElementDuration = MainElementAnimationDuration,
            float secondaryElementDuration = SecondaryElementAnimationDuration)
        {
            _fadeObjectsContainer = fadeObjectsContainer;
            _mainElement = mainResizableControl;
            _secondaryElements = secondaryElements;
            _mainElementDuration = mainElementDuration;
            _secondaryElementDuration = secondaryElementDuration;
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
            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                _ = sequence
                    .Group(Tween.Position(
                        _secondaryElements[i].Resizable,
                        _mainElement.Reference.position,
                        _secondaryElementDuration,
                        Ease.InBack));
            }

            await sequence
                .ChainCallback(target: this, target => target.SwitchSecondaryButtonsVisibility(false))
                .Chain(Tween.UISizeDelta(_mainElement.Resizable, _mainElement.Reference.sizeDelta.x * Vector2.left, _mainElementDuration))
                .Group(Tween.UIAnchoredPosition(_mainElement.Resizable, _mainElement.Reference.sizeDelta.x * 0.5f * Vector2.left, _mainElementDuration))
                .Chain(Tween.Alpha(_fadeObjectsContainer, 0, FadeDuration));
        }

        /// <inheritdoc/>
        public async Task Show(bool instant = false)
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
                return;
            }

            // if you're using Unitask you can replace it to wait one frame until elements are correctly setup by canvas
            await Task.Delay(100);

            for (int i = 0; i < _secondaryElements.Length; i++)
            {
                _secondaryElements[i].Resizable.position = _mainElement.Reference.position;
            }

            _mainElement.Resizable.sizeDelta = _mainElement.Reference.sizeDelta.x * Vector2.left;
            _mainElement.Resizable.anchoredPosition = _mainElement.Reference.sizeDelta.x * 0.5f * Vector2.left;
            _mainElement.Resizable.gameObject.SetActive(true);

            var sequence = Sequence.Create();
            _ = sequence
                .Group(Tween.Alpha(_fadeObjectsContainer, 1, FadeDuration))
                .Chain(Tween.UISizeDelta(_mainElement.Resizable, Vector2.zero, _mainElementDuration))
                .Group(
                    Tween.UIAnchoredPosition(_mainElement.Resizable, Vector2.zero, _mainElementDuration)
                    .OnComplete(target: this, target => target.SwitchSecondaryButtonsVisibility(true)))
                .Chain(
                    Tween.UIAnchoredPosition(_secondaryElements[0].Resizable, Vector2.zero, _secondaryElementDuration, Ease.OutBack));

            for (int i = 1; i < _secondaryElements.Length; i++)
            {
                _ = sequence.Group(Tween.UIAnchoredPosition(_secondaryElements[i].Resizable, Vector2.zero, _secondaryElementDuration, Ease.OutBack));
            }

            await sequence;
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
