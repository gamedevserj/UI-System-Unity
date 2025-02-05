using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class AudioSettingsMenuView : SettingsMenuView
    {

        //[SerializeField] private RectTransform resizableControlMusic; // label container
        [SerializeField] private Slider musicSlider;
        //[SerializeField] private RectTransform resizableControlSfx; // label container
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Button saveSettingsButton;
        [SerializeField] private RectTransform panel; 

        public Slider MusicSlider => musicSlider;
        public Slider SfxSlider => sfxSlider;
        public Button SaveSettingsButton => saveSettingsButton;
        public RectTransform Panel => panel;
        //public RectTransform ResizableControlMusic => resizableControlMusic;
        //public RectTransform ResizableControlSfx => resizableControlSfx;
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
            //new ITweenableMenuElement[] { ReturnButton, SaveSettingsButton, ResetButton,
            //MusicSlider, SfxSlider, ResizableControlMusic, ResizableControlSfx });
        }
        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { MusicSlider, SfxSlider, SaveSettingsButton, ResetButton, ReturnButton };
        }

    }
}