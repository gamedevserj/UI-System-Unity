using System;
using System.Threading.Tasks;
using UISystem.Core.MenuSystem;
using UnityEngine.InputSystem;

namespace UISystem.MenuSystem.Models
{
    public class RebindKeysMenuModel : ISettingsMenuModel
    {

        private bool _isRebinding;
        private readonly GameSettings _settings;
        private InputActionRebindingExtensions.RebindingOperation _rebindOperation;

        public bool IsRebinding => _isRebinding;

        public bool HasUnappliedSettings => false;

        public RebindKeysMenuModel(GameSettings settings)
        {
            _settings = settings;
        }

        public void ResetToDefault()
        {
            _settings.ResetInputMapToDefault();
        }

        /// <summary>
        /// Starts the process of rebinding a key
        /// </summary>
        /// <param name="action">Action to rebind</param>
        /// <param name="index">0 - keyboard, 1 - joystick</param>
        /// <param name="onFinishedRebinding"></param>
        public void StartRebinding(InputAction action, int index = 0, Action onFinishedRebinding = null)
        {
            // the code for rebinding is taken from the official sample in input asset
            _rebindOperation?.Cancel(); // Will null out _rebindOperation.

            void CleanUp()
            {
                _rebindOperation?.Dispose();
                _rebindOperation = null;
            }

            async void FinishRebinding()
            {
                onFinishedRebinding?.Invoke();
                CleanUp();
                // need to delay if player presses return button
                await Task.Delay(100);
                _isRebinding = false;
            }

            // Configure the rebind.
            _rebindOperation = action.PerformInteractiveRebinding(index)
                .WithCancelingThrough("asdfg")// overwriting default cancel
                .OnPotentialMatch(operation =>
                {
                    // allows to cancel with either button
                    // doesn't allow cancelling keyboard rebind with gamepad's start button
                    // solution is to check for input in update and cancel it from there if one these buttons was pressed
                    // https://discussions.unity.com/t/impossible-to-get-cancelling-of-control-rebind-working-with-both-keyboard-and-gamepad/1576251/3
                    if (operation.selectedControl.name == "escape" ||
                        operation.selectedControl.name == "start")
                    {
                        operation.Cancel();
                        return;
                    }
                })
                .OnCancel(operation => { FinishRebinding(); })
                .OnComplete(operation => 
                {
                    _settings.SaveInputKeys();
                    FinishRebinding(); 
                });

            _rebindOperation.Start();
            _isRebinding = true;
        }

        public void SaveSettings()
        {
            // is not implemented in this setup
            // saving happens when player presses a key
            // if you want to change it - store the actions that player tried to rebind and save/discard them when the button is pressed
        }

        public void DiscardChanges()
        {
            // is not implemented in this setup
            // saving happens when player presses a key
            // if you want to change it - store the actions that player tried to rebind and save/discard them when the button is pressed
        }
    }
}