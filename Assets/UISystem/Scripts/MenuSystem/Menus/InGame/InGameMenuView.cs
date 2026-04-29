using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// In-game menu view.
    /// </summary>
    public class InGameMenuView : MenuView
    {
        /// <inheritdoc/>
        protected override Selectable DefaultSelectedElement => null;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
        }
    }
}
