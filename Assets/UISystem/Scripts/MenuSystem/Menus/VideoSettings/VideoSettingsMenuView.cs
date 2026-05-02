using UISystem.Common.Elements;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Video settings menu view.
    /// </summary>
    public partial class VideoSettingsMenuView : SettingsMenuView
    {
        [SerializeField] private DropdownView _windowModeDropdown;
        [SerializeField] private DropdownView _resolutionDropdown;
        [SerializeField] private DropdownView _refreshRateDropdown;
        [SerializeField] private ButtonView _saveSettingsButton;
        [SerializeField] private RectTransform _panel;

        /// <summary>
        /// Gets windowed mode dropdown.
        /// </summary>
        public DropdownView WindowModeDropdown => _windowModeDropdown;

        /// <summary>
        /// Gets resolution dropdown.
        /// </summary>
        public DropdownView ResolutionDropdown => _resolutionDropdown;

        /// <summary>
        /// Gets refresh rate dropdown.
        /// </summary>
        public DropdownView RefreshRateDropdown => _refreshRateDropdown;

        /// <summary>
        /// Gets save settings button.
        /// </summary>
        public ButtonView SaveSettingsButton => _saveSettingsButton;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new PanelSizeTransition(FadeObjectsContainer, _panel);
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[]
            {
                WindowModeDropdown,
                ResolutionDropdown,
                RefreshRateDropdown,
                SaveSettingsButton,
                ReturnButton,
                ResetButton,
            };
        }
    }
}
