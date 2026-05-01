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
    /// <summary>
    /// Video settings menu controller.
    /// </summary>
    internal class VideoSettingsMenuController : SettingsMenuController<IViewCreator<VideoSettingsMenuView>, VideoSettingsMenuView, VideoSettingsMenuModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoSettingsMenuController"/> class.
        /// </summary>
        /// <param name="viewCreator">View creator.</param>
        /// <param name="menusManager">Menus manager.</param>
        /// <param name="model">Video settings menu model.</param>
        /// <param name="popupsManager">Popups manager.</param>
        public VideoSettingsMenuController(
            IViewCreator<VideoSettingsMenuView> viewCreator,
            IMenusManager menusManager,
            VideoSettingsMenuModel model,
            IPopupsManager popupsManager)
            : base(viewCreator, menusManager, model, popupsManager)
        {
        }

        /// <inheritdoc/>
        protected override void SetupElements()
        {
            base.SetupElements();
            SetupWindowModeDropdown();
            SetupResolutionDropdown();
            SetupRefreshRateDropdown();
            View.SaveSettingsButton.AddOnClickListener(Model.SaveSettings);
        }

        /// <inheritdoc/>
        protected override void UpdateAllViewValues()
        {
            View.WindowModeDropdown.SetValue(Model.CurrenWindowModeIndex);
            View.ResolutionDropdown.SetValue(Model.CurrentResolutionIndex);
        }

        private void SetupWindowModeDropdown()
        {
            View.WindowModeDropdown.AddOptions(VideoSettings.FullScreenModeNames);
            View.WindowModeDropdown.SetValue(Model.CurrenWindowModeIndex);
            View.WindowModeDropdown.AddOnValueChangedListener(OnWindowModeDropdownSelect);
        }

        private void SetupResolutionDropdown()
        {
            View.ResolutionDropdown.AddOptions(VideoSettings.ResolutionNames);
            View.ResolutionDropdown.SetValue(Model.CurrentResolutionIndex);
            View.ResolutionDropdown.AddOnValueChangedListener(OnResolutionDropdownSelect);
        }

        private void SetupRefreshRateDropdown()
        {
            View.RefreshRateDropdown.AddOptions(VideoSettings.RefreshRateNames);
            View.RefreshRateDropdown.SetValue(Model.CurrentRefreshRate);
            View.RefreshRateDropdown.AddOnValueChangedListener(OnRefreshRateDropdownSelect);
        }

        private void OnResolutionDropdownSelect(int index)
        {
            Model.SelectResolution(index);
        }

        private void OnWindowModeDropdownSelect(int index)
        {
            Model.SelectWindowMode(index);
        }

        private void OnRefreshRateDropdownSelect(int index)
        {
            Model.SelectRefreshRate(index);
        }
    }
}
