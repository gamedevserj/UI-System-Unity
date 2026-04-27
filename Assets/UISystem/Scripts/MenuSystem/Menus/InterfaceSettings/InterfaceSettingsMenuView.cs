using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Interface settings menu view.
    /// </summary>
    public partial class InterfaceSettingsMenuView : SettingsMenuView
    {
        [SerializeField] private DropdownView _controllerIconsDropdown;
        [SerializeField] private ButtonView _saveSettingsButton;
        [SerializeField] private RectTransform _panel;

        /// <summary>
        /// Gets save settings button.
        /// </summary>
        public ButtonView SaveSettingsButton => _saveSettingsButton;

        /// <summary>
        /// Gets controller icons dropdown.
        /// </summary>
        public DropdownView ControllerIconsDropdown => _controllerIconsDropdown;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new PanelSizeTransition(FadeObjectsContainer, _panel);
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
        }
    }
}
