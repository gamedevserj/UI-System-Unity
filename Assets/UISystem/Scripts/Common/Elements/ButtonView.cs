using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    public class ButtonView : MonoBehaviour, IInteractableElement, IResizableElement
    {

        [SerializeField] private RectTransform resizable;

        private Button _button;
        private RectTransform _rectTransform;

        public Button Button
        {
            get
            {
                if (_button == null) { _button = GetComponent<Button>(); }
                return _button;
            }
        }
        public RectTransform ButtonTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }
        public RectTransform Resizable => resizable;
        public bool IsInteractable { get; private set; }

        public void SwitchInteractability(bool enable) => IsInteractable = enable;
    }
}