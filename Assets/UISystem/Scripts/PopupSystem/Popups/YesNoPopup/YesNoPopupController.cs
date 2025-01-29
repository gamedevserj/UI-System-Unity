using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;
using UnityEngine;

namespace UISystem.PopupSystem.Popups.Controllers
{
    internal class YesNoPopupController : PopupControllerBase<IViewCreator<YesNoPopupView>, YesNoPopupView>
    {
        public override PopupType Type => PopupType.YesNo;
        public override PopupResult PressedReturnPopupResult => PopupResult.No;

        public YesNoPopupController(IViewCreator<YesNoPopupView> viewCreator, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
        { }

        protected override void SetupElements()
        {
            _view.YesButton.onClick.AddListener(() => _popupsManager.HidePopup(PopupResult.Yes));
            _view.NoButton.onClick.AddListener(() => _popupsManager.HidePopup(PopupResult.No));
        }

    }
}