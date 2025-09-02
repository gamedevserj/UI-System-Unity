using System;
using System.Collections.Generic;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;
using UnityEngine;

namespace UISystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PopupViewsDatabase", menuName = "PopupViewsDatabase")]
    public class PopupViewsDatabase : ScriptableObject
    {
        [SerializeField] private ViewBase yesPopupPrefab;
        [SerializeField] private ViewBase yesNoPopupPrefab;
        [SerializeField] private ViewBase yesNoCancelPopupPrefab;

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
                { typeof(YesPopupView), yesPopupPrefab },
                { typeof(YesNoPopupView), yesNoPopupPrefab},
                { typeof(YesNoCancelPopupView), yesNoCancelPopupPrefab},
            };
        }
    }
}

