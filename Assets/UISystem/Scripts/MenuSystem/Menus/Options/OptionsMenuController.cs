using AsyncAwaitBestPractices;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers
{
    /// <summary>
    /// Options menu controller.
    /// </summary>
    internal class OptionsMenuController : MenuControllerBase<IViewCreator<OptionsMenuView>, OptionsMenuView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMenuController"/> class.
        /// </summary>
        /// <param name="viewCreator">View creator.</param>
        /// <param name="menusManager">Menus manager.</param>
        public OptionsMenuController(IViewCreator<OptionsMenuView> viewCreator, IMenusManager menusManager)
            : base(viewCreator, menusManager)
        {
        }

        /// <inheritdoc/>
        protected override void SetupElements()
        {
            View.ReturnButton.AddOnClickListener(OnReturnButtonDown);
            View.AudioSettingsButton.AddOnClickListener(OnAudioSettingsButtonDown);
            View.VideoSettingsButton.AddOnClickListener(OnVideoSettingsButtonDown);
            View.RebindKeysButton.AddOnClickListener(OnRebindKeysButtonDown);
            View.InterfaceSettingsButton.AddOnClickListener(OnInterfaceSettingsButtonDown);
        }

        private void OnAudioSettingsButtonDown()
        {
            View.SetLastSelectedElement(View.AudioSettingsButton.Button);
            MenusManager.ShowMenu(typeof(AudioSettingsMenuView)).SafeFireAndForget();
        }

        private void OnVideoSettingsButtonDown()
        {
            View.SetLastSelectedElement(View.VideoSettingsButton.Button);
            MenusManager.ShowMenu(typeof(VideoSettingsMenuView)).SafeFireAndForget();
        }

        private void OnRebindKeysButtonDown()
        {
            View.SetLastSelectedElement(View.RebindKeysButton.Button);
            MenusManager.ShowMenu(typeof(RebindKeysMenuView)).SafeFireAndForget();
        }

        private void OnInterfaceSettingsButtonDown()
        {
            View.SetLastSelectedElement(View.InterfaceSettingsButton.Button);
            MenusManager.ShowMenu(typeof(InterfaceSettingsMenuView)).SafeFireAndForget();
        }
    }
}
