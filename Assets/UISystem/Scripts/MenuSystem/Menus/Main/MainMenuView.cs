using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class MainMenuView : MenuView
    {

        [SerializeField] private Button playButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;

        public Button PlayButton => playButton;
        public Button OptionsButton => optionsButton;
        public Button QuitButton => quitButton;
        public CanvasGroup FadeObjectsContainer => canvasGroup;

        protected override Selectable DefaultSelectedElement => PlayButton;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new MainElementDropTransition(this, FadeObjectsContainer, PlayButton, new[] { OptionsButton, QuitButton });
        }

        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
        }

    }
}