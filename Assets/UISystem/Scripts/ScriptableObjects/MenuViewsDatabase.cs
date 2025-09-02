using System;
using System.Collections.Generic;
using UISystem.MenuSystem.Views;
using UISystem.Views;
using UnityEngine;

namespace UISystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MenuViewsDatabase", menuName = "MenuViewsDatabase")]
    public class MenuViewsDatabase : ScriptableObject
    {

        [SerializeField] private ViewBase mainMenuPrefab;
        [SerializeField] private ViewBase inGameMenuPrefab;
        [SerializeField] private ViewBase optionMenuPrefab;
        [SerializeField] private ViewBase audioSettingsMenuPrefab;
        [SerializeField] private ViewBase interfaceSettingsMenuPrefab;
        [SerializeField] private ViewBase videoSettingsMenuPrefab;
        [SerializeField] private ViewBase rebindKeysMenuPrefab;
        [SerializeField] private ViewBase pauseMenuPrefab;

        private Dictionary<Type, ViewBase> _prefabs;

        public ViewBase GetView(Type type)
        {
            if (_prefabs == null || _prefabs.Count == 0)
                CreateDictionary();

            return _prefabs[type];
        }

        public void ClearDictionary() => _prefabs = null;

        private void CreateDictionary()
        {
            _prefabs = new Dictionary<Type, ViewBase>
            {
                { typeof(MainMenuView), mainMenuPrefab },
                { typeof(InGameMenuView), inGameMenuPrefab},
                { typeof(OptionsMenuView), optionMenuPrefab},
                { typeof(AudioSettingsMenuView), audioSettingsMenuPrefab},
                { typeof(InterfaceSettingsMenuView), interfaceSettingsMenuPrefab},
                { typeof(VideoSettingsMenuView), videoSettingsMenuPrefab},
                { typeof(RebindKeysMenuView), rebindKeysMenuPrefab},
                { typeof(PauseMenuView), pauseMenuPrefab},
            };
        }
    }
}