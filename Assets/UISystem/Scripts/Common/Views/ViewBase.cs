using System;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Views
{
    /// <summary>
    /// Base class for a window with interactable elements (menu, popup, etc.)
    /// </summary>
    public abstract partial class ViewBase : MonoBehaviour, IView
    {

        [SerializeField] protected CanvasGroup canvasGroup;
        
        private IViewTransition _transition;
        protected Selectable[] _focusableElements;

        // is required by interface as other engines have additional checks for object not being null/being in process of destruction
        public bool IsValid => true;

        public virtual void Init()
        {
            _transition = CreateTransition();
            PopulateFocusableElements();
        }

        public void SwitchFocusAvailability(bool enable)
        {
            if (canvasGroup == null)
                return;
            canvasGroup.interactable = canvasGroup.blocksRaycasts = enable;
            //if (_focusableElements != null)
            //{
            //    for (int i = 0; i < _focusableElements.Length; i++)
            //    {
            //        if (enable)
            //            _focusableElements[i].enabled = true;
            //        else
            //            _focusableElements[i].enabled = false;
            //    }
            //}
        }

        public void Show(Action onShown, bool instant = false)
        {
            SwitchFocusAvailability(false);
            _transition.Show(() =>
            {
                SwitchFocusAvailability(true);
                onShown?.Invoke();
            }, instant);
        }

        public void Hide(Action onHidden, bool instant = false)
        {
            SwitchFocusAvailability(false);
            _transition.Hide(() =>
            {
                onHidden?.Invoke();
            }, instant);
        }

        public void DestroyView() => Destroy(this.gameObject);
        public abstract void FocusElement();
        protected abstract void PopulateFocusableElements();
        protected abstract IViewTransition CreateTransition();

    }
}