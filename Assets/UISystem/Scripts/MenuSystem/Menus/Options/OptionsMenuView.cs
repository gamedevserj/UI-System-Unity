using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class OptionsMenuView : MenuView
    {

        [SerializeField] private ButtonView interfaceSettingsButton;
        [SerializeField] private ButtonView audioSettingsButton;
        [SerializeField] private ButtonView videoSettingsButton;
        [SerializeField] private ButtonView rebindKeysButton;
        [SerializeField] private ButtonView returnButton;

        public ButtonView ReturnButton => returnButton;
        public ButtonView InterfaceSettingsButton => interfaceSettingsButton;
        public ButtonView AudioSettingsButton => audioSettingsButton;
        public ButtonView VideoSettingsButton => videoSettingsButton;
        public ButtonView RebindKeysButton => rebindKeysButton;

        protected override Selectable DefaultSelectedElement => InterfaceSettingsButton.Button;

        protected override IViewTransition CreateTransition()
        {
            return new MainElementDropTransition(FadeObjectsContainer, InterfaceSettingsButton, 
                new IResizableElement[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton });
        }
        protected override void SetInteractableElements()
        {
            _interactableElements = new IInteractableElement[] { ReturnButton, AudioSettingsButton, VideoSettingsButton,
            RebindKeysButton, InterfaceSettingsButton };
        }

    }
}