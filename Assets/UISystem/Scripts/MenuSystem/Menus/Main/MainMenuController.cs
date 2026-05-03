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
    internal class MainMenuController : MenuController<IViewCreator<MainMenuView>, MainMenuView>
    {
        private readonly IPopupsManager _popupsManager;
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
            IPopupsManager popupsManager,
            ScreenFadeManager screenFadeManager,
            MenuBackgroundController menuBackgroundController)
            : base(viewCreator, menusManager)
        {
            _popupsManager = popupsManager;
            _screenFadeManager = screenFadeManager;
            _menuBackgroundController = menuBackgroundController;
        }

        /// <inheritdoc/>
        public override async Task Show(bool instant = false)
        {
            _menuBackgroundController.ShowBackground(instant).SafeFireAndForget();
            await base.Show(instant);
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
            View.SetLastSelectedElement(View.PlayButton);
            await _screenFadeManager.FadeOut();
            await MenusManager.ShowMenu<InGameMenuView>(StackingType.Clear, instant: true);
            await _screenFadeManager.FadeIn();
        }

        private void PressedOptions()
        {
            View.SetLastSelectedElement(View.OptionsButton);
            MenusManager.ShowMenu<OptionsMenuView>().SafeFireAndForget();
        }

        private void PressedQuit()
        {
            View.SetLastSelectedElement(View.QuitButton);
            ShowQuitPopup();
        }

        private void ShowQuitPopup()
        {
            SwitchInteractability(false);
            _popupsManager
                .ShowPopup<YesNoPopupView>(PopupMessages.QuitGame, (result) =>
                {
                    if (result == PopupResult.Yes)
                        Application.Quit();
                    else if (result == PopupResult.No)
                        SwitchInteractability(true);
                })
                .SafeFireAndForget();
        }
    }
}
