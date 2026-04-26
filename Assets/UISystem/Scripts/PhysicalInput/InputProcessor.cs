using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem;
using UnityEngine.InputSystem;
using static UISystem.PhysicalInput.UIInputActions;

namespace UISystem.PhysicalInput
{
    internal class InputProcessor : IUIActions
    {

        private IInputReceiver _menuInputReceiver;
        private IInputReceiver _activeReceiver;

        private readonly IMenusManager _menusManager;
        private readonly IPopupsManager<PopupResult> _popupsManager;

        private bool CanProcessActions => _activeReceiver != null && _activeReceiver.CanReceivePhysicalInput;

        public InputProcessor(UIInputActions inputActions, IMenusManager menusManager, IPopupsManager<PopupResult> popupsManager)
        {
            inputActions.UI.SetCallbacks(this);
            _menusManager = menusManager;
            _menusManager.OnControllerSwitch += OnMenuControllerSwitch;
            _popupsManager = popupsManager;
            _popupsManager.OnControllerSwitch += OnPopupControllerSwitch;
        }

        ~InputProcessor()
        {
            _menusManager.OnControllerSwitch -= OnMenuControllerSwitch;
            _popupsManager.OnControllerSwitch -= OnPopupControllerSwitch;
        }

        private void OnPopupControllerSwitch(IInputReceiver inputReceiver)
        {
            _activeReceiver = inputReceiver ?? _menuInputReceiver;
        }

        private void OnMenuControllerSwitch(IInputReceiver inputReceiver)
        {
            _activeReceiver = _menuInputReceiver = inputReceiver;
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (CanProcessActions && context.started)
                _activeReceiver.OnReturnButtonDown();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (CanProcessActions && context.started)
                _activeReceiver.OnPauseButtonDown();
        }
    }
}