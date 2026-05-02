using UISystem.Core.Elements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    /// <summary>
    /// Button view.
    /// </summary>
    public class ButtonView : MonoBehaviour, IInteractableElement, IResizableElement
    {
        [SerializeField] private RectTransform _resizable;

        private Button _button;
        private RectTransform _rectTransform;

        /// <summary>
        /// Gets Button component.
        /// </summary>
        public Button Button
        {
            get
            {
                if (_button == null)
                    _button = GetComponent<Button>();

                return _button;
            }
        }

        /// <inheritdoc/>
        public RectTransform Reference
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();

                return _rectTransform;
            }
        }

        /// <inheritdoc/>
        public RectTransform Resizable => _resizable;

        /// <inheritdoc/>
        public void Select() => Button.Select();

        /// <inheritdoc/>
        public void SwitchInteractability(bool enable) => Button.enabled = enable;

        /// <summary>
        /// Adds action to perform on click.
        /// </summary>
        /// <param name="action">Action to perform on click.</param>
        public void AddOnClickListener(UnityAction action) => Button.onClick.AddListener(action);
    }
}
