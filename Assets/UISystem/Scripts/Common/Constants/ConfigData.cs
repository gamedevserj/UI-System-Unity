using UISystem.Common.Enums;
using UnityEngine;

namespace UISystem.Constants
{
    public static class ConfigData
    {

        public const string ConfigLocation = "Config.txt";

        public const string AudioSectionName = "AudioSettings";
        public const string MusicVolumeKey = "MusicVolume";
        public const float DefaultMusicVolume = 0.5f;
        public const string SfxVolumeKey = "SfxVolume";
        public const float DefaultSfxVolume = 0.5f;

        public const string VideoSectionName = "VideoSettings";
        public const string ResolutionKey = "Resolution";
        public static Vector2Int DefaultResolution => new(Screen.currentResolution.width, Screen.currentResolution.height);
        public const string RefreshRateKey = "RefreshRate";
        public static int DefaultRefreshRate => Screen.currentResolution.refreshRate;
        public const string WindowModeKey = "WindowMode";
        public const FullScreenMode DefaultFullScreenMode = FullScreenMode.ExclusiveFullScreen;

        public const string KeysSectionName = "Keys";
        public const string OverridesKey = "Overrides";

        public const string InterfaceSectionName = "Interface";
        public const string ControllerIconsKey = "ControllerIcons";
        public const ControllerIconsType DefaultControllerIconsType = ControllerIconsType.Xbox;

    }
}