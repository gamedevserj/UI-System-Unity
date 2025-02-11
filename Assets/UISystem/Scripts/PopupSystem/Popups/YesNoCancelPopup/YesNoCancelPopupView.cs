using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem.Popups.Views
{
    internal partial class YesNoCancelPopupView : PopupView
    {

        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;
        [SerializeField] private Button cancelButton;

        public Button YesButton => yesButton;
        public Button NoButton => noButton;
        public Button CancelButton => cancelButton;

        public override Selectable DefaultSelectedElement => CancelButton;
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }
        protected override void SetInteractableElements()
        {
            
        }

    }
}