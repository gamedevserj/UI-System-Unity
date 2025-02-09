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
            IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager)
            : base(viewCreator, model, menusManager, popupsManager)
        { }

        public override void OnReturnButtonDown()
        {
            if (!_model.IsRebinding)
                base.OnReturnButtonDown();
        }

        private static void UpdateButtonView(RebindableButtonView button, InputAction action, int index)
        {
            button.Label.text = action.bindings[index].ToDisplayString();
            action.GetBindingDisplayString(index, out string device, out string path);

            if (index == InputsData.JoystickEventIndex)
            {
                //TODO: assign icon
                Sprite sprite = null;
                switch (GameSettings.ControllerIconsType)
                {
                    case Common.Enums.ControllerIconsType.Xbox:
                        sprite = XboxIcons.GetIcon(path);
                        break;
                    case Common.Enums.ControllerIconsType.Ps5:
                        sprite = PS5Icons.GetIcon(path);
                        break;
                    default:
                        break;
                }
                button.Icon.sprite = sprite;
            }
            else if (index == InputsData.KeyboardEventIndex)
            {
                //TODO: assign icon
            }
        }

        private void OnButtonDown(RebindableButtonView button, InputAction action, int index)
        {
            if (_model.IsRebinding)
                return;
            button.Label.text = "...";
            _view.SetLastSelectedElement(button.Button);
            SwitchInteractability(false);

            _model.StartRebinding(action, index, () =>
            {
                SwitchRebindingButtonFocusability(button.Button, true);
                UpdateButtonView(button, action, index);
                SwitchInteractability(true);
            });
        }

        private void SwitchRebindingButtonFocusability(Button button, bool allowFocus)
        {
            SwitchInteractability(allowFocus);
            if (allowFocus)
            {
                _view.SetLastSelectedElement(button);
            }
        }

        protected override void SetupElements()
        {
            _view.ReturnButton.AddListener(OnReturnButtonDown);
            _view.ResetButton.AddListener(OnResetToDefaultButtonDown);

            _view.MoveLeft.AddListener(() => 
            { OnButtonDown(_view.MoveLeft, Actions.Gameplay.Left, InputsData.KeyboardEventIndex); });
            _view.MoveLeftJoystick.AddListener(() => 
            { OnButtonDown(_view.MoveLeftJoystick, Actions.Gameplay.Left, InputsData.JoystickEventIndex); });

            _view.MoveRight.AddListener(() =>
            { OnButtonDown(_view.MoveRight, Actions.Gameplay.Right, InputsData.KeyboardEventIndex); });
            _view.MoveRightJoystick.AddListener(() =>
            { OnButtonDown(_view.MoveRightJoystick, Actions.Gameplay.Right, InputsData.JoystickEventIndex); });

            _view.Jump.AddListener(() =>
            { OnButtonDown(_view.Jump, Actions.Gameplay.Jump, InputsData.KeyboardEventIndex); });
            _view.JumpJoystick.AddListener(() =>
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