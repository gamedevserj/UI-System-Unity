using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class MainMenuView : MenuView
    {

        [SerializeField] private ButtonView playButton;
        [SerializeField] private ButtonView optionsButton;
        [SerializeField] private ButtonView quitButton;

        public ButtonView PlayButton => playButton;
        public ButtonView OptionsButton => optionsButton;
        public ButtonView QuitButton => quitButton;

        protected override Selectable DefaultSelectedElement => PlayButton.Button;

        protected override IViewTransition CreateTransition()
        {
            return new MainElementDropTransition(FadeObjectsContainer, PlayButton, new IResizableElement[] { OptionsButton, QuitButton });
        }

        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
        }

    }
}