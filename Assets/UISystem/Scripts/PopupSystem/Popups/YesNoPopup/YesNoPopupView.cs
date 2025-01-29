using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem.Popups.Views
{
    internal partial class YesNoPopupView : PopupView
    {

        [SerializeField] private Button yesButton;
        [SerializeField] private Button noButton;

        public Button YesButton => yesButton;
        public Button NoButton => noButton;

        public override Selectable DefaultSelectedElement => NoButton;
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
            //new ITweenableMenuElement[] { YesButton, NoButton, MessageMask });
        }

        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { YesButton, NoButton };
        }

    }
}