using System;
using System.Collections.Generic;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Extensions;
using UnityEngine;

namespace UISystem.MenuSystem.Models
{
    /// <summary>
    /// Video settings menu model.
    /// </summary>
    public class VideoSettingsMenuModel : ISettingsMenuModel
    {
        private readonly GameSettings _settings;

        private Vector2Int _lastResolution;
        private FullScreenMode _lastWindowMode;
        private int _lastRefreshRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoSettingsMenuModel"/> class.
        /// </summary>
        /// <param name="settings">Game settings.</param>
        public VideoSettingsMenuModel(GameSettings settings)
        {
            _settings = settings;
            SetVideoParameters();
            RememberLastSavedSettings();
        }

        /// <summary>
        /// Gets current resolution index.
        /// </summary>
        public int CurrentResolutionIndex => AvailableResolutions.IndexOf(CurrentWindowSize);

        /// <summary>
        /// Gets current refresh rate.
        /// </summary>
        public int CurrentRefreshRate => Array.IndexOf(AvailableRefreshRates, _settings.RefreshRate);

        /// <summary>
        /// Gets current window mode index.
        /// </summary>
        public int CurrenWindowModeIndex => Array.IndexOf(VideoSettings.FullScreenModes, _settings.WindowMode);

        /// <inheritdoc/>
        public bool HasUnappliedSettings => !_settings.Resolution.Equals(CurrentWindowSize) || _settings.WindowMode != _lastWindowMode
            || _settings.RefreshRate != _lastRefreshRate;

        private static IReadOnlyList<Vector2Int> AvailableResolutions => VideoSettings.AvailableResolutions;

        private static int[] AvailableRefreshRates => VideoSettings.AvailableRefreshRates;

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

        /// <summary>
        /// Selects windowed mode.
        /// </summary>
        /// <param name="index">Index of mode.</param>
        public void SelectWindowMode(int index)
        {
            Screen.fullScreenMode = _settings.WindowMode = VideoSettings.FullScreenModes[index];
        }

        /// <summary>
        /// Selects resolution.
        /// </summary>
        /// <param name="index">Index of resolution.</param>
        public void SelectResolution(int index)
        {
            _settings.Resolution = AvailableResolutions[index];
            Screen.SetResolution(_settings.Resolution.x, _settings.Resolution.y, _settings.WindowMode, _settings.RefreshRate);
        }

        /// <summary>
        /// Selects refresh rate.
        /// </summary>
        /// <param name="index">Index of refresh rate.</param>
        public void SelectRefreshRate(int index)
        {
            _settings.RefreshRate = AvailableRefreshRates[index];
            Screen.SetResolution(_settings.Resolution.x, _settings.Resolution.y, _settings.WindowMode, _settings.RefreshRate);
        }

        /// <inheritdoc/>
        public void SaveSettings()
        {
            if (_settings.WindowMode == FullScreenMode.Windowed)
                _settings.Resolution = CurrentWindowSize;

            RememberLastSavedSettings();
            _settings.SaveVideoSettings();
        }

        /// <inheritdoc/>
        public void DiscardChanges()
        {
            _settings.Resolution = _lastResolution;
            _settings.WindowMode = _lastWindowMode;
            _settings.RefreshRate = _lastRefreshRate;
            SetVideoParameters();
        }

        /// <inheritdoc/>
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
