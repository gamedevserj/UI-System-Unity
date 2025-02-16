using System;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.PhysicalInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UISystem
{
    /// <summary>
    /// methods to set properties are not marked as static so that only classes with access to instance can change them
    /// </summary>
    public class GameSettings
    {

        public static event Action<float> OnMusicVolumeChanged;
        public static event Action<float> OnSfxVolumeChanged;
        public static event Action<ControllerIconsType> OnControllerIconsChanged;

        private float _musicVolume;
        private float _sfxVolume;
        private ControllerIconsType _controllerIcons;

        private readonly INIParser _config;

        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                OnMusicVolumeChanged?.Invoke(value);
            }
        }
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                OnSfxVolumeChanged?.Invoke(value);
            }
        }
        public Vector2Int Resolution { get; set; } = ConfigData.DefaultResolution;
        public FullScreenMode WindowMode { get; set; } = ConfigData.DefaultFullScreenMode;
        public int RefreshRate { get; set; } = ConfigData.DefaultRefreshRate;
        public ControllerIconsType ControllerIconsType
        {
            get => _controllerIcons;
            set
            {
                _controllerIcons = value;
                OnControllerIconsChanged?.Invoke(value);
            }
        }

        public GameActions Actions { get; private set; }

        public GameSettings(INIParser config, GameActions actions)
        {
            _config = config;
            Actions = actions;
            OpenConfig();
            LoadSettings();
        }

        public void SaveAudioSettings()
        {
            SaveSetting(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, MusicVolume);
            SaveSetting(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, SfxVolume);
        }

        public void SaveInterfaceSettings()
        {
            SaveSetting(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ControllerIconsType);
        }

        public void SaveVideoSettings()
        {
            SaveSetting(ConfigData.VideoSectionName, ConfigData.ResolutionKey, Resolution);
            SaveSetting(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)WindowMode);
            SaveSetting(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, RefreshRate);
        }

        public void SaveInputKeys()
        {
            SaveSetting(ConfigData.KeysSectionName, ConfigData.OverridesKey, Actions.asset.SaveBindingOverridesAsJson());
        }

        public void ResetInputMapToDefault()
        {
            Actions.asset.RemoveAllBindingOverrides();
            SaveInputKeys();
        }

        private void LoadSettings()
        {
            MusicVolume = GetConfigValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
            SfxVolume = GetConfigValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);
            
            Resolution = GetConfigValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
            WindowMode = (FullScreenMode)(int)GetConfigValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultFullScreenMode);
            RefreshRate = GetConfigValue(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, ConfigData.DefaultRefreshRate);

            ControllerIconsType = (ControllerIconsType)(int)GetConfigValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType);
            LoadActions();
            CloseConfig();
        }

        // if config didn't contain the key, saves and returns default value, otherwise returns saved value
        // is used to save newly added keys
        private float GetConfigValue(string sectionName, string keyName, float defaultValue)
        {
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            float value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            return value;
        }

        private int GetConfigValue(string sectionName, string keyName, int defaultValue)
        {
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            int value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            return value;
        }

        private Vector2Int GetConfigValue(string sectionName, string keyName, Vector2Int defaultValue)
        {
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            Vector2Int value = VideoSettings.ResolutionFromString(_config.ReadValue(sectionName, keyName, VideoSettings.ResolutionStringName(defaultValue)));
            if (isNewSetting) _config.WriteValue(sectionName, keyName, VideoSettings.ResolutionStringName(value));

            return value;
        }

        private bool CheckIfNewSetting(string sectionName, string keyName)
        {
            if (!_config.IsSectionExists(sectionName) || !_config.IsKeyExists(sectionName, keyName))
                return true;
            return false;
        }

        private void SaveSetting(string sectionName, string keyName, float value)
        {
            SaveSetting(sectionName, keyName, value.ToString("0.00"));
        }

        private void SaveSetting(string sectionName, string keyName, string value)
        {
            OpenConfig();
            _config.WriteValue(sectionName, keyName, value);
            _config.Close();
        }

        private void SaveSetting(string sectionName, string keyName, Vector2Int value)
        {
            OpenConfig();
            var resolutionName = ParseResolution(value);
            _config.WriteValue(sectionName, keyName, resolutionName);
            _config.Close();
        }

        private void OpenConfig() => _config.Open(ConfigData.ConfigLocation);
        private void CloseConfig() => _config.Close();

        private void LoadActions()
        {
            string keyOverrides = _config.ReadValue(ConfigData.KeysSectionName, ConfigData.OverridesKey, "");
            if (!string.IsNullOrEmpty(keyOverrides))
            {
                Actions.asset.LoadBindingOverridesFromJson(keyOverrides);
            }
        }

        private string ParseResolution(Vector2Int resolution)
        {
            return VideoSettings.ResolutionStringName(resolution);
        }
    }
}