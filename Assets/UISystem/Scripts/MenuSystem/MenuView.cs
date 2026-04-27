using UISystem.Core.MenuSystem;
using UISystem.Views;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    /// <summary>
    /// Base class for menu views.
    /// </summary>
    public abstract partial class MenuView : ViewBase, IMenuView<Selectable>
    {
        private Selectable _lastSelectedElement;

        /// <summary>
        /// Gets the element that will have focus by default when menu is shown for the first time.
        /// </summary>
        protected abstract Selectable DefaultSelectedElement { get; }

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
        public void SetLastSelectedElement(Selectable lastSelectedElement)
        {
            _lastSelectedElement = lastSelectedElement;
        }
    }
}
