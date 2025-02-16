using UISystem.Common.Elements;
using UISystem.Common.Enums;
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
        private const string EllipsisPath = "Textures/Inputs/ellipsis";

        public RebindKeysMenuController(IViewCreator<RebindKeysMenuView> viewCreator, RebindKeysMenuModel model,
            IMenusManager<MenuType> menusManager, IPopupsManager<PopupType, PopupResult> popupsManager)
            : base(viewCreator, model, menusManager, popupsManager)
        { }

        public override void OnReturnButtonDown()
        {
            if (!_model.IsRebinding)
                base.OnReturnButtonDown();
        }

        private void UpdateButtonView(RebindableButtonView button, InputAction action, int index)
        {
            action.GetBindingDisplayString(index, out string device, out string path);

            Sprite sprite = null;
            if (index == InputsData.JoystickEventIndex)
            {
                sprite = _model.IconsType switch
                {
                    ControllerIconsType.Xbox => XboxIcons.GetIcon(path),
                    ControllerIconsType.Ps5 => PS5Icons.GetIcon(path),
                    _ => XboxIcons.GetIcon(path)
                };
                
            }
            else if (index == InputsData.KeyboardEventIndex)
            {
                if (device == InputsData.KeyboardDevice)
                {
                    sprite = KeyboardIcons.GetIcon(path);
                }
                else if (device == InputsData.MouseDevice)
                {
                    sprite = MouseIcons.GetIcon(path);
                }
            }
            button.Icon.sprite = sprite;
        }

        private void OnButtonDown(RebindableButtonView button, InputAction action, int index)
        {
            if (_model.IsRebinding)
                return;
            button.Icon.sprite = Resources.Load<Sprite>(EllipsisPath); ;
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