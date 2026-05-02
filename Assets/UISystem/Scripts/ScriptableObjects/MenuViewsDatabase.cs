using System;
using System.Collections.Generic;
using UISystem.MenuSystem.Views;
using UISystem.Views;
using UnityEngine;

namespace UISystem.ScriptableObjects
{
    /// <summary>
    /// Scriptable object containing menu view prefabs.
    /// </summary>
    [CreateAssetMenu(fileName = "MenuViewsDatabase", menuName = "MenuViewsDatabase")]
    public class MenuViewsDatabase : ScriptableObject
    {
        [SerializeField] private ViewBase _mainMenuPrefab;
        [SerializeField] private ViewBase _inGameMenuPrefab;
        [SerializeField] private ViewBase _optionMenuPrefab;
        [SerializeField] private ViewBase _audioSettingsMenuPrefab;
        [SerializeField] private ViewBase _interfaceSettingsMenuPrefab;
        [SerializeField] private ViewBase _videoSettingsMenuPrefab;
        [SerializeField] private ViewBase _rebindKeysMenuPrefab;
        [SerializeField] private ViewBase _pauseMenuPrefab;

        private Dictionary<Type, ViewBase> _prefabs;

        /// <summary>
        /// Gets the view prefab.
        /// </summary>
        /// <param name="type">Type of view.</param>
        /// <returns>Prefab of the view.</returns>
        public ViewBase GetPrefab(Type type)
        {
            if (_prefabs == null || _prefabs.Count == 0)
                CreateDictionary();

            return _prefabs[type];
        }

        private void CreateDictionary()
        {
            _prefabs = new Dictionary<Type, ViewBase>
            {
                { typeof(MainMenuView), _mainMenuPrefab },
                { typeof(InGameMenuView), _inGameMenuPrefab },
                { typeof(OptionsMenuView), _optionMenuPrefab },
                { typeof(AudioSettingsMenuView), _audioSettingsMenuPrefab },
                { typeof(InterfaceSettingsMenuView), _interfaceSettingsMenuPrefab },
                { typeof(VideoSettingsMenuView), _videoSettingsMenuPrefab },
                { typeof(RebindKeysMenuView), _rebindKeysMenuPrefab },
                { typeof(PauseMenuView), _pauseMenuPrefab },
            };
        }
    }
}
