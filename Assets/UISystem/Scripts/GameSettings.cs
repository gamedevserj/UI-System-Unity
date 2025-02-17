using System;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.PhysicalInput;
using UISystem.Saving;
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

        private readonly ISaver _saver;

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

        public GameSettings(ISaver saver, GameActions actions)
        {
            _saver = saver;
            Actions = actions;
            LoadSettings();
        }

        public void SaveAudioSettings()
        {
            _saver.Save(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, MusicVolume);
            _saver.Save(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, SfxVolume);
        }

        public void SaveInterfaceSettings()
        {
            _saver.Save(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ControllerIconsType);
        }

        public void SaveVideoSettings()
        {
            _saver.Save(ConfigData.VideoSectionName, ConfigData.ResolutionKey, VideoSettings.ResolutionStringName(Resolution));
            _saver.Save(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)WindowMode);
            _saver.Save(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, RefreshRate);
        }

        public void SaveInputKeys()
        {
            _saver.Save(ConfigData.KeysSectionName, ConfigData.OverridesKey, Actions.asset.SaveBindingOverridesAsJson());
        }

        public void ResetInputMapToDefault()
        {
            Actions.asset.RemoveAllBindingOverrides();
            SaveInputKeys();
        }

        private void LoadSettings()
        {
            MusicVolume = _saver.Load(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
            SfxVolume = _saver.Load(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);
            
            Resolution = _saver.Load(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution,
                VideoSettings.ResolutionStringName, VideoSettings.ResolutionFromString);
            WindowMode = (FullScreenMode)_saver.Load(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultFullScreenMode);
            RefreshRate = _saver.Load(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, ConfigData.DefaultRefreshRate);

            ControllerIconsType = (ControllerIconsType)_saver.Load(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType);
            LoadActions();
        }

        private void LoadActions()
        {
            string keyOverrides = _saver.Load(ConfigData.KeysSectionName, ConfigData.OverridesKey, "");
            if (!string.IsNullOrEmpty(keyOverrides))
            {
                Actions.asset.LoadBindingOverridesFromJson(keyOverrides);
            }
        }
    }
}