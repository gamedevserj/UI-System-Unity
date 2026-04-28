using System;
using UISystem.Helpers;
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

        /// <inheritdoc/>
        public float Load(string sectionName, string keyName, float defaultValue)
        {
            return Load(
                sectionName,
                keyName,
                defaultValue,
                (value) => value.ToString(),
                (stringValue) => float.TryParse(stringValue, out float result) ? result : defaultValue);
        }

        /// <inheritdoc/>
        public int Load(string sectionName, string keyName, int defaultValue)
        {
            return Load(
                sectionName,
                keyName,
                defaultValue,
                (value) => value.ToString(),
                (stringValue) => int.TryParse(stringValue, out int result) ? result : defaultValue);
        }

        /// <inheritdoc/>
        public string Load(string sectionName, string keyName, string defaultValue)
        {
            return Load(sectionName, keyName, defaultValue, (v) => v, (stringValue) => stringValue);
        }

        /// <inheritdoc/>
        public Vector2Int Load(string sectionName, string keyName, Vector2Int defaultValue)
        {
            return Load(
                sectionName,
                keyName,
                defaultValue,
                (v) => v.ToString(),
                (stringValue) => ParsingHelpers.TryParseVector2Int(stringValue, out Vector2Int value) ? value : defaultValue);
        }

        private static string ConvertToString<T>(T value)
        {
            if (value is float floatValue)
            {
                return floatValue.ToString("0.00");
            }

            return value.ToString();
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
        private T Load<T>(
            string sectionName,
            string keyName,
            T defaultValue,
            Func<T, string> parserToString,
            Func<string, T> parserFromString)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            var stringValue = _config.ReadValue(sectionName, keyName, parserToString(defaultValue));
            T value = parserFromString(stringValue);
            if (isNewSetting)
                _config.WriteValue(sectionName, keyName, stringValue);

            CloseConfig();
            return value;
        }

        private void Save(string sectionName, string keyName, string value)
        {
            OpenConfig();
            _config.WriteValue(sectionName, keyName, value);
            CloseConfig();
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
