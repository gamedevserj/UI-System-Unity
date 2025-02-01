using System.Collections.Generic;
using TMPro;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UnityEngine;

namespace UISystem.MenuSystem.Models
{
    public class VideoSettingsMenuModel : ISettingsMenuModel
    {

        private Resolution _tempResolution;
        private FullScreenMode _tempWindowMode;
        private readonly GameSettings _settings;
        private readonly Resolution[] _availableResolutions;

        public int CurrentResolutionIndex { get; private set; }
        public int CurrenWindowModeIndex { get; private set; }
        public bool HasUnappliedSettings => !GameSettings.Resolution.Equals(_tempResolution) || GameSettings.WindowMode != _tempWindowMode;

        public VideoSettingsMenuModel(GameSettings settings)
        {
            _availableResolutions = Screen.resolutions;
            _settings = settings;
            LoadSettings();
        }

        public void SelectWindowMode(int index)
        {
            //_tempWindowMode = VideoSettings.WindowModeOptions[index];
            SetWindowMode(_tempWindowMode);
        }

        public void SelectResolution(int index)
        {
            _tempResolution = _availableResolutions[index];
            Screen.SetResolution(_tempResolution.width, _tempResolution.height, _tempWindowMode, _tempResolution.refreshRate);
            //SetResolution(_tempResolution);
        }

        public string[] GetWindowModeOptionNames()
        {
            return new string[0];
            //return VideoSettings.WindowModeNames;
        }

        public string[] GetAvailableResolutionNames()
        {
            string[] resolutionNames = new string[_availableResolutions.Length];
            for (int i = 0; i < _availableResolutions.Length; i++)
            {
                resolutionNames[i] = GetResolutionName(_availableResolutions[i]);
            }
            return resolutionNames;
        }

        public void SaveSettings()
        {
            //_settings.SetResolution(_tempResolution);
            //_settings.SetWindowMode(_tempWindowMode);
            //_settings.Save();
        }

        public void DiscardChanges()
        {
            LoadSettings();
        }

        public void ResetToDefault()
        {
            //_tempResolution = ConfigData.DefaultResolution;
            //_tempWindowMode = ConfigData.Fullscreen;
            //SaveSettings();
            //SetResolution(_tempResolution);
            //SetWindowMode(_tempWindowMode);
        }

        private static Vector2Int[] GetAvailableResolutions()
        {
            return new Vector2Int[0];
            //return VideoSettings.GetResolutionsForAspect(Aspect);
        }

        private void LoadSettings()
        {
            _tempResolution = GameSettings.Resolution;
            _tempWindowMode = GameSettings.WindowMode;
            //SetResolution(_tempResolution);
            //SetWindowMode(_tempWindowMode);
        }

        private void SetResolution(Resolution resolution)
        {
            //CurrentResolutionIndex = VideoSettings.GetResolutionIndex(resolution, GetAvailableResolutions());
            //WindowSetSize(resolution);
        }

        private void SetWindowMode(FullScreenMode mode)
        {
            //CurrenWindowModeIndex = VideoSettings.GetWindwoModeIndex(mode);
            //WindowSetMode(mode);

            //// if you change resolution in fullscreen, then change window mode - window will not have the resolution that was selected
            //// this is to prevent that
            //if (mode == WindowMode.Windowed)
            //    WindowSetSize(_tempResolution);
        }

        private static string GetResolutionName(Resolution resolution)
        {
            return resolution.width + "x" + resolution.height;
        }
    }
}