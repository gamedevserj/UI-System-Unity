using System;
using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UnityEngine;

namespace UISystem.Views
{
    /// <summary>
    /// Base class for a window with interactable elements (menu, popup, etc.)
    /// </summary>
    public abstract partial class ViewBase : MonoBehaviour, IView
    {

        [SerializeField] protected CanvasGroup canvasGroup;

        private IViewTransition _transition;
        protected IInteractableElement[] _interactableElements;

        public CanvasGroup FadeObjectsContainer => canvasGroup;

        public virtual void Init()
        {
            _transition = CreateTransition();
            SetInteractableElements();
        }

        public void SwitchInteractability(bool enable)
        {
            if (canvasGroup == null)
                return;
            // you can use this line instead of switching interactability for every element
            // mind that it will cause your buttons to change to disabled state which will change their appearance during transitions
            // canvasGroup.interactable = canvasGroup.blocksRaycasts = enable;
            if (_interactableElements != null)
            {
                for (int i = 0; i < _interactableElements.Length; i++)
                {
                    _interactableElements[i].SwitchInteractability(enable);
                }
            }
        }

        public void Show(Action onShown, bool instant = false)
        {
            SwitchInteractability(false);
            _transition.Show(() =>
            {
                SwitchInteractability(true);
                onShown?.Invoke();
            }, instant);
        }

        public void Hide(Action onHidden, bool instant = false)
        {
            SwitchInteractability(false);
            _transition.Hide(() =>
            {
                onHidden?.Invoke();
            }, instant);
        }

        public void DestroyView() => Destroy(this.gameObject);
        public abstract void FocusElement();
        protected abstract void SetInteractableElements();
        protected abstract IViewTransition CreateTransition();

    }
}