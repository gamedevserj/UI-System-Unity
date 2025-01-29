﻿using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UnityEngine;

namespace UISystem.PopupSystem
{
    // just a base class to adapt generic controller to Unity's specific parameters
    // so that there is no need to specify InputEvent for every controller
    internal abstract class PopupControllerBase<TViewCreator, TView> : PopupController<TViewCreator, KeyCode, TView, PopupType, PopupResult>
        where TViewCreator : IViewCreator<TView>
        where TView : IPopupView
    {
        protected PopupControllerBase(TViewCreator viewCreator, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
        {
        }
    }
}