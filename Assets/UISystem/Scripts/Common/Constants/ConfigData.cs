using UISystem.Common.Enums;
using UnityEngine;

namespace UISystem.Constants
{
    /// <summary>
    /// Class containing config keys and default values.
    /// </summary>
    public static class ConfigData
    {
        /// <summary>
        /// Gets config location.
        /// </summary>
        public static string ConfigLocation => Application.persistentDataPath + "/" + "Config.txt";

        /// <summary>
        /// Gets audio section name.
        /// </summary>
        public const string AudioSectionName = "AudioSettings";

        /// <summary>
        /// Gets music volume key name.
        /// </summary>
        public const string MusicVolumeKey = "MusicVolume";

        /// <summary>
        /// Gets default music volume value.
        /// </summary>
        public const float DefaultMusicVolume = 0.5f;

        /// <summary>
        /// Gets sfx volume key name.
        /// </summary>
        public const string SfxVolumeKey = "SfxVolume";

        /// <summary>
        /// Gets default music volume value.
        /// </summary>
        public const float DefaultSfxVolume = 0.5f;

        /// <summary>
        /// Gets video section name.
        /// </summary>
        public const string VideoSectionName = "VideoSettings";

        /// <summary>
        /// Gets resolution key name.
        /// </summary>
        public const string ResolutionKey = "Resolution";

        /// <summary>
        /// Gets default resolution value.
        /// </summary>
        public static Vector2Int DefaultResolution => new(Screen.currentResolution.width, Screen.currentResolution.height);

        /// <summary>
        /// Gets refresh rate key name.
        /// </summary>
        public const string RefreshRateKey = "RefreshRate";

        /// <summary>
        /// Gets default refresh rate value.
        /// </summary>
        public static int DefaultRefreshRate => Screen.currentResolution.refreshRate;

        /// <summary>
        /// Gets window mode key name.
        /// </summary>
        public const string WindowModeKey = "WindowMode";

        /// <summary>
        /// Gets default full screen value.
        /// </summary>
        public const FullScreenMode DefaultFullScreenMode = FullScreenMode.ExclusiveFullScreen;

        /// <summary>
        /// Gets input keys section name.
        /// </summary>
        public const string KeysSectionName = "Keys";

        /// <summary>
        /// Gets overrides key name.
        /// </summary>
        public const string OverridesKey = "Overrides";

        /// <summary>
        /// Gets interface section name.
        /// </summary>
        public const string InterfaceSectionName = "Interface";

        /// <summary>
        /// Gets controller icons key name.
        /// </summary>
        public const string ControllerIconsKey = "ControllerIcons";

        /// <summary>
        /// Gets default controller icon type value.
        /// </summary>
        public const ControllerIconsType DefaultControllerIconsType = ControllerIconsType.Xbox;
    }
}
