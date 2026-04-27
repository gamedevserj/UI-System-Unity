using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UISystem.Constants
{
    /// <summary>
    /// Class containing video settings.
    /// </summary>
    public static class VideoSettings
    {
        private static readonly Vector2Int[] _availableResolutions;
        private static readonly List<string> _resolutionNames;

        private static readonly int[] _availableRefreshRates;
        private static readonly List<string> _refreshRateNames;

        private static readonly FullScreenMode[] _fullScreenModes = new FullScreenMode[]
        {
            FullScreenMode.ExclusiveFullScreen,
            FullScreenMode.FullScreenWindow,
            FullScreenMode.MaximizedWindow,
            FullScreenMode.Windowed,
        };

        private static readonly List<string> _fullScreenModeNames;

        static VideoSettings()
        {
            _resolutionNames = new List<string>();
            _refreshRateNames = new List<string>();
            var previousResolution = Vector2Int.zero;
            int previousRefreshRate = 0;
            var availableResolutions = new List<Vector2Int>();
            var availableRefreshRates = new List<int>();
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                // unity considers refresh rate when getting resolutions, this removes duplicates
                if (!availableResolutions.Contains(new Vector2Int(Screen.resolutions[i].width, Screen.resolutions[i].height)))
                {
                    _resolutionNames.Add(GetResolutionName(new Vector2Int(Screen.resolutions[i].width, Screen.resolutions[i].height)));
                    previousResolution.x = Screen.resolutions[i].width;
                    previousResolution.y = Screen.resolutions[i].height;
                    availableResolutions.Add(previousResolution);
                }

                if (!availableRefreshRates.Contains(Screen.resolutions[i].refreshRate))
                {
                    previousRefreshRate = Screen.resolutions[i].refreshRate;
                    availableRefreshRates.Add(previousRefreshRate);
                    _refreshRateNames.Add(previousRefreshRate.ToString());
                }
            }

            _availableResolutions = availableResolutions.ToArray();
            _availableRefreshRates = availableRefreshRates.ToArray();

            _fullScreenModeNames = new List<string>();
            foreach (var item in _fullScreenModes)
            {
                _fullScreenModeNames.Add(ParseFullScreenMode(item));
            }
        }

        /// <summary>
        /// Gets available resolutions.
        /// </summary>
        public static Vector2Int[] AvailableResolutions => _availableResolutions;

        /// <summary>
        /// Gets resolution names.
        /// </summary>
        public static List<string> ResolutionNames => _resolutionNames;

        /// <summary>
        /// Gets available refresh rates.
        /// </summary>
        public static int[] AvailableRefreshRates => _availableRefreshRates;

        /// <summary>
        /// Gets refresh rate names.
        /// </summary>
        public static List<string> RefreshRateNames => _refreshRateNames;

        /// <summary>
        /// Gets available full screen modes.
        /// </summary>
        public static FullScreenMode[] FullScreenModes => _fullScreenModes;

        /// <summary>
        /// Gets full screen mode names.
        /// </summary>
        public static List<string> FullScreenModeNames => _fullScreenModeNames;

        /// <summary>
        /// Gets resolution string name.
        /// </summary>
        /// <param name="resolution">Resolution.</param>
        /// <returns>Name of the resolution.</returns>
        public static string GetResolutionName(Vector2Int resolution)
        {
            return resolution.x + "x" + resolution.y;
        }

        /// <summary>
        /// Gets the resolution from string.
        /// </summary>
        /// <param name="resolutionName">Resolution name.</param>
        /// <returns>Resolution.</returns>
        /// <exception cref="InvalidOperationException">Thrown if <param name="resolutionName"/> can not be converted to resolution.</exception>
        public static Vector2Int GetResolutionFromString(string resolutionName)
        {
            string[] names = resolutionName.Split('x');
            if (!int.TryParse(names[0], out int width) || !int.TryParse(names[1], out int height))
            {
                throw new InvalidOperationException("Couldn't convert resolution name!");
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
