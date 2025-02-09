using UISystem.Core.Transitions;
using UISystem.Transitions;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class InGameMenuView : MenuView
    {

        protected override Selectable DefaultSelectedElement => null;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
        }

        protected override void SetInteractableElements()
        { }

    }
}