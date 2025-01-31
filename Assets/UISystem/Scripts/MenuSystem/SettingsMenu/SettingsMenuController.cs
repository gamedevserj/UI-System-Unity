using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.SettingsMenu
{
    internal abstract class SettingsMenuController<TViewCreator, TView, TModel>
        : MenuController<TViewCreator, TView, TModel, KeyCode, Selectable, MenuType>
        where TViewCreator : IViewCreator<TView>
        where TView : SettingsMenuView
        where TModel : ISettingsMenuModel
    {

        protected readonly IPopupsManager<KeyCode, PopupType, PopupResult> _popupsManager;

        protected SettingsMenuController(TViewCreator viewCreator, TModel model, IMenusManager<KeyCode, MenuType> menusManager,
            IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager) 
            : base(viewCreator, model, menusManager)
        {
            _popupsManager = popupsManager;
        }

        protected abstract void ResetViewToDefault();

        protected override void SetupElements()
        {
            _view.ReturnButton.onClick.AddListener(OnReturnButtonDown);
            _view.ResetButton.onClick.AddListener(OnResetToDefaultButtonDown);
        }

        public override void OnReturnButtonDown()
        {
            if (_model.HasUnappliedSettings)
            {
                _view.SetLastSelectedElement(_view.ReturnButton);
                CanReceivePhysicalInput = false;
                SwitchFocusAvailability(false);
                _popupsManager.ShowPopup(PopupType.YesNoCancel, PopupMessages.SaveChanges, (result) =>
                {
                    OnReturnToPreviousMenuPopupClosed(result);
                    CanReceivePhysicalInput = true;
                });
            }
            else
            {
                base.OnReturnButtonDown();
            }
        }

        protected void OnReturnToPreviousMenuPopupClosed(PopupResult result)
        {
            switch (result)
            {
                case PopupResult.No:
                    _model.DiscardChanges();
                    base.OnReturnButtonDown();
                    break;
                case PopupResult.Yes:
                    _model.SaveSettings();
                    base.OnReturnButtonDown();
                    break;
                case PopupResult.Cancel:
                    SwitchFocusAvailability(true);
                    break;
                default:
                    SwitchFocusAvailability(true);
                    break;
            }
        }

        protected virtual void OnResetToDefaultButtonDown()
        {
            _view.SetLastSelectedElement(_view.ResetButton);
            SwitchFocusAvailability(false);
            _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.ResetToDefault, (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    _model.ResetToDefault();
                    ResetViewToDefault();
                }
                SwitchFocusAvailability(true);
            });
        }
    }
}