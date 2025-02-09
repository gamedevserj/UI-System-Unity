using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;

namespace UISystem.MenuSystem.Controllers
{
    internal class AudioSettingsMenuController : SettingsMenuController<IViewCreator<AudioSettingsMenuView>, AudioSettingsMenuView, AudioSettingsMenuModel>
    {

        public override MenuType Type => MenuType.AudioSettings;

        public AudioSettingsMenuController(IViewCreator<AudioSettingsMenuView> viewCreator, AudioSettingsMenuModel model,
            IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager) 
            : base(viewCreator, model, menusManager, popupsManager)
        { }

        protected override void SetupElements()
        {
            base.SetupElements();
            SetupMusicSlider();
            SetupSfxSlider();
            _view.SaveSettingsButton.AddListener(OnSaveSettingsButtonDown);
        }

        private void OnSaveSettingsButtonDown()
        {
            _model.SaveSettings();
            _view.SetLastSelectedElement(_view.SaveSettingsButton.Button);
        }

        private void SetupMusicSlider()
        {
            _view.MusicSlider.SetValue(_model.MusicVolume);
            _view.MusicSlider.AddListener(OnMusicSliderDragEnded);
        }

        private void OnMusicSliderDragEnded(float value)
        {
            _model.MusicVolume = value;
        }

        private void SetupSfxSlider()
        {
            _view.SfxSlider.SetValue(_model.SfxVolume);
            _view.SfxSlider.AddListener(OnSfxSliderDragEnded);
        }

        private void OnSfxSliderDragEnded(float value)
        {
            _model.SfxVolume = value;
        }

        protected override void ResetViewToDefault()
        {
            _view.MusicSlider.SetValue(_model.MusicVolume);
            _view.SfxSlider.SetValue(_model.SfxVolume);
            _view.SetLastSelectedElement(_view.ResetButton.Button);
        }

    }
}