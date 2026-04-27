using System;
using UnityEngine;

namespace UISystem.Saving
{
    /// <summary>
    /// Saves to ini file.
    /// </summary>
    internal class IniSaver : ISaver
    {
        private readonly INIParser _config;
        private readonly string _configLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="IniSaver"/> class.
        /// </summary>
        /// <param name="configLocation">Config location.</param>
        public IniSaver(string configLocation)
        {
            _configLocation = configLocation;
            _config = new INIParser();
        }

        /// <inheritdoc/>
        public void Save<T>(string sectionName, string keyName, T value)
        {
            Save(sectionName, keyName, ConvertToString<T>(value));
        }

        private static string ConvertToString<T>(T value)
        {
            if (value is float floatValue)
            {
                return floatValue.ToString("0.00");
            }

            return value.ToString();
        }

        private void Save(string sectionName, string keyName, string value)
        {
            OpenConfig();
            _config.WriteValue(sectionName, keyName, value);
            CloseConfig();
        }

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        public float Load(string sectionName, string keyName, float defaultValue)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            float value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            CloseConfig();
            return value;
        }

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        public int Load(string sectionName, string keyName, int defaultValue)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            int value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            CloseConfig();
            return value;
        }

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        public string Load(string sectionName, string keyName, string defaultValue)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            string value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            CloseConfig();
            return value;
        }

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <param name="parserToString">Func that will parse data to string.</param>
        /// <param name="parserFromString">Func that will parse data from string.</param>
        /// <returns>Value that was loaded.</returns>
        public Vector2Int Load(
            string sectionName,
            string keyName,
            Vector2Int defaultValue,
            Func<Vector2Int, string> parserToString,
            Func<string, Vector2Int> parserFromString)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            Vector2Int value = parserFromString(_config.ReadValue(sectionName, keyName, parserToString(defaultValue)));
            if (isNewSetting) _config.WriteValue(sectionName, keyName, parserToString(value));

            CloseConfig();
            return value;
        }

        private bool CheckIfNewSetting(string sectionName, string keyName)
        {
            if (!_config.IsSectionExists(sectionName) || !_config.IsKeyExists(sectionName, keyName))
                return true;
            return false;
        }

        private void OpenConfig() => _config.Open(_configLocation);

        private void CloseConfig() => _config.Close();
    }
}
