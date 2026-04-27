using System;
using System.Threading.Tasks;
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
        [SerializeField] protected CanvasGroup _canvasGroup;

        private IViewTransition _transition;

        /// <summary>
        /// Gets CanvasGroup responsible for fading the whole view.
        /// </summary>
        public CanvasGroup FadeObjectsContainer => _canvasGroup;

        /// <summary>
        /// Gets or sets interactable elements.
        /// </summary>
        protected IInteractableElement[] InteractableElements { get; set; }

        /// <inheritdoc/>
        public virtual void Init()
        {
            _transition = CreateTransition();
            SetInteractableElements();
        }

        /// <inheritdoc/>
        public void SwitchInteractability(bool enable)
        {
            if (_canvasGroup == null)
                return;

            /* you can use this line instead of switching interactability for every element
             * mind that it will cause your buttons to change to disabled state which will change their appearance during transitions
             * canvasGroup.interactable = canvasGroup.blocksRaycasts = enable;
            */

            if (InteractableElements != null)
            {
                for (int i = 0; i < InteractableElements.Length; i++)
                {
                    InteractableElements[i].SwitchInteractability(enable);
                }
            }
        }

        /// <inheritdoc/>
        public async Task Show(bool instant = false)
        {
            SwitchInteractability(false);
            await _transition.Show(instant);
            SwitchInteractability(true);
        }

        /// <inheritdoc/>
        public async Task Hide(bool instant = false)
        {
            SwitchInteractability(false);
            await _transition.Hide(instant);
        }

        /// <inheritdoc/>
        public void DestroyView() => Destroy(this.gameObject);

        /// <inheritdoc/>
        public abstract void FocusElement();

        /// <summary>
        /// Sets interactable elements.
        /// </summary>
        protected abstract void SetInteractableElements();

        /// <summary>
        /// Creates transition.
        /// </summary>
        /// <returns>Instance of transition.</returns>
        protected abstract IViewTransition CreateTransition();
    }
}
