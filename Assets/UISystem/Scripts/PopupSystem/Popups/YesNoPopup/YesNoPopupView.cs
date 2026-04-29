using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem.Popups.Views
{
    /// <summary>
    /// Tes/No popup view.
    /// </summary>
    internal partial class YesNoPopupView : PopupView
    {
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;

        /// <summary>
        /// Gets yes button.
        /// </summary>
        public Button YesButton => _yesButton;

        /// <summary>
        /// Gets no button.
        /// </summary>
        public Button NoButton => _noButton;

        /// <inheritdoc/>
        public override Selectable DefaultSelectedElement => NoButton;

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
