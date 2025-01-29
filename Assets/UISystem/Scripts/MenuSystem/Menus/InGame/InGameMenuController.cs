using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UnityEngine;

namespace UISystem.MenuSystem.Controllers
{
    internal class InGameMenuController : MenuControllerBase<IViewCreator<InGameMenuView>, InGameMenuView>
    {

        public override MenuType Type => MenuType.InGame;

        public InGameMenuController(IViewCreator<InGameMenuView> viewCreator, IMenuModel model, IMenusManager<KeyCode, MenuType> menusManager) : base(viewCreator, model, menusManager)
        { }

        public override void OnPauseButtonDown()
        {
            _menusManager.ShowMenu(MenuType.Pause);
        }

        protected override void SetupElements()
        {

        }

    }
}