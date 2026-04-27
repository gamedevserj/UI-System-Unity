using System;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem;
using UnityEngine.InputSystem;
using static UISystem.PhysicalInput.UIInputActions;

namespace UISystem.PhysicalInput
{
    /// <summary>
    /// Input processor.
    /// </summary>
    internal class InputProcessor : IUIActions
    {
        private readonly IMenusManager _menusManager;
        private readonly IPopupsManager<PopupResult> _popupsManager;

        private IInputReceiver _menuInputReceiver;
        private IInputReceiver _activeReceiver;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputProcessor"/> class.
        /// </summary>
        /// <param name="inputActions">Input actions.</param>
        /// <param name="menusManager">Menus manager.</param>
        /// <param name="popupsManager">Popups manager.</param>
        public InputProcessor(UIInputActions inputActions, IMenusManager menusManager, IPopupsManager<PopupResult> popupsManager)
        {
            inputActions.UI.SetCallbacks(this);
            _menusManager = menusManager;
            _menusManager.OnControllerSwitch += OnMenuControllerSwitch;
            _popupsManager = popupsManager;
            _popupsManager.OnControllerSwitch += OnPopupControllerSwitch;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="InputProcessor"/> class.
        /// </summary>
        ~InputProcessor()
        {
            _menusManager.OnControllerSwitch -= OnMenuControllerSwitch;
            _popupsManager.OnControllerSwitch -= OnPopupControllerSwitch;
        }

        private bool CanProcessActions => _activeReceiver != null && _activeReceiver.CanReceivePhysicalInput;

        /// <summary>
        /// Action to perform when cancel button is pressed.
        /// </summary>
        /// <param name="context">Callback context.</param>
        public void OnCancel(InputAction.CallbackContext context)
        {
            if (CanProcessActions && context.started)
                _activeReceiver.OnReturnButtonDown();
        }

        /// <summary>
        /// Action to perform when pause button is pressed.
        /// </summary>
        /// <param name="context">Callback context.</param>
        public void OnPause(InputAction.CallbackContext context)
        {
            if (CanProcessActions && context.started)
                _activeReceiver.OnPauseButtonDown();
        }

        private void OnPopupControllerSwitch(IInputReceiver inputReceiver)
        {
            _activeReceiver = inputReceiver ?? _menuInputReceiver;
        }

        private void OnMenuControllerSwitch(IInputReceiver inputReceiver)
        {
            _activeReceiver = _menuInputReceiver = inputReceiver;
        }
    }
}
