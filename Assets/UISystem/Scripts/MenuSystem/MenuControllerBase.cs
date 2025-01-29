using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    // just a base class to adapt generic controller to Godot's specific parameters
    // so that there is no need to specify IMenuModel, InputEvent, IFocusableControl for every controller
    internal abstract class MenuControllerBase<TViewCreator, TView>
        : MenuController<TViewCreator, TView, IMenuModel, KeyCode, Selectable, MenuType>
        where TViewCreator : IViewCreator<TView>
        where TView : IMenuView<Selectable>
    {
        protected MenuControllerBase(TViewCreator viewCreator, IMenuModel model, IMenusManager<KeyCode, MenuType> menusManager) : base(viewCreator, model, menusManager)
        {
        }
    }
}
