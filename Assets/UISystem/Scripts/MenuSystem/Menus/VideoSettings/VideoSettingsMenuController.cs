using System.Linq;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UnityEngine;

namespace UISystem.MenuSystem.Controllers
{
    internal class VideoSettingsMenuController : SettingsMenuController<IViewCreator<VideoSettingsMenuView>, VideoSettingsMenuView, VideoSettingsMenuModel>
    {

        public override MenuType Type => MenuType.VideoSettings;

        public VideoSettingsMenuController(IViewCreator<VideoSettingsMenuView> viewCreator, VideoSettingsMenuModel model,
            IMenusManager<KeyCode, MenuType> menusManager, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager) : base(viewCreator, model, menusManager, popupsManager)
        { }

        protected override void SetupElements()
        {
            base.SetupElements();
            SetupWindowModeDropdown();
            SetupResolutionDropdown();
            _view.SaveSettingsButton.onClick.AddListener(_model.SaveSettings);
        }

        private void SetupWindowModeDropdown()
        {
            var windowModeNames = _model.GetWindowModeOptionNames();
            //OptionButtonItem[] items = new OptionButtonItem[windowModeNames.Length];
            //for (int i = 0; i < items.Length; i++)
            //{
            //    var name = Regex.Replace(windowModeNames[i].ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
            //    items[i] = new OptionButtonItem(name, i);
            //}
            //_view.WindowModeDropdown.AddMultipleItems(items);
            //_view.WindowModeDropdown.Select(_model.CurrenWindowModeIndex);
            //_view.WindowModeDropdown.ItemSelected += OnWindowModeDropdownSelect;
        }

        private void SetupResolutionDropdown()
        {
            var resolutionNames = _model.GetAvailableResolutionNames().ToList();
            //OptionButtonItem[] items = new OptionButtonItem[resolutionNames.Length];
            //for (int i = 0; i < items.Length; i++)
            //{
            //    items[i] = new OptionButtonItem(resolutionNames[i], i);
            //}

            //_view.ResolutionDropdown.AddMultipleItems(items);
            //_view.ResolutionDropdown.Select(_model.CurrentResolutionIndex);
            //_view.ResolutionDropdown.ItemSelected += OnResolutionDropdownSelect;

            _view.ResolutionDropdown.AddOptions(_model.GetAvailableResolutionNames().ToList());
            _view.ResolutionDropdown.onValueChanged.AddListener(OnResolutionDropdownSelect);
            _view.ResolutionDropdown.value = (int)_model.CurrentResolutionIndex;
        }

        private void OnResolutionDropdownSelect(int index)
        {
            _model.SelectResolution(index);
        }

        private void OnWindowModeDropdownSelect(long index)
        {
            _model.SelectWindowMode((int)index);
        }

        protected override void ResetViewToDefault()
        {
            //_view.WindowModeDropdown.Select(_model.CurrenWindowModeIndex);
            //_view.ResolutionDropdown.Select(_model.CurrentResolutionIndex);
        }
    }
}