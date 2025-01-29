using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class OptionsMenuView : MenuView
    {

        [SerializeField] private Button interfaceSettingsButton;
        [SerializeField] private Button audioSettingsButton;
        [SerializeField] private Button videoSettingsButton;
        [SerializeField] private Button rebindKeysButton;
        [SerializeField] private Button returnButton;

        public Button ReturnButton => returnButton;
        public Button InterfaceSettingsButton => interfaceSettingsButton;
        public Button AudioSettingsButton => audioSettingsButton;
        public Button VideoSettingsButton => videoSettingsButton;
        public Button RebindKeysButton => rebindKeysButton;
        public CanvasGroup FadeObjectsContainer => canvasGroup;

        protected override Selectable DefaultSelectedElement => InterfaceSettingsButton;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new MainElementDropTransition(this, FadeObjectsContainer, InterfaceSettingsButton,
            //new[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, RebindKeysButton });
        }
        protected override void PopulateFocusableElements()
        {

            //_focusableElements = new IFocusableControl[] { ReturnButton, AudioSettingsButton, VideoSettingsButton,
            //RebindKeysButton, InterfaceSettingsButton };
        }

    }
}