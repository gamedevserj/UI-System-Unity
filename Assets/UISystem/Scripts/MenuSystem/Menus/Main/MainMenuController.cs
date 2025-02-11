using System;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.ScreenFade;
using UnityEngine;

namespace UISystem.MenuSystem.Controllers
{
    internal class MainMenuController : MenuControllerBase<IViewCreator<MainMenuView>, MainMenuView>
    {

        public override MenuType Type => MenuType.Main;

        private readonly IPopupsManager<PopupType, PopupResult> _popupsManager;
        private readonly MenuBackgroundController _menuBackgroundController;
        private readonly ScreenFadeManager _screenFadeManager;

        public MainMenuController(IViewCreator<MainMenuView> viewCreator, IMenuModel model, IMenusManager<MenuType> menusManager,
             IPopupsManager<PopupType, PopupResult> popupsManager, ScreenFadeManager screenFadeManager, 
            MenuBackgroundController menuBackgroundController)
            : base(viewCreator, model, menusManager)
        {
            
            _popupsManager = popupsManager;
            _screenFadeManager = screenFadeManager;
            _menuBackgroundController = menuBackgroundController;
        }

        public override void Show(Action onComplete = null, bool instant = false)
        {
            base.Show(onComplete, instant);
            _menuBackgroundController.ShowBackground(instant);
        }

        public override void Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
        {
            base.Hide(stackingType, onComplete, instant);
            if (stackingType != StackingType.Add)
                _menuBackgroundController.HideBackground(instant);
        }

        protected override void SetupElements()
        {
            _view.PlayButton.AddListener(PressedPlay);
            _view.OptionsButton.AddListener(PressedOptions);
            _view.QuitButton.AddListener(PressedQuit);
        }

        public override void OnReturnButtonDown()
        {
            if (CanReturnToPreviousMenu)
                ShowQuitPopup();
        }

        private void PressedPlay()
        {
            _view.SetLastSelectedElement(_view.PlayButton.Button);
            _screenFadeManager.FadeOut(() =>
            {
                _menusManager.ShowMenu(MenuType.InGame, StackingType.Clear, instant: true);
            });
        }

        private void PressedOptions()
        {
            _view.SetLastSelectedElement(_view.OptionsButton.Button);
            _menusManager.ShowMenu(MenuType.Options);
        }

        private void PressedQuit()
        {
            _view.SetLastSelectedElement(_view.QuitButton.Button);
            ShowQuitPopup();
        }

        private void ShowQuitPopup()
        {
            SwitchInteractability(false);
            _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.QuitGame, (result) =>
            {
                if (result == PopupResult.Yes)
                    Application.Quit();
                else if (result == PopupResult.No)
                    SwitchInteractability(true);
            });
        }

    }
}