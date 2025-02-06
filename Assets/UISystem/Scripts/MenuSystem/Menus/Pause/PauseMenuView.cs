using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class PauseMenuView : MenuView
    {

        [SerializeField] private Button resumeGameButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button returnToMainMenuButton;

        public Button ResumeGameButton => resumeGameButton;
        public Button OptionsButton => optionsButton;
        public Button ReturnToMainMenuButton => returnToMainMenuButton;

        protected override Selectable DefaultSelectedElement => ResumeGameButton;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new MainElementDropTransition(this, FadeObjectsContainer, ResumeGameButton, new[] { OptionsButton, ReturnToMainMenuButton });
        }

        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { ResumeGameButton, OptionsButton, ReturnToMainMenuButton };
        }

    }
}