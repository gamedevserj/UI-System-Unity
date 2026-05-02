using UISystem.Common.Elements;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Pause menu view.
    /// </summary>
    public partial class PauseMenuView : MenuView
    {
        [SerializeField] private ButtonView _resumeGameButton;
        [SerializeField] private ButtonView _optionsButton;
        [SerializeField] private ButtonView _returnToMainMenuButton;

        /// <summary>
        /// Gets resume game button.
        /// </summary>
        public ButtonView ResumeGameButton => _resumeGameButton;

        /// <summary>
        /// Gets options button.
        /// </summary>
        public ButtonView OptionsButton => _optionsButton;

        /// <summary>
        /// Gets return to main menu button.
        /// </summary>
        public ButtonView ReturnToMainMenuButton => _returnToMainMenuButton;

        /// <inheritdoc/>
        protected override IInteractableElement DefaultSelectedElement => ResumeGameButton;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new MainElementDropTransition(FadeObjectsContainer, ResumeGameButton, new[] { OptionsButton, ReturnToMainMenuButton });
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
        }
    }
}
