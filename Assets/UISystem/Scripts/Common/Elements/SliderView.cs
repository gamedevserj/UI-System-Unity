using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    /// <summary>
    /// Slider view.
    /// </summary>
    public class SliderView : MonoBehaviour, IInteractableElement
    {

        private Slider _slider;

        /// <summary>
        /// Gets slider component.
        /// </summary>
        public Slider Slider
        {
            get
            {
                if (_slider == null)
                    _slider = GetComponent<Slider>();
                return _slider;
            }
        }

        /// <inheritdoc/>
        public void SwitchInteractability(bool enable) => Slider.enabled = enable;

        /// <summary>
        /// Sets slider value.
        /// </summary>
        /// <param name="value">Value to set.</param>
        public void SetValue(float value) => Slider.value = value;

        /// <summary>
        /// Adds action to perform when value is changed.
        /// </summary>
        /// <param name="action">Action to perform when value is changed.</param>
        public void AddOnValueChangedListener(UnityAction<float> action) => Slider.onValueChanged.AddListener(action);
    }
}
