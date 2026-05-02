using System.Collections.Generic;
using TMPro;
using UISystem.Core.Elements;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem.Common.Elements
{
    /// <summary>
    /// Dropdown view.
    /// </summary>
    public class DropdownView : MonoBehaviour, IInteractableElement
    {
        private TMP_Dropdown _dropdown;

        /// <summary>
        /// Gets dropdown component.
        /// </summary>
        public TMP_Dropdown Dropdown
        {
            get
            {
                if (_dropdown == null)
                    _dropdown = GetComponent<TMP_Dropdown>();
                return _dropdown;
            }
        }

        /// <inheritdoc/>
        public void Select() => Dropdown.Select();

        /// <inheritdoc/>
        public void SwitchInteractability(bool enable)
        {
            Dropdown.enabled = enable;
        }

        /// <summary>
        /// Sets dropdown value.
        /// </summary>
        /// <param name="value">Value to set.</param>
        public void SetValue(int value) => Dropdown.value = value;

        /// <summary>
        /// Adds action to perform when value is changed.
        /// </summary>
        /// <param name="action">Action to perform when value is changed.</param>
        public void AddOnValueChangedListener(UnityAction<int> action) => Dropdown.onValueChanged.AddListener(action);

        /// <summary>
        /// Adds options for dropdown.
        /// </summary>
        /// <param name="options">Text options to select from.</param>
        public void AddOptions(List<string> options) => Dropdown.AddOptions(options);

        /// <summary>
        /// Adds options for dropdown.
        /// </summary>
        /// <param name="options">Sprite options to select from.</param>
        public void AddOptions(List<Sprite> options) => Dropdown.AddOptions(options);

        /// <summary>
        /// Adds options for dropdown.
        /// </summary>
        /// <param name="options"><see cref="TMP_Dropdown.OptionData"/> to select from.</param>
        public void AddOptions(List<TMP_Dropdown.OptionData> options) => Dropdown.AddOptions(options);
    }
}
