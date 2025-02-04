using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UISystem.Constants
{
    public static class VideoSettings
    {

        public static readonly Vector2Int[] AvailableResolutions;
        public static readonly List<string> ResolutionNames;

        public static readonly int[] AvailableRefreshRates;
        public static readonly List<string> RefreshRateNames;

        public static readonly FullScreenMode[] FullScreenModes = new FullScreenMode[]
        {
            FullScreenMode.ExclusiveFullScreen,
            FullScreenMode.FullScreenWindow,
            FullScreenMode.MaximizedWindow,
            FullScreenMode.Windowed
        };
        public static readonly List<string> FullScreenModeNames;

        static VideoSettings()
        {
            ResolutionNames = new List<string>();
            RefreshRateNames = new List<string>();
            Vector2Int previousResolution = Vector2Int.zero;
            int previousRefreshRate = 0;
            List<Vector2Int> availableResolutions = new List<Vector2Int>();
            List<int> availableRefreshRates = new List<int>();
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                // unity considers refresh rate when getting resolutions, this removes duplicates
                if (!availableResolutions.Contains(new Vector2Int(Screen.resolutions[i].width, Screen.resolutions[i].height)))
                //if(previousResolution.x != Screen.resolutions[i].width || previousResolution.y != Screen.resolutions[i].height)
                {
                    ResolutionNames.Add(ResolutionStringName(new Vector2Int(Screen.resolutions[i].width, Screen.resolutions[i].height)));
                    previousResolution.x = Screen.resolutions[i].width;
                    previousResolution.y = Screen.resolutions[i].height;
                    availableResolutions.Add(previousResolution);
                }
                if (!availableRefreshRates.Contains(Screen.resolutions[i].refreshRate))
                {
                    previousRefreshRate = Screen.resolutions[i].refreshRate;
                    availableRefreshRates.Add(previousRefreshRate);
                    RefreshRateNames.Add(previousRefreshRate.ToString());
                }
            }
            AvailableResolutions = availableResolutions.ToArray();
            AvailableRefreshRates = availableRefreshRates.ToArray();

            FullScreenModeNames = new List<string>();
            foreach (var item in FullScreenModes)
            {
                FullScreenModeNames.Add(ParseFullScreenMode(item));
            }
        }

        public static string ResolutionStringName(Vector2Int resolution)
        {
            return resolution.x + "x" + resolution.y;
        }

        public static Vector2Int ResolutionFromString(string resolutionName)
        {
            string[] names = resolutionName.Split('x');
            if (!Int32.TryParse(names[0], out int width) || !Int32.TryParse(names[1], out int height))
            {
                throw new Exception("Couldn't convert resolution name!");
            }
            else
            {
                return new Vector2Int(width, height);
            }
        }

        private static string ParseFullScreenMode(FullScreenMode name)
        {
            return Regex.Replace(name.ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
        }

        //public static readonly WindowMode[] WindowModeOptions = new WindowMode[]
        //{
        //WindowMode.ExclusiveFullscreen,
        //WindowMode.Fullscreen,
        //WindowMode.Windowed,
        //WindowMode.Maximized,
        //};
        //public static readonly string[] WindowModeNames;

        //private static readonly Vector2Int[] Resolutions16x9 = new Vector2Int[]
        //{

        //new (854, 480),
        //new (960, 540),
        //new (1280, 720),
        //new (1366, 768),
        //new(1600, 900),
        //new(1920, 1080),
        //new(2560, 1440),
        //new(3200, 1800),
        //new(3840, 2160),
        //};
        //private static readonly string[] ResolutionNames16x9;

        //private static readonly Vector2I[] Resolutions16x10 = new Vector2I[]
        //{
        //new (1280, 800),
        //new (1440, 900),
        //new (1680, 1050),
        //new (1920, 1200),
        //new (2560, 1600),
        //new (3840, 2400),
        //};
        //private static readonly string[] ResolutionNames16x10;

        //static VideoSettings()
        //{
        //    ResolutionNames16x9 = new string[Resolutions16x9.Length];
        //    for (int i = 0; i < Resolutions16x9.Length; i++)
        //    {
        //        ResolutionNames16x9[i] = GetResolutionName(Resolutions16x9[i]);
        //    }

        //    ResolutionNames16x10 = new string[Resolutions16x10.Length];
        //    for (int i = 0; i < Resolutions16x10.Length; i++)
        //    {
        //        ResolutionNames16x10[i] = GetResolutionName(Resolutions16x10[i]);
        //    }

        //    WindowModeNames = new string[WindowModeOptions.Length];
        //    for (int i = 0; i < WindowModeOptions.Length; i++)
        //    {
        //        WindowModeNames[i] = Regex.Replace(WindowModeOptions[i].ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
        //    }
        //}

        //public static Vector2I[] GetResolutionsForAspect(double aspect)
        //{
        //    if (Mathf.IsEqualApprox(aspect, 1.77f))
        //        return Resolutions16x9;
        //    if (Mathf.IsEqualApprox(aspect, 1.6f))
        //        return Resolutions16x10;

        //    return Resolutions16x9;
        //}

        //public static string[] GetResolutionsNamesForAspect(double aspect)
        //{
        //    if (Mathf.IsEqualApprox(aspect, 1.77f))
        //        return ResolutionNames16x9;
        //    if (Mathf.IsEqualApprox(aspect, 1.6f))
        //        return ResolutionNames16x10;

        //    return ResolutionNames16x9;
        //}

        //public static int GetResolutionIndex(Vector2I resolution, Vector2I[] allResolutions)
        //{
        //    return Array.IndexOf(allResolutions, resolution);
        //}

        //public static int GetWindwoModeIndex(FullScreenMode mode)
        //{
        //    return Array.IndexOf(WindowModeOptions, mode);
        //}

        //private static string GetResolutionName(Resolution resolution)
        //{
        //    return resolution.width + "x" + resolution.height;
        //}

    }
}