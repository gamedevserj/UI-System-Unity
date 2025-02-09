using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class InterfaceSettingsMenuView : SettingsMenuView
    {

        [SerializeField] private DropdownView controllerIconsDropdown;
        [SerializeField] private ButtonView saveSettingsButton;
        [SerializeField] private RectTransform panel;

        public ButtonView SaveSettingsButton => saveSettingsButton;
        public DropdownView ControllerIconsDropdown => controllerIconsDropdown;
        public RectTransform Panel => panel;

        protected override IViewTransition CreateTransition()
        {
            return new PanelSizeTransition(FadeObjectsContainer, panel);
        }
        protected override void SetInteractableElements()
        {
            _interactableElements = new IInteractableElement[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
        }
    }

}