using TMPro;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class InterfaceSettingsMenuView : SettingsMenuView
    {

        [SerializeField] private TMP_Dropdown controllerIconsDropdown;
        [SerializeField] private Button saveSettingsButton;
        [SerializeField] private RectTransform panel;

        public Button SaveSettingsButton => saveSettingsButton;
        public TMP_Dropdown ControllerIconsDropdown => controllerIconsDropdown;
        public RectTransform Panel => panel;
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }
        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
        }
    }

}