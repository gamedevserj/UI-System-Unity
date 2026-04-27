using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Options menu view.
    /// </summary>
    public partial class OptionsMenuView : MenuView
    {
        [SerializeField] private ButtonView _interfaceSettingsButton;
        [SerializeField] private ButtonView _audioSettingsButton;
        [SerializeField] private ButtonView _videoSettingsButton;
        [SerializeField] private ButtonView _rebindKeysButton;
        [SerializeField] private ButtonView _returnButton;

        /// <summary>
        /// Gets return button.
        /// </summary>
        public ButtonView ReturnButton => _returnButton;

        /// <summary>
        /// Gets interface settings button.
        /// </summary>
        public ButtonView InterfaceSettingsButton => _interfaceSettingsButton;

        /// <summary>
        /// Gets audio settings button.
        /// </summary>
        public ButtonView AudioSettingsButton => _audioSettingsButton;

        /// <summary>
        /// Gets video settings button.
        /// </summary>
        public ButtonView VideoSettingsButton => _videoSettingsButton;

        /// <summary>
        /// Gets rebind keys button.
        /// </summary>
        public ButtonView RebindKeysButton => _rebindKeysButton;

        /// <inheritdoc/>
        protected override Selectable DefaultSelectedElement => InterfaceSettingsButton.Button;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new MainElementDropTransition(
                FadeObjectsContainer,
                InterfaceSettingsButton,
                new IResizableElement[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton });
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[]
            {
                ReturnButton,
                AudioSettingsButton,
                VideoSettingsButton,
                RebindKeysButton,
                InterfaceSettingsButton,
            };
        }
    }
}
