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

        private bool CanProcessActions => _activeReceiver != null && _activeReceiver.CanReceivePhysicalInput;

        public InputProcessor(UIInputActions inputActions)
        {
            inputActions.UI.SetCallbacks(this);
            MenusManager.OnControllerSwitch += OnMenuControllerSwitch;
            PopupsManager<PopupResult>.OnControllerSwitch += OnPopupControllerSwitch;
        }

        ~InputProcessor()
        {
            MenusManager.OnControllerSwitch -= OnMenuControllerSwitch;
            PopupsManager<PopupResult>.OnControllerSwitch -= OnPopupControllerSwitch;
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