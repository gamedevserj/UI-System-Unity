using System;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UnityEngine;

namespace UISystem.MenuSystem.Models
{
    public class VideoSettingsMenuModel : ISettingsMenuModel
    {

        private Vector2Int _tempResolution;
        private FullScreenMode _tempWindowMode;
        private int _tempRefreshRate = Screen.currentResolution.refreshRate;
        private readonly GameSettings _settings;

        public int CurrentResolutionIndex => Array.IndexOf(AvailableResolutions, TempResolution);
        public int CurrentRefreshRate => Array.IndexOf(AvailableRefreshRates, _tempRefreshRate);
        public int CurrenWindowModeIndex => Array.IndexOf(VideoSettings.FullScreenModes, _tempWindowMode);

        private Vector2Int TempResolution
        {
            get
            {
                // updating resolution in case player resized window manually to allow saving custom resolution in windowed mode
                _tempResolution.x = Screen.width;
                _tempResolution.y = Screen.height;
                return _tempResolution;
            }
        }

        public bool HasUnappliedSettings => !GameSettings.Resolution.Equals(TempResolution) || GameSettings.WindowMode != _tempWindowMode
            || GameSettings.RefreshRate != _tempRefreshRate;

        private static Vector2Int[] AvailableResolutions => VideoSettings.AvailableResolutions;
        private static int[] AvailableRefreshRates => VideoSettings.AvailableRefreshRates;

        public VideoSettingsMenuModel(GameSettings settings)
        {
            _settings = settings;
            LoadSettings();
        }

        public void SelectWindowMode(int index)
        {
            _tempWindowMode = VideoSettings.FullScreenModes[index];
            Screen.fullScreenMode = _tempWindowMode;
        }

        public void SelectResolution(int index)
        {
            _tempResolution = AvailableResolutions[index];
            Screen.SetResolution(_tempResolution.x, _tempResolution.y, _tempWindowMode, _tempRefreshRate);
        }

        public void SelectRefreshRate(int index)
        {
            _tempRefreshRate = AvailableRefreshRates[index];
            Screen.SetResolution(_tempResolution.x, _tempResolution.y, _tempWindowMode, _tempRefreshRate);
        }

        public void SaveSettings()
        {
            _settings.SaveResolution(_tempResolution);
            _settings.SaveWindowMode(_tempWindowMode);
        }

        public void DiscardChanges()
        {
            LoadSettings();
        }

        public void ResetToDefault()
        {
            _tempResolution = ConfigData.DefaultResolution;
            _tempWindowMode = ConfigData.DefaultFullScreenMode;
            _tempRefreshRate = ConfigData.DefaultRefreshRate;
            SaveSettings();
        }

        private void LoadSettings()
        {
            _tempResolution = GameSettings.Resolution;
            _tempWindowMode = GameSettings.WindowMode;
            _tempRefreshRate = GameSettings.RefreshRate;
            Screen.SetResolution(_tempResolution.x, _tempResolution.y, _tempWindowMode, _tempRefreshRate);
        }
    }
}