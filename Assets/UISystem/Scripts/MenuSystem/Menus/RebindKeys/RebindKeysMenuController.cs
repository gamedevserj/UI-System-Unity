using UISystem.Common.Elements;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UISystem.PopupSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Controllers
{
    internal class RebindKeysMenuController : SettingsMenuController<IViewCreator<RebindKeysMenuView>, RebindKeysMenuView, RebindKeysMenuModel>
    {

        public override MenuType Type => MenuType.RebindKeys;

        private GameActions Actions => GameSettings.Actions;

        public RebindKeysMenuController(IViewCreator<RebindKeysMenuView> viewCreator, RebindKeysMenuModel model,
            IMenusManager<KeyCode, MenuType> menusManager, IPopupsManager<KeyCode, PopupType, PopupResult> popupsManager)
            : base(viewCreator, model, menusManager, popupsManager)
        { }

        public override void OnAnyButtonDown(KeyCode inputEvent)
        {
            //if (_model.IsRebinding)
            //    _model.RebindKey(inputEvent);
        }
        public override void OnReturnButtonDown()
        {
            if (!_model.IsRebinding)
                base.OnReturnButtonDown();
        }

        private static void UpdateButtonView(RebindableButtonView button, InputAction action, int index)
        {
            button.Label.text = action.bindings[index].ToDisplayString();
            if (index == InputsData.JoystickEventIndex)
            {
                //TODO: assign icon
            }
            else if (index == InputsData.KeyboardEventIndex)
            {
                //TODO: assign icon
            }
        }

        private void OnButtonDown(RebindableButtonView button, InputAction action, int index)
        {
            button.Label.text = "...";
            _view.SetLastSelectedElement(button.Button);
            SwitchFocusAvailability(false);

            _model.StartRebinding(action, index, () =>
            {
                SwitchRebindingButtonFocusability(button.Button, true);
                UpdateButtonView(button, action, index);
                SwitchFocusAvailability(true);
            });
        }

        private void SwitchRebindingButtonFocusability(Button button, bool allowFocus)
        {
            SwitchFocusAvailability(allowFocus);
            if (allowFocus)
            {
                _view.SetLastSelectedElement(button);
            }
        }

        protected override void SetupElements()
        {
            _view.ReturnButton.onClick.AddListener(OnReturnButtonDown);
            _view.ResetButton.onClick.AddListener(OnResetToDefaultButtonDown);

            _view.MoveLeft.Button.onClick.AddListener(() => 
            { OnButtonDown(_view.MoveLeft, Actions.Gameplay.Left, InputsData.KeyboardEventIndex); });
            _view.MoveLeftJoystick.Button.onClick.AddListener(() => 
            { OnButtonDown(_view.MoveLeftJoystick, Actions.Gameplay.Left, InputsData.JoystickEventIndex); });

            _view.MoveRight.Button.onClick.AddListener(() =>
            { OnButtonDown(_view.MoveRight, Actions.Gameplay.Right, InputsData.KeyboardEventIndex); });
            _view.MoveRightJoystick.Button.onClick.AddListener(() =>
            { OnButtonDown(_view.MoveRightJoystick, Actions.Gameplay.Right, InputsData.JoystickEventIndex); });

            _view.Jump.Button.onClick.AddListener(() =>
            { OnButtonDown(_view.Jump, Actions.Gameplay.Jump, InputsData.KeyboardEventIndex); });
            _view.JumpJoystick.Button.onClick.AddListener(() =>
            { OnButtonDown(_view.JumpJoystick, Actions.Gameplay.Jump, InputsData.JoystickEventIndex); });

            UpdateAllButtonViews();
        }

        private void UpdateAllButtonViews()
        {
            UpdateButtonView(_view.MoveLeft, Actions.Gameplay.Left, InputsData.KeyboardEventIndex);
            UpdateButtonView(_view.MoveLeftJoystick, Actions.Gameplay.Left, InputsData.JoystickEventIndex);

            UpdateButtonView(_view.MoveRight, Actions.Gameplay.Right, InputsData.KeyboardEventIndex);
            UpdateButtonView(_view.MoveRightJoystick, Actions.Gameplay.Right, InputsData.JoystickEventIndex);

            UpdateButtonView(_view.Jump, Actions.Gameplay.Jump, InputsData.KeyboardEventIndex);
            UpdateButtonView(_view.JumpJoystick, Actions.Gameplay.Jump, InputsData.JoystickEventIndex);
        }

        protected override void ResetViewToDefault()
        {
            UpdateAllButtonViews();
        }

    }
}