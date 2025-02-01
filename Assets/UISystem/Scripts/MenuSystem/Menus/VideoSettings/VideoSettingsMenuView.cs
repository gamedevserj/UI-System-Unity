using TMPro;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class VideoSettingsMenuView : SettingsMenuView
    {

        [SerializeField] private TMP_Dropdown windowModeDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown refreshRateDropdown;
        [SerializeField] private Button saveSettingsButton;
        [SerializeField] private RectTransform panel;

        public TMP_Dropdown WindowModeDropdown => windowModeDropdown;
        public TMP_Dropdown ResolutionDropdown => resolutionDropdown;
        public TMP_Dropdown RefreshRateDropdown => refreshRateDropdown;
        public Button SaveSettingsButton => saveSettingsButton;
        public RectTransform Panel => panel;
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }

        protected override void PopulateFocusableElements()
        {
            
        }

    }
}