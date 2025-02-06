using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UnityEngine;

namespace UISystem.MenuSystem.Controllers
{
    internal class PauseMenuController : MenuControllerBase<IViewCreator<PauseMenuView>, PauseMenuView>
    {

        public override MenuType Type => MenuType.Pause;

        private readonly IPopupsManager<KeyCode, PopupType, PopupResult> _popupsManager;
        //private readonly ScreenFadeManager _screenFadeManager;
        //private readonly MenuBackgroundController _menuBackgroundController;

        public PauseMenuController(IViewCreator<PauseMenuView> viewCreator, IMenuModel model, IMenusManager<KeyCode, MenuType> menusManager,
            IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager)//, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController)
            : base(viewCreator, model, menusManager)
        {
            _popupsManager = popupsManager;
            //_screenFadeManager = screenFadeManager;
            //_menuBackgroundController = menuBackgroundController;
        }

        //public override void Show(Action onComplete = null, bool instant = false)
        //{
        //    base.Show(onComplete, instant);
        //    _menuBackgroundController.ShowBackground(instant);
        //}

        //public override void Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
        //{
        //    base.Hide(stackingType, () =>
        //    {
        //        if (stackingType != StackingType.Add)
        //            _menuBackgroundController.HideBackground(instant);

        //        onComplete?.Invoke();
        //    }, instant);
        //}

        protected override void SetupElements()
        {
            _view.ResumeGameButton.onClick.AddListener(OnReturnButtonDown);
            _view.OptionsButton.onClick.AddListener(PressedOptions);
            _view.ReturnToMainMenuButton.onClick.AddListener(PressedReturn);
        }

        private void PressedOptions()
        {
            _view.SetLastSelectedElement(_view.OptionsButton);
            _menusManager.ShowMenu(MenuType.Options);
        }

        private void PressedReturn()
        {
            _view.SetLastSelectedElement(_view.ReturnToMainMenuButton);
            SwitchFocusAvailability(false);

            _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.QuitToMainMenu, (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    _menusManager.ShowMenu(MenuType.Main, StackingType.Clear, null, true);
                    //_screenFadeManager.FadeOut(() =>
                    //{
                    //    _menusManager.ShowMenu(MenuType.Main, StackingType.Clear, null, true);
                    //});
                }
                else if (result == PopupResult.No)
                {
                    SwitchFocusAvailability(true);
                }
            });
        }

    }
}