using System;
using System.Collections.Generic;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;
using UnityEngine;

namespace UISystem.ScriptableObjects
{
    /// <summary>
    /// Scriptable object containing popup view prefabs.
    /// </summary>
    [CreateAssetMenu(fileName = "PopupViewsDatabase", menuName = "PopupViewsDatabase")]
    public class PopupViewsDatabase : ScriptableObject
    {
        [SerializeField] private ViewBase _yesPopupPrefab;
        [SerializeField] private ViewBase _yesNoPopupPrefab;
        [SerializeField] private ViewBase _yesNoCancelPopupPrefab;

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
                { typeof(YesPopupView), _yesPopupPrefab },
                { typeof(YesNoPopupView), _yesNoPopupPrefab },
                { typeof(YesNoCancelPopupView), _yesNoCancelPopupPrefab },
            };
        }
    }
}
