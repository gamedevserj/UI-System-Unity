using System;
using UnityEngine;

namespace UISystem.Saving
{
    internal class IniSaver : ISaver
    {

        private readonly INIParser _config;
        private readonly string _configLocation;

        public IniSaver(string configLocation)
        {
            _configLocation = configLocation;
            _config = new INIParser();
        }

        public void Save(string sectionName, string keyName, float value)
        {
            Save(sectionName, keyName, value.ToString("0.00"));
        }

        public void Save(string sectionName, string keyName, int value)
        {
            Save(sectionName, keyName, value.ToString());
        }

        public void Save(string sectionName, string keyName, string value)
        {
            OpenConfig();
            _config.WriteValue(sectionName, keyName, value);
            CloseConfig();
        }

        // if config didn't contain the key, saves and returns default value, otherwise returns saved value
        // is used to save newly added keys
        public float Load(string sectionName, string keyName, float defaultValue)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            float value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            CloseConfig();
            return value;
        }

        public int Load(string sectionName, string keyName, int defaultValue)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            int value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            CloseConfig();
            return value;
        }

        public string Load(string sectionName, string keyName, string defaultValue)
        {
            OpenConfig();
            bool isNewSetting = CheckIfNewSetting(sectionName, keyName);

            string value = _config.ReadValue(sectionName, keyName, defaultValue);
            if (isNewSetting) _config.WriteValue(sectionName, keyName, value);

            CloseConfig();
            return value;
        }

        public Vector2Int Load(string sectionName, string keyName, Vector2Int defaultValue, Func<Vector2Int, string> parserToString,
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
