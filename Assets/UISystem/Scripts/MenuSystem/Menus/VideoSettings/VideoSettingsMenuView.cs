using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    public partial class VideoSettingsMenuView : SettingsMenuView
    {

        [SerializeField] private DropdownView windowModeDropdown;
        [SerializeField] private DropdownView resolutionDropdown;
        [SerializeField] private DropdownView refreshRateDropdown;
        [SerializeField] private ButtonView saveSettingsButton;
        [SerializeField] private RectTransform panel;

        public DropdownView WindowModeDropdown => windowModeDropdown;
        public DropdownView ResolutionDropdown => resolutionDropdown;
        public DropdownView RefreshRateDropdown => refreshRateDropdown;
        public ButtonView SaveSettingsButton => saveSettingsButton;
        public RectTransform Panel => panel;
        protected override IViewTransition CreateTransition()
        {
            return new PanelSizeTransition(FadeObjectsContainer, Panel);
        }

        protected override void SetInteractableElements()
        {
            _interactableElements = new IInteractableElement[] {WindowModeDropdown, ResolutionDropdown, RefreshRateDropdown, 
                SaveSettingsButton, ReturnButton, ResetButton};
        }

    }
}