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
        public static Resolution DefaultResolution => Screen.currentResolution;
        public const string WindowModeKey = "WindowMode";
        public const FullScreenMode Fullscreen = FullScreenMode.ExclusiveFullScreen;

        public const string KeysSectionName = "Keys";

        public const string InterfaceSectionName = "Interface";
        public const string ControllerIconsKey = "ControllerIcons";
        public const ControllerIconsType DefaultControllerIconsType = ControllerIconsType.Xbox;

    }
}