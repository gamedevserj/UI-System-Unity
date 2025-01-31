using System;
using System.Collections.Generic;
using UISystem.Common.Enums;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UnityEngine;

namespace UISystem.MenuSystem.Controllers
{
    internal class InterfaceSettingsMenuController : SettingsMenuController<IViewCreator<InterfaceSettingsMenuView>, InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
    {

        private readonly int _controllerIconsNumber;
        public override MenuType Type => MenuType.InterfaceSettings;

        public InterfaceSettingsMenuController(IViewCreator<InterfaceSettingsMenuView> viewCreator, InterfaceSettingsMenuModel model,
            IMenusManager<KeyCode, MenuType> menusManager)//, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager)
            : base(viewCreator, model, menusManager)//, popupsManager)
        {
            _controllerIconsNumber = Enum.GetNames(typeof(ControllerIconsType)).Length;
        }

        protected override void SetupElements()
        {
            SetupControllerIconsDropdown();
            base.SetupElements();
            _view.SaveSettingsButton.onClick.AddListener(OnSaveSettingsButtonDown);
        }

        private void OnSaveSettingsButtonDown()
        {
            _model.SaveSettings();
            _view.SetLastSelectedElement(_view.SaveSettingsButton);
        }

        private void SetupControllerIconsDropdown()
        {
            _view.ControllerIconsDropdown.ClearOptions();
            var options = new List<string>();
            foreach (var item in Enum.GetValues(typeof(ControllerIconsType)))
            {
                options.Add(item.ToString());
            }
            
            _view.ControllerIconsDropdown.AddOptions(options);
            _view.ControllerIconsDropdown.onValueChanged.AddListener(SelectControllerIconsType);
            _view.ControllerIconsDropdown.value = (int)_model.ControllerIconsType;
        }

        private void SelectControllerIconsType(int index)
        {
            _model.SelectIconType(index);
            _view.SetLastSelectedElement(_view.ControllerIconsDropdown);
        }

        protected override void ResetViewToDefault()
        {
            _view.ControllerIconsDropdown.value = (int)_model.ControllerIconsType;
        }

    }
}