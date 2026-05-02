using System;
using System.Collections.Generic;
using UISystem.Common.Enums;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers
{
    /// <summary>
    /// Interface settings menu controller.
    /// </summary>
    internal class InterfaceSettingsMenuController : SettingsMenuController<IViewCreator<InterfaceSettingsMenuView>, InterfaceSettingsMenuView, InterfaceSettingsMenuModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceSettingsMenuController"/> class.
        /// </summary>
        /// <param name="viewCreator">View creator.</param>
        /// <param name="menusManager">Menus manager.</param>
        /// <param name="model">Interface settings menu model.</param>
        /// <param name="popupsManager">Popups manager.</param>
        public InterfaceSettingsMenuController(
            IViewCreator<InterfaceSettingsMenuView> viewCreator,
            IMenusManager menusManager,
            InterfaceSettingsMenuModel model,
            IPopupsManager popupsManager)
            : base(viewCreator, menusManager, model, popupsManager)
        {
        }

        /// <inheritdoc/>
        protected override void SetupElements()
        {
            SetupControllerIconsDropdown();
            base.SetupElements();
            View.SaveSettingsButton.AddOnClickListener(OnSaveSettingsButtonDown);
        }

        /// <inheritdoc/>
        protected override void UpdateAllViewValues()
        {
            View.ControllerIconsDropdown.SetValue((int)Model.ControllerIconsType);
        }

        private void OnSaveSettingsButtonDown()
        {
            Model.SaveSettings();
            View.SetLastSelectedElement(View.SaveSettingsButton);
        }

        private void SetupControllerIconsDropdown()
        {
            View.ControllerIconsDropdown.Dropdown.ClearOptions();
            var options = new List<string>();
            foreach (var item in Enum.GetValues(typeof(ControllerIconsType)))
            {
                options.Add(item.ToString());
            }

            View.ControllerIconsDropdown.AddOptions(options);
            View.ControllerIconsDropdown.SetValue((int)Model.ControllerIconsType);
            View.ControllerIconsDropdown.AddOnValueChangedListener(SelectControllerIconsType);
        }

        private void SelectControllerIconsType(int index)
        {
            Model.SelectIconType(index);
            View.SetLastSelectedElement(View.ControllerIconsDropdown);
        }
    }
}
