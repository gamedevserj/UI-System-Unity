using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem.Popups.Views
{
    /// <summary>
    /// Tes/No/Cancel popup view.
    /// </summary>
    internal partial class YesNoCancelPopupView : PopupView
    {
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private Button _cancelButton;

        /// <summary>
        /// Gets yes button.
        /// </summary>
        public Button YesButton => _yesButton;

        /// <summary>
        /// Gets no button.
        /// </summary>
        public Button NoButton => _noButton;

        /// <summary>
        /// Gets cancel button.
        /// </summary>
        public Button CancelButton => _cancelButton;

        /// <inheritdoc/>
        public override Selectable DefaultSelectedElement => CancelButton;

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
