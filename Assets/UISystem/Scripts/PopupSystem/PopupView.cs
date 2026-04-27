using TMPro;
using UISystem.Core.PopupSystem;
using UISystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem
{
    /// <summary>
    /// Base class for popup views.
    /// </summary>
    public abstract partial class PopupView : ViewBase, IPopupView
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private TextMeshProUGUI _messageLabel;

        /// <summary>
        /// Gets the element that is focused by default when popup is shown for the first time.
        /// </summary>
        public abstract Selectable DefaultSelectedElement { get; }

        /// <inheritdoc/>
        public override void FocusElement()
        {
            if (DefaultSelectedElement != null)
            {
                DefaultSelectedElement.Select();
            }
        }

        /// <inheritdoc/>
        public void SetMessage(string message)
        {
            _messageLabel.text = message;
        }
    }
}
