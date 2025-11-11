using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers
{
    internal class OptionsMenuController : MenuControllerBase<IViewCreator<OptionsMenuView>, OptionsMenuView>
    {
        public OptionsMenuController(IViewCreator<OptionsMenuView> viewCreator, IMenuModel model, IMenusManager menusManager) : base(viewCreator, model, menusManager)
        { }

        protected override void SetupElements()
        {
            _view.ReturnButton.AddListener(OnReturnButtonDown);
            _view.AudioSettingsButton.AddListener(OnAudioSettingsButtonDown);
            _view.VideoSettingsButton.AddListener(OnVideoSettingsButtonDown);
            _view.RebindKeysButton.AddListener(OnRebindKeysButtonDown);
            _view.InterfaceSettingsButton.AddListener(OnInterfaceSettingsButtonDown);
        }

        private void OnAudioSettingsButtonDown()
        {
            _view.SetLastSelectedElement(_view.AudioSettingsButton.Button);
            _menusManager.ShowMenu(typeof(AudioSettingsMenuView));
        }

        private void OnVideoSettingsButtonDown()
        {
            _view.SetLastSelectedElement(_view.VideoSettingsButton.Button);
            _menusManager.ShowMenu(typeof(VideoSettingsMenuView));
        }

        private void OnRebindKeysButtonDown()
        {
            _view.SetLastSelectedElement(_view.RebindKeysButton.Button);
            _menusManager.ShowMenu(typeof(RebindKeysMenuView));
        }

        private void OnInterfaceSettingsButtonDown()
        {
            _view.SetLastSelectedElement(_view.InterfaceSettingsButton.Button);
            _menusManager.ShowMenu(typeof(InterfaceSettingsMenuView));
        }
    }
}