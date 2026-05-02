using System;
using UnityEngine;

namespace UISystem.Saving
{
    /// <summary>
    /// Defines contract for classes that will save settings.
    /// </summary>
    public interface ISaver
    {
        /// <summary>
        /// Saves value.
        /// </summary>
        /// <typeparam name="T">Type of data to save.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="value">Value to save.</param>
        void Save<T>(string sectionName, string keyName, T value);

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        float Load(string sectionName, string keyName, float defaultValue);

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        int Load(string sectionName, string keyName, int defaultValue);

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        string Load(string sectionName, string keyName, string defaultValue);

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        Vector2Int Load(string sectionName, string keyName, Vector2Int defaultValue);

        /// <summary>
        /// Loads saved value, if config didn't contain the key, saves and returns default value.
        /// Is used to save newly added keys.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <param name="keyName">Key name.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Value that was loaded.</returns>
        TEnum Load<TEnum>(string sectionName, string keyName, TEnum defaultValue)
            where TEnum : struct, Enum;
    }
}
