using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
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
    /// <summary>
    /// Main menu controller.
    /// </summary>
    internal class MainMenuController : MenuControllerBase<IViewCreator<MainMenuView>, MainMenuView>
    {
        private readonly IPopupsManager<PopupResult> _popupsManager;
        private readonly MenuBackgroundController _menuBackgroundController;
        private readonly ScreenFadeManager _screenFadeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenuController"/> class.
        /// </summary>
        /// <param name="viewCreator">View creator.</param>
        /// <param name="menusManager">Menus manager.</param>
        /// <param name="popupsManager">Popups manager.</param>
        /// <param name="screenFadeManager">Screen fade manager.</param>
        /// <param name="menuBackgroundController">Background controller.</param>
        public MainMenuController(
            IViewCreator<MainMenuView> viewCreator,
            IMenusManager menusManager,
            IPopupsManager<PopupResult> popupsManager,
            ScreenFadeManager screenFadeManager,
            MenuBackgroundController menuBackgroundController)
            : base(viewCreator, menusManager)
        {
            _popupsManager = popupsManager;
            _screenFadeManager = screenFadeManager;
            _menuBackgroundController = menuBackgroundController;
        }

        /// <inheritdoc/>
        public override async Task Show(Action onComplete = null, bool instant = false)
        {
            _menuBackgroundController.ShowBackground(instant).SafeFireAndForget();
            await base.Show(onComplete, instant);
        }

        /// <inheritdoc/>
        public override async Task Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
        {
            if (stackingType != StackingType.Add)
                _menuBackgroundController.HideBackground(instant).SafeFireAndForget();
            await base.Hide(stackingType, onComplete, instant);
        }

        /// <inheritdoc/>
        public override void OnReturnButtonDown()
        {
            if (CanReturnToPreviousMenu)
                ShowQuitPopup();
        }

        /// <inheritdoc/>
        protected override void SetupElements()
        {
            View.PlayButton.AddOnClickListener(async () => { await PressedPlay(); });
            View.OptionsButton.AddOnClickListener(PressedOptions);
            View.QuitButton.AddOnClickListener(PressedQuit);
        }

        private async Task PressedPlay()
        {
            View.SetLastSelectedElement(View.PlayButton.Button);
            await _screenFadeManager.FadeOut();
            MenusManager.ShowMenu(typeof(InGameMenuView), StackingType.Clear, instant: true);
            await _screenFadeManager.FadeIn();
        }

        private void PressedOptions()
        {
            View.SetLastSelectedElement(View.OptionsButton.Button);
            MenusManager.ShowMenu(typeof(OptionsMenuView));
        }

        private void PressedQuit()
        {
            View.SetLastSelectedElement(View.QuitButton.Button);
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
