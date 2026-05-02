using UISystem.Core.Elements;
using UISystem.Core.MenuSystem;
using UISystem.Views;

namespace UISystem.MenuSystem
{
    /// <summary>
    /// Base class for menu views.
    /// </summary>
    public abstract partial class MenuView : ViewBase, IMenuView
    {
        private IInteractableElement _lastSelectedElement;

        /// <summary>
        /// Gets the element that will have focus by default when menu is shown for the first time.
        /// </summary>
        protected abstract IInteractableElement DefaultSelectedElement { get; }

        /// <inheritdoc/>
        public override void FocusElement()
        {
            if (_lastSelectedElement != null)
            {
                _lastSelectedElement.Select();
            }
            else if (DefaultSelectedElement != null)
            {
                DefaultSelectedElement.Select();
            }
        }

        /// <inheritdoc/>
        public void SetLastSelectedElement(IInteractableElement lastSelectedElement)
        {
            _lastSelectedElement = lastSelectedElement;
        }
    }
}
