﻿using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;
using UnityEngine;

namespace UISystem.PopupSystem.Popups.Controllers
{
    internal class YesPopupController : PopupControllerBase<IViewCreator<YesPopupView>, YesPopupView>
    {

        public override PopupType Type => PopupType.Yes;
        public override PopupResult PressedReturnPopupResult => PopupResult.Yes;

        public YesPopupController(IViewCreator<YesPopupView> viewCreator, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
        { }

        protected override void SetupElements()
        {
            _view.YesButton.onClick.AddListener(() => _popupsManager.HidePopup(PopupResult.Yes));
        }

    }
}