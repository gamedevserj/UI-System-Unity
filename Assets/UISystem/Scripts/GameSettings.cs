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
    /// A class to handle game settings.
    /// Methods to set properties are not marked as static so that only classes with access to instance can change them.
    /// </summary>
    public class GameSettings
    {
        private readonly ISaver _saver;

        private float _musicVolume;
        private float _sfxVolume;
        private ControllerIconsType _controllerIcons;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSettings"/> class.
        /// </summary>
        /// <param name="saver">Settings saver.</param>
        /// <param name="actions">Game actions.</param>
        public GameSettings(ISaver saver, GameActions actions)
        {
            _saver = saver;
            Actions = actions;
            LoadSettings();
        }

        /// <summary>
        /// Event dispatched when music volume is changed.
        /// </summary>
        public static event Action<float> OnMusicVolumeChanged;

        /// <summary>
        /// Event dispatched when SFX volume is changed.
        /// </summary>
        public static event Action<float> OnSfxVolumeChanged;

        /// <summary>
        /// Event dispatched when type of controller icons is changed.
        /// </summary>
        public static event Action<ControllerIconsType> OnControllerIconsChanged;

        /// <summary>
        /// Gets or sets music volume.
        /// </summary>
        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                OnMusicVolumeChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Gets or sets SFX volume.
        /// </summary>
        public float SfxVolume
        {
            get => _sfxVolume;
            set
            {
                _sfxVolume = value;
                OnSfxVolumeChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Gets or sets resolution.
        /// </summary>
        public Vector2Int Resolution { get; set; } = ConfigData.DefaultResolution;

        /// <summary>
        /// Gets or sets full screen mode.
        /// </summary>
        public FullScreenMode WindowMode { get; set; } = ConfigData.DefaultFullScreenMode;

        /// <summary>
        /// Gets or sets refresh rate.
        /// </summary>
        public int RefreshRate { get; set; } = ConfigData.DefaultRefreshRate;

        /// <summary>
        /// Gets or sets type of controller icons to show.
        /// </summary>
        public ControllerIconsType ControllerIconsType
        {
            get => _controllerIcons;
            set
            {
                _controllerIcons = value;
                OnControllerIconsChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// Gets game actions.
        /// </summary>
        public GameActions Actions { get; private set; }

        /// <summary>
        /// Saves audio settings.
        /// </summary>
        public void SaveAudioSettings()
        {
            _saver.Save(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, MusicVolume);
            _saver.Save(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, SfxVolume);
        }

        /// <summary>
        /// Saves interface settings.
        /// </summary>
        public void SaveInterfaceSettings()
        {
            _saver.Save(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ControllerIconsType);
        }

        /// <summary>
        /// Saves video settings.
        /// </summary>
        public void SaveVideoSettings()
        {
            _saver.Save(ConfigData.VideoSectionName, ConfigData.ResolutionKey, Resolution);
            _saver.Save(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)WindowMode);
            _saver.Save(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, RefreshRate);
        }

        /// <summary>
        /// Saves input keys.
        /// </summary>
        public void SaveInputKeys()
        {
            _saver.Save(ConfigData.KeysSectionName, ConfigData.OverridesKey, Actions.asset.SaveBindingOverridesAsJson());
        }

        /// <summary>
        /// Resets input keys.
        /// </summary>
        public void ResetInputMapToDefault()
        {
            Actions.asset.RemoveAllBindingOverrides();
            SaveInputKeys();
        }

        private void LoadSettings()
        {
            MusicVolume = _saver.Load(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
            SfxVolume = _saver.Load(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);

            Resolution = _saver.Load(
                ConfigData.VideoSectionName,
                ConfigData.ResolutionKey,
                ConfigData.DefaultResolution);
            WindowMode = (FullScreenMode)_saver.Load(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultFullScreenMode);
            RefreshRate = _saver.Load(ConfigData.VideoSectionName, ConfigData.RefreshRateKey, ConfigData.DefaultRefreshRate);

            ControllerIconsType = (ControllerIconsType)_saver.Load(
                ConfigData.InterfaceSectionName,
                ConfigData.ControllerIconsKey,
                (int)ConfigData.DefaultControllerIconsType);
            LoadActions();
        }

        private void LoadActions()
        {
            string keyOverrides = _saver.Load(ConfigData.KeysSectionName, ConfigData.OverridesKey, string.Empty);
            if (!string.IsNullOrEmpty(keyOverrides))
            {
                Actions.asset.LoadBindingOverridesFromJson(keyOverrides);
            }
        }
    }
}
