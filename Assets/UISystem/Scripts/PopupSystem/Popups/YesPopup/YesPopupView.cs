using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem.Popups.Views
{
    /// <summary>
    /// Yes popup view.
    /// </summary>
    internal partial class YesPopupView : PopupView
    {
        [SerializeField] private Button _yesButton;

        /// <summary>
        /// Gets yes button.
        /// </summary>
        public Button YesButton => _yesButton;

        /// <inheritdoc/>
        public override Selectable DefaultSelectedElement => YesButton;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
        }
    }
}
