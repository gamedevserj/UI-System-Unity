using System;
using System.Threading.Tasks;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;
using UnityEngine;

namespace UISystem.MenuSystem.Controllers
{
    internal class MainMenuController : MenuControllerBase<IViewCreator<MainMenuView>, MainMenuView>
    {

        private readonly IPopupsManager<PopupResult> _popupsManager;
        private readonly MenuBackgroundController _menuBackgroundController;
        private readonly ScreenFadeManager _screenFadeManager;

        public MainMenuController(IViewCreator<MainMenuView> viewCreator, IMenusManager menusManager,
             IPopupsManager<PopupResult> popupsManager, ScreenFadeManager screenFadeManager, 
            MenuBackgroundController menuBackgroundController)
            : base(viewCreator, menusManager)
        {
            
            _popupsManager = popupsManager;
            _screenFadeManager = screenFadeManager;
            _menuBackgroundController = menuBackgroundController;
        }

        public override async Task Show(Action onComplete = null, bool instant = false)
        {
            _menuBackgroundController.ShowBackground(instant);
            await base.Show(onComplete, instant);
        }

        public override async Task Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
        {
            if (stackingType != StackingType.Add)
                _menuBackgroundController.HideBackground(instant);
            await base.Hide(stackingType, onComplete, instant);
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
                _menusManager.ShowMenu(typeof(InGameMenuView), StackingType.Clear, instant: true);
            });
        }

        private void PressedOptions()
        {
            _view.SetLastSelectedElement(_view.OptionsButton.Button);
            _menusManager.ShowMenu(typeof(OptionsMenuView));
        }

        private void PressedQuit()
        {
            _view.SetLastSelectedElement(_view.QuitButton.Button);
            ShowQuitPopup();
        }

        private void ShowQuitPopup()
        {
            SwitchInteractability(false);
            _popupsManager.ShowPopup(typeof(YesNoPopupView), PopupMessages.QuitGame, (result) =>
            {
                if (result == PopupResult.Yes)
                    Application.Quit();
                else if (result == PopupResult.No)
                    SwitchInteractability(true);
            });
        }
    }
}