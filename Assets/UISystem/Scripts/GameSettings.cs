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

        private readonly INIParser _config;

        public static float MusicVolume { get; private set; } = ConfigData.DefaultMusicVolume;
        public static float SfxVolume { get; private set; } = ConfigData.DefaultSfxVolume;
        public static Vector2Int Resolution { get; private set; } = ConfigData.DefaultResolution;
        public static FullScreenMode WindowMode { get; private set; } = ConfigData.DefaultFullScreenMode;
        public static int RefreshRate { get; private set; } = ConfigData.DefaultRefreshRate;
        public static ControllerIconsType ControllerIconsType { get; private set; } = ConfigData.DefaultControllerIconsType;

        public static GameActions Actions { get; private set; }

        public GameSettings(INIParser config, GameActions actions)
        {
            _config = config;
            Actions = actions;
            OpenConfig();
            LoadSettings();
        }

        public void SaveMusicVolume(float volume)
        {
            MusicVolume = volume;
            SaveSetting(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, volume);
            OnMusicVolumeChanged?.Invoke(volume);
        }

        public void SaveSfxVolume(float volume)
        {
            SfxVolume = volume;
            SaveSetting(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, volume);
            OnSfxVolumeChanged?.Invoke(volume);
        }

        public void SaveControllerIconsType(ControllerIconsType type)
        {
            ControllerIconsType = type;
            SaveSetting(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)type);
            OnControllerIconsChanged?.Invoke(type);
        }

        public void SaveResolution(Vector2Int resolution)
        {
            Resolution = resolution;
            SaveSetting(ConfigData.VideoSectionName, ConfigData.ResolutionKey, resolution);
        }

        public void SaveWindowMode(FullScreenMode mode)
        {
            WindowMode = mode;
            SaveSetting(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)mode);
        }

        public void SetRefreshRate(int rate)
        {
            RefreshRate = rate;
            SaveSetting(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, rate);
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

        private void OpenConfig() => _config.Open(Application.persistentDataPath + ConfigData.ConfigLocation);
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