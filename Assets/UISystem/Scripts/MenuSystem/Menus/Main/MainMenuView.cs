using UISystem.Common.Elements;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Main menu view.
    /// </summary>
    public partial class MainMenuView : MenuView
    {
        [SerializeField] private ButtonView _playButton;
        [SerializeField] private ButtonView _optionsButton;
        [SerializeField] private ButtonView _quitButton;

        /// <summary>
        /// Gets play button.
        /// </summary>
        public ButtonView PlayButton => _playButton;

        /// <summary>
        /// Gets options button.
        /// </summary>
        public ButtonView OptionsButton => _optionsButton;

        /// <summary>
        /// Gets quit button.
        /// </summary>
        public ButtonView QuitButton => _quitButton;

        /// <inheritdoc/>
        protected override IInteractableElement DefaultSelectedElement => PlayButton;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new MainElementDropTransition(FadeObjectsContainer, PlayButton, new IResizableElement[] { OptionsButton, QuitButton });
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[] { PlayButton, OptionsButton, QuitButton };
        }
    }
}
