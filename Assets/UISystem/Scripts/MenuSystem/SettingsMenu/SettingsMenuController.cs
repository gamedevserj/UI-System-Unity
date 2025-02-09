using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem;
using UnityEngine.UI;

namespace UISystem.MenuSystem.SettingsMenu
{
    internal abstract class SettingsMenuController<TViewCreator, TView, TModel>
        : MenuController<TViewCreator, TView, TModel, Selectable, MenuType>
        where TViewCreator : IViewCreator<TView>
        where TView : SettingsMenuView
        where TModel : ISettingsMenuModel
    {

        protected readonly IPopupsManager<PopupType, PopupResult> _popupsManager;

        protected SettingsMenuController(TViewCreator viewCreator, TModel model, IMenusManager<MenuType> menusManager,
            IPopupsManager<PopupType, PopupResult> popupsManager) 
            : base(viewCreator, model, menusManager)
        {
            _popupsManager = popupsManager;
        }

        protected abstract void ResetViewToDefault();

        protected override void SetupElements()
        {
            _view.ReturnButton.Button.onClick.AddListener(OnReturnButtonDown);
            _view.ResetButton.Button.onClick.AddListener(OnResetToDefaultButtonDown);
        }

        public override void OnReturnButtonDown()
        {
            if (_model.HasUnappliedSettings)
            {
                _view.SetLastSelectedElement(_view.ReturnButton.Button);
                CanReceivePhysicalInput = false;
                SwitchInteractability(false);
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
                    SwitchInteractability(true);
                    break;
                default:
                    SwitchInteractability(true);
                    break;
            }
        }

        protected virtual void OnResetToDefaultButtonDown()
        {
            _view.SetLastSelectedElement(_view.ResetButton.Button);
            SwitchInteractability(false);
            _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.ResetToDefault, (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    _model.ResetToDefault();
                    ResetViewToDefault();
                }
                SwitchInteractability(true);
            });
        }
    }
}