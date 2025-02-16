using System;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UnityEngine;

namespace UISystem.MenuSystem.Models
{
    public class VideoSettingsMenuModel : ISettingsMenuModel
    {

        private Vector2Int _lastResolution;
        private FullScreenMode _lastWindowMode;
        private int _lastRefreshRate;

        private readonly GameSettings _settings;

        public int CurrentResolutionIndex => Array.IndexOf(AvailableResolutions, CurrentWindowSize);
        public int CurrentRefreshRate => Array.IndexOf(AvailableRefreshRates, _settings.RefreshRate);
        public int CurrenWindowModeIndex => Array.IndexOf(VideoSettings.FullScreenModes, _settings.WindowMode);

        public bool HasUnappliedSettings => !_settings.Resolution.Equals(CurrentWindowSize) || _settings.WindowMode != _lastWindowMode
            || _settings.RefreshRate != _lastRefreshRate;

        private Vector2Int CurrentWindowSize
        {
            get
            {
                // updating resolution in case player resized window manually to allow saving custom resolution in windowed mode
                _lastResolution.x = Screen.width;
                _lastResolution.y = Screen.height;
                return _lastResolution;
            }
        }

        private static Vector2Int[] AvailableResolutions => VideoSettings.AvailableResolutions;
        private static int[] AvailableRefreshRates => VideoSettings.AvailableRefreshRates;

        public VideoSettingsMenuModel(GameSettings settings)
        {
            _settings = settings;
            SetVideoParameters();
            RememberLastSavedSettings();
        }

        public void SelectWindowMode(int index)
        {
            Screen.fullScreenMode = _settings.WindowMode = VideoSettings.FullScreenModes[index];
        }

        public void SelectResolution(int index)
        {
            _settings.Resolution = AvailableResolutions[index];
            Screen.SetResolution(_settings.Resolution.x, _settings.Resolution.y, _settings.WindowMode, _settings.RefreshRate);
        }

        public void SelectRefreshRate(int index)
        {
            _settings.RefreshRate = AvailableRefreshRates[index];
            Screen.SetResolution(_settings.Resolution.x, _settings.Resolution.y, _settings.WindowMode, _settings.RefreshRate);
        }

        public void SaveSettings()
        {
            if (_settings.WindowMode == FullScreenMode.Windowed)
                _settings.Resolution = CurrentWindowSize;

            RememberLastSavedSettings();
            _settings.SaveVideoSettings();
        }

        public void DiscardChanges()
        {
            _settings.Resolution = _lastResolution;
            _settings.WindowMode = _lastWindowMode;
            _settings.RefreshRate = _lastRefreshRate;
            SetVideoParameters();
        }

        public void ResetToDefault()
        {
            _settings.Resolution = ConfigData.DefaultResolution;
            _settings.WindowMode = ConfigData.DefaultFullScreenMode;
            _settings.RefreshRate = ConfigData.DefaultRefreshRate;
            SetVideoParameters();
            SaveSettings();
        }

        private void SetVideoParameters()
        {
            Screen.SetResolution(_settings.Resolution.x, _settings.Resolution.y, _settings.WindowMode, _settings.RefreshRate);
        }

        private void RememberLastSavedSettings()
        {
            _lastResolution = _settings.Resolution;
            _lastWindowMode = _settings.WindowMode;
            _lastRefreshRate = _settings.RefreshRate;
        }
    }
}