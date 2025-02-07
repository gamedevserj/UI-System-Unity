using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.Controllers
{
    internal class VideoSettingsMenuController : SettingsMenuController<IViewCreator<VideoSettingsMenuView>, VideoSettingsMenuView, VideoSettingsMenuModel>
    {

        public override MenuType Type => MenuType.VideoSettings;

        public VideoSettingsMenuController(IViewCreator<VideoSettingsMenuView> viewCreator, VideoSettingsMenuModel model,
            IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager) : base(viewCreator, model, menusManager, popupsManager)
        { }

        protected override void SetupElements()
        {
            base.SetupElements();
            SetupWindowModeDropdown();
            SetupResolutionDropdown();
            SetupRefreshRateDropdown();
            _view.SaveSettingsButton.onClick.AddListener(_model.SaveSettings);
        }

        private void SetupWindowModeDropdown()
        {
            _view.WindowModeDropdown.AddOptions(VideoSettings.FullScreenModeNames);
            _view.WindowModeDropdown.value = _model.CurrenWindowModeIndex;
            _view.WindowModeDropdown.onValueChanged.AddListener(OnWindowModeDropdownSelect);
        }

        private void SetupResolutionDropdown()
        {
            _view.ResolutionDropdown.AddOptions(VideoSettings.ResolutionNames);
            _view.ResolutionDropdown.value = _model.CurrentResolutionIndex;
            _view.ResolutionDropdown.onValueChanged.AddListener(OnResolutionDropdownSelect);
        }

        private void SetupRefreshRateDropdown()
        {
            _view.RefreshRateDropdown.AddOptions(VideoSettings.RefreshRateNames);
            _view.RefreshRateDropdown.value = _model.CurrentRefreshRate;
            _view.RefreshRateDropdown.onValueChanged.AddListener(OnRefreshRateDropdownSelect);
        }

        private void OnResolutionDropdownSelect(int index)
        {
            _model.SelectResolution(index);
        }

        private void OnWindowModeDropdownSelect(int index)
        {
            _model.SelectWindowMode(index);
        }

        private void OnRefreshRateDropdownSelect(int index)
        {
            _model.SelectRefreshRate(index);
        }

        protected override void ResetViewToDefault()
        {
            _view.WindowModeDropdown.value = _model.CurrenWindowModeIndex;
            _view.ResolutionDropdown.value = _model.CurrentResolutionIndex;
        }
    }
}