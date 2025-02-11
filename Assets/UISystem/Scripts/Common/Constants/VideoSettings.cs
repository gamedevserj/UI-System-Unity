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
    }
}