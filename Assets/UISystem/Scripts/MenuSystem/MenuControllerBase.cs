using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    // just a base class to adapt generic controller to Unity's specific parameters
    // so that there is no need to specify IMenuModel, IFocusableControl for every controller
    internal abstract class MenuControllerBase<TViewCreator, TView>
        : MenuController<TViewCreator, TView, IMenuModel, Selectable>
        where TViewCreator : IViewCreator<TView>
        where TView : IMenuView<Selectable>
    {
        protected MenuControllerBase(TViewCreator viewCreator, IMenuModel model, IMenusManager menusManager) : base(viewCreator, model, menusManager)
        {
        }
    }
}
