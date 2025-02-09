using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    public class SliderView : MonoBehaviour, IInteractableElement
    {

        private Slider slider;
        public Slider Slider
        {
            get
            {
                if (slider == null)
                    slider = GetComponent<Slider>();
                return slider;
            }
        }

        public void SwitchInteractability(bool enable) => Slider.enabled = enable;

        public void SetValue(float value) => Slider.value = value;

        public void AddListener(UnityAction<float> action) => Slider.onValueChanged.AddListener(action);
    }
}
