using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Views;
using UnityEngine.UI;

namespace UISystem.MenuSystem.SettingsMenu
{
    /// <summary>
    /// Settings menu controller.
    /// </summary>
    /// <typeparam name="TViewCreator">Type of view creator.</typeparam>
    /// <typeparam name="TView">Type of view. Must be of type <see cref="SettingsMenuView"/>.</typeparam>
    /// <typeparam name="TModel">Type of model. Must implement <see cref="ISettingsMenuModel"/>.</typeparam>
    internal abstract class SettingsMenuController<TViewCreator, TView, TModel>
        : MenuController<TViewCreator, TView, Selectable>
        where TViewCreator : IViewCreator<TView>
        where TView : SettingsMenuView
        where TModel : ISettingsMenuModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsMenuController{TViewCreator, TView, TModel}"/> class.
        /// </summary>
        /// <param name="viewCreator">View creator.</param>
        /// <param name="menusManager">Menus manager.</param>
        /// <param name="model">Menu model.</param>
        /// <param name="popupsManager">Popups manager.</param>
        protected SettingsMenuController(
            TViewCreator viewCreator,
            IMenusManager menusManager,
            TModel model,
            IPopupsManager<PopupResult> popupsManager)
            : base(viewCreator, menusManager)
        {
            Model = model;
            PopupsManager = popupsManager;
        }

        /// <summary>
        /// Gets the menu model.
        /// </summary>
        protected TModel Model { get; private set; }

        /// <summary>
        /// Gets the popups manager.
        /// </summary>
        protected IPopupsManager<PopupResult> PopupsManager { get; private set; }

        /// <inheritdoc/>
        public override void OnReturnButtonDown()
        {
            if (Model.HasUnappliedSettings)
            {
                View.SetLastSelectedElement(View.ReturnButton.Button);
                CanReceivePhysicalInput = false;
                SwitchInteractability(false);
                PopupsManager.ShowPopup(typeof(YesNoCancelPopupView), PopupMessages.SaveChanges, (result) =>
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

        /// <summary>
        /// Updates all view values.
        /// </summary>
        protected abstract void UpdateAllViewValues();

        /// <inheritdoc/>
        protected override void SetupElements()
        {
            View.ReturnButton.AddOnClickListener(OnReturnButtonDown);
            View.ResetButton.AddOnClickListener(OnResetToDefaultButtonDown);
        }

        /// <summary>
        /// Action to perform when popup confirming leaving menu is hidden.
        /// </summary>
        /// <param name="result">Popup result.</param>
        protected void OnReturnToPreviousMenuPopupClosed(PopupResult result)
        {
            switch (result)
            {
                case PopupResult.No:
                    Model.DiscardChanges();
                    base.OnReturnButtonDown();
                    break;
                case PopupResult.Yes:
                    Model.SaveSettings();
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

        /// <summary>
        /// Shows popup to confirm resetting to default.
        /// </summary>
        protected virtual void OnResetToDefaultButtonDown()
        {
            View.SetLastSelectedElement(View.ResetButton.Button);
            SwitchInteractability(false);
            PopupsManager.ShowPopup(typeof(YesNoPopupView), PopupMessages.ResetToDefault, (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    Model.ResetToDefault();
                    UpdateAllViewValues();
                }

                SwitchInteractability(true);
            });
        }
    }
}
