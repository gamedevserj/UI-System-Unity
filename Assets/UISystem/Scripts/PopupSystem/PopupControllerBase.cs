using UISystem.Core.PopupSystem;
using UISystem.Core.Views;

namespace UISystem.PopupSystem
{
    // just a base class to adapt generic controller 
    // so that there is no need to specify enums
    internal abstract class PopupControllerBase<TViewCreator, TView> : PopupController<TViewCreator, TView, PopupResult>
        where TViewCreator : IViewCreator<TView>
        where TView : IPopupView
    {
        protected PopupControllerBase(TViewCreator viewCreator, IPopupsManager<PopupResult> popupsManager) : base(viewCreator, popupsManager)
        {
        }
    }
}