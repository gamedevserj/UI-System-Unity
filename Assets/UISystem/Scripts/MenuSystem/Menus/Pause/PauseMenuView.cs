using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class PauseMenuView : MenuView
    {

        [SerializeField] private ButtonView resumeGameButton;
        [SerializeField] private ButtonView optionsButton;
        [SerializeField] private ButtonView returnToMainMenuButton;

        public ButtonView ResumeGameButton => resumeGameButton;
        public ButtonView OptionsButton => optionsButton;
        public ButtonView ReturnToMainMenuButton => returnToMainMenuButton;

        protected override Selectable DefaultSelectedElement => ResumeGameButton.Button;

        protected override IViewTransition CreateTransition()
        {
            //return new FadeTransition(FadeObjectsContainer);
            return new MainElementDropTransition(FadeObjectsContainer, ResumeGameButton, new[] { OptionsButton, ReturnToMainMenuButton });
        }

        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
        }

    }
}