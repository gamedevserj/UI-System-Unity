﻿using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem.MenuSystem.Controllers
{
    internal class AudioSettingsMenuController : SettingsMenuController<IViewCreator<AudioSettingsMenuView>, AudioSettingsMenuView, AudioSettingsMenuModel>
    {

        public override MenuType Type => MenuType.AudioSettings;

        public AudioSettingsMenuController(IViewCreator<AudioSettingsMenuView> viewCreator, AudioSettingsMenuModel model,
            IMenusManager<KeyCode, MenuType> menusManager, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager) 
            : base(viewCreator, model, menusManager, popupsManager)
        { }

        protected override void SetupElements()
        {
            base.SetupElements();
            SetupMusicSlider();
            SetupSfxSlider();
            _view.SaveSettingsButton.onClick.AddListener(OnSaveSettingsButtonDown);
        }

        private void OnSaveSettingsButtonDown()
        {
            _model.SaveSettings();
            _view.SetLastSelectedElement(_view.SaveSettingsButton);
        }

        private void SetupMusicSlider()
        {
            _view.MusicSlider.value = _model.MusicVolume;
            _view.MusicSlider.onValueChanged.AddListener(OnMusicSliderDragEnded);
            //_view.MusicSlider.SetValueNoSignal(_model.MusicVolume);
            //_view.MusicSlider.DragEnded += OnMusicSliderDragEnded;
            //_view.MusicSlider.DragStarted += OnMusicSliderDragStarted;
        }

        private void OnMusicSliderDragEnded(float value)
        {
            _model.MusicVolume = value;
        }

        //private void OnMusicSliderDragStarted()
        //{
        //    _model.MusicVolume = _view.MusicSlider.value;
        //    _view.SetLastSelectedElement(_view.MusicSlider);
        //}

        private void SetupSfxSlider()
        {
            _view.SfxSlider.value = _model.SfxVolume;
            _view.SfxSlider.onValueChanged.AddListener(OnSfxSliderDragEnded);
            //_view.SfxSlider.SetValueNoSignal(_model.SfxVolume);
            //_view.SfxSlider.DragEnded += OnSfxSliderDragEnded;
            //_view.SfxSlider.DragStarted += OnSfxSliderDragStarted;
        }

        private void OnSfxSliderDragEnded(float value)
        {
            _model.SfxVolume = value;
        }

        //private void OnSfxSliderDragStarted()
        //{
        //    _model.SfxVolume = (float)_view.SfxSlider.Value;
        //    _view.SetLastSelectedElement(_view.SfxSlider);
        //}

        protected override void ResetViewToDefault()
        {
            //_view.MusicSlider.SetValue(_model.MusicVolume);
            //_view.SfxSlider.SetValue(_model.SfxVolume);
            _view.SetLastSelectedElement(_view.ResetButton);
        }

    }
}