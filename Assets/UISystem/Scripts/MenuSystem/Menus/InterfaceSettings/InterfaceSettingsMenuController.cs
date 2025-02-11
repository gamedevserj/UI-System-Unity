using System;
using System.Collections.Generic;
using UISystem.Common.Enums;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.Controllers
{
    internal class InterfaceSettingsMenuController : SettingsMenuController<IViewCreator<InterfaceSettingsMenuView>, InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
    {

        public override MenuType Type => MenuType.InterfaceSettings;

        public InterfaceSettingsMenuController(IViewCreator<InterfaceSettingsMenuView> viewCreator, InterfaceSettingsMenuModel model,
            IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager)
            : base(viewCreator, model, menusManager, popupsManager)
        { }

        protected override void SetupElements()
        {
            SetupControllerIconsDropdown();
            base.SetupElements();
            _view.SaveSettingsButton.AddListener(OnSaveSettingsButtonDown);
        }

        private void OnSaveSettingsButtonDown()
        {
            _model.SaveSettings();
            _view.SetLastSelectedElement(_view.SaveSettingsButton.Button);
        }

        private void SetupControllerIconsDropdown()
        {
            _view.ControllerIconsDropdown.Dropdown.ClearOptions();
            var options = new List<string>();
            foreach (var item in Enum.GetValues(typeof(ControllerIconsType)))
            {
                options.Add(item.ToString());
            }
            
            _view.ControllerIconsDropdown.AddOptions(options);
            _view.ControllerIconsDropdown.SetValue((int)_model.ControllerIconsType);
            _view.ControllerIconsDropdown.AddListener(SelectControllerIconsType);
        }

        private void SelectControllerIconsType(int index)
        {
            _model.SelectIconType(index);
            _view.SetLastSelectedElement(_view.ControllerIconsDropdown.Dropdown);
        }

        protected override void ResetViewToDefault()
        {
            _view.ControllerIconsDropdown.SetValue((int)_model.ControllerIconsType);
        }

    }
}