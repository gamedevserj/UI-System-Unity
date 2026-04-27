using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    /// <summary>
    /// A base class to adapt generic controller to Unity's specific parameters
    /// so that there is no need to specify IMenuModel, IFocusableControl for every controller.
    /// </summary>
    /// <typeparam name="TViewCreator">Type of view creator. Must implement <see cref="IViewCreator{TView}"/>..</typeparam>
    /// <typeparam name="TView">Type of view. Must implement <see cref="IMenuView{Selectable}"/>.</typeparam>
    internal abstract class MenuControllerBase<TViewCreator, TView>
        : MenuController<TViewCreator, TView, Selectable>
        where TViewCreator : IViewCreator<TView>
        where TView : IMenuView<Selectable>
    {
        /// <inheritdoc/>
        protected MenuControllerBase(TViewCreator viewCreator, IMenusManager menusManager)
            : base(viewCreator, menusManager)
        {
        }
    }
}
