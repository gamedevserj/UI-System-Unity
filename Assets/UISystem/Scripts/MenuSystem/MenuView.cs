using UISystem.Core.MenuSystem;
using UISystem.Views;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    public abstract partial class MenuView : ViewBase, IMenuView<Selectable>
    {

        private Selectable _lastSelectedElement;
        protected abstract Selectable DefaultSelectedElement { get; }

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

        public void SetLastSelectedElement(Selectable lastSelectedElement)
        {
            _lastSelectedElement = lastSelectedElement;
        }
    }
}