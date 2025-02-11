using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem.Popups.Views
{
    internal partial class YesPopupView : PopupView
    {

        [SerializeField] private Button yesButton;

        public Button YesButton => yesButton;
        public override Selectable DefaultSelectedElement => YesButton;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }

        protected override void SetInteractableElements()
        {
            
        }

    }
}