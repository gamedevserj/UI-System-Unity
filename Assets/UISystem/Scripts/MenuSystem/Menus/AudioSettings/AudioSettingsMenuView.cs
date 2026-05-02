using UISystem.Common.Elements;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Audio setting menu view.
    /// </summary>
    public partial class AudioSettingsMenuView : SettingsMenuView
    {
        [SerializeField] private SliderView _musicSlider;
        [SerializeField] private SliderView _sfxSlider;
        [SerializeField] private ButtonView _saveSettingsButton;
        [SerializeField] private RectTransform _panel;

        /// <summary>
        /// Gets music slider.
        /// </summary>
        public SliderView MusicSlider => _musicSlider;

        /// <summary>
        /// Gets SFX slider.
        /// </summary>
        public SliderView SfxSlider => _sfxSlider;

        /// <summary>
        /// Gets save settings button.
        /// </summary>
        public ButtonView SaveSettingsButton => _saveSettingsButton;

        /// <summary>
        /// Gets panel containing elements.
        /// </summary>
        public RectTransform Panel => _panel;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new PanelSizeTransition(FadeObjectsContainer, Panel);
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
        }
    }
}
