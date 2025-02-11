using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UISystem.Common.Elements
{
    public class DropdownView : MonoBehaviour, IInteractableElement
    {

        private TMP_Dropdown dropdown;
        public TMP_Dropdown Dropdown
        {
            get
            {
                if (dropdown == null)
                    dropdown = GetComponent<TMP_Dropdown>();
                return dropdown;
            }
        }

        public void SwitchInteractability(bool enable)
        {
            Dropdown.enabled = enable;
        }

        public void SetValue(int value) => Dropdown.value = value;

        public void AddListener(UnityAction<int> action) => Dropdown.onValueChanged.AddListener(action);

        public void AddOptions(List<string> options) => Dropdown.AddOptions(options);

        public void AddOptions(List<Sprite> options) => Dropdown.AddOptions(options);

        public void AddOptions(List<TMP_Dropdown.OptionData> options) => Dropdown.AddOptions(options);
    }
}
