using System;
using UnityEngine;
using UISystem.Common.Enums;
using UISystem.Constants;

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


        public GameSettings(INIParser config)
        {
            _config = config;
            OpenConfig();
            LoadSettings();
        }

        public void SetMusicVolume(float volume)
        {
            MusicVolume = volume;
            SaveSetting(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, volume);
            OnMusicVolumeChanged?.Invoke(volume);
        }

        public void SetSfxVolume(float volume)
        {
            SfxVolume = volume;
            SaveSetting(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, volume);
            OnSfxVolumeChanged?.Invoke(volume);
        }

        public void SetControllerIconsType(ControllerIconsType type)
        {
            ControllerIconsType = type;
            SaveSetting(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)type);
            OnControllerIconsChanged?.Invoke(type);
        }

        public void SetResolution(Vector2Int resolution)
        {
            Resolution = resolution;
            SaveSetting(ConfigData.VideoSectionName, ConfigData.ResolutionKey, resolution);
        }

        public void SetWindowMode(FullScreenMode mode)
        {
            WindowMode = mode;
            SaveSetting(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)mode);
        }

        public void SetRefreshRate(int rate)
        {
            RefreshRate = rate;
            SaveSetting(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, rate);
        }

        //public void ResetInputMapToDefault()
        //{
        //    InputMap.LoadFromProjectSettings();
        //    SetAllInputsInConfig();
        //    Save();
        //}

        //public void SaveInputActionKey(string action, Godot.Collections.Array<InputEvent> events)
        //{
        //    InputMap.ActionEraseEvents(action);
        //    foreach (var item in events)
        //    {
        //        InputMap.ActionAddEvent(action, item);
        //    }
        //    SetInputInConfig(action);
        //    Save();
        //}

        //public void Save()
        //{
        //    _config.Save(ConfigData.ConfigLocation);
        //}



        private void LoadSettings()
        {
            MusicVolume = GetConfigValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
            SfxVolume = GetConfigValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);
            
            Resolution = GetConfigValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
            WindowMode = (FullScreenMode)(int)GetConfigValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultFullScreenMode);

            ControllerIconsType = (ControllerIconsType)(int)GetConfigValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType);

            //LoadInputs(ref saveNewSettings);

            //if (saveNewSettings)
            //    Save();
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

        //private void LoadInputs(ref bool saveNewSettings)
        //{
        //    if (!_config.HasSection(ConfigData.KeysSectionName))
        //    {
        //        InputMap.LoadFromProjectSettings();
        //        SetAllInputsInConfig();
        //        saveNewSettings = true;
        //        return;
        //    }

        //    var savedKeys = _config.GetSectionKeys(ConfigData.KeysSectionName);
        //    for (int i = 0; i < InputsData.RebindableActions.Length; i++)
        //    {
        //        if (!savedKeys.Contains(InputsData.RebindableActions[i]))
        //        {
        //            SetInputInConfig(InputsData.RebindableActions[i]);
        //            saveNewSettings = true;
        //        }
        //    }

        //    for (int i = 0; i < savedKeys.Length; i++)
        //    {
        //        var action = savedKeys[i];
        //        Godot.Collections.Array<InputEvent> events = (Godot.Collections.Array<InputEvent>)_config.GetValue(ConfigData.KeysSectionName, action);

        //        InputMap.ActionEraseEvents(action);
        //        for (int k = 0; k < events.Count; k++)
        //        {
        //            InputMap.ActionAddEvent(action, events[k]);
        //        }
        //    }
        //}

        //private void SetAllInputsInConfig()
        //{
        //    for (var i = 0; i < InputsData.RebindableActions.Length; i++)
        //    {
        //        SetInputInConfig(InputsData.RebindableActions[i]);
        //    }
        //}

        //private void SetInputInConfig(string action)
        //{
        //    var events = InputMap.ActionGetEvents(action);
        //    _config.SetValue(ConfigData.KeysSectionName, action, events);
        //}

        private string ParseResolution(Vector2Int resolution)
        {
            return VideoSettings.ResolutionStringName(resolution);
        }
    }
}