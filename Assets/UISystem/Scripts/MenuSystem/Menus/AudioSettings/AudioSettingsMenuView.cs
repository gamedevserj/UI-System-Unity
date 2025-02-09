using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    public partial class AudioSettingsMenuView : SettingsMenuView
    {

        [SerializeField] private SliderView musicSlider;
        [SerializeField] private SliderView sfxSlider;
        [SerializeField] private ButtonView saveSettingsButton;
        [SerializeField] private RectTransform panel; 

        public SliderView MusicSlider => musicSlider;
        public SliderView SfxSlider => sfxSlider;
        public ButtonView SaveSettingsButton => saveSettingsButton;
        public RectTransform Panel => panel;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
            //new ITweenableMenuElement[] { ReturnButton, SaveSettingsButton, ResetButton,
            //MusicSlider, SfxSlider, ResizableControlMusic, ResizableControlSfx });
        }
        protected override void SetInteractableElements()
        {
            _interactableElements = new IInteractableElement[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
        }

    }
}