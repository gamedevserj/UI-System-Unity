using UISystem.Common.Elements;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.Core.Elements;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UISystem.MenuSystem.Controllers
{
    /// <summary>
    /// Rebind keys menu controller.
    /// </summary>
    internal class RebindKeysMenuController : SettingsMenuController<IViewCreator<RebindKeysMenuView>, RebindKeysMenuView, RebindKeysMenuModel>
    {
        private const string EllipsisPath = "Textures/Inputs/ellipsis";

        /// <summary>
        /// Initializes a new instance of the <see cref="RebindKeysMenuController"/> class.
        /// </summary>
        /// <param name="viewCreator">View creator.</param>
        /// <param name="menusManager">Menus manager.</param>
        /// <param name="model">Rebind keys menu model.</param>
        /// <param name="popupsManager">Popups manager.</param>
        public RebindKeysMenuController(
            IViewCreator<RebindKeysMenuView> viewCreator,
            IMenusManager menusManager,
            RebindKeysMenuModel model,
            IPopupsManager popupsManager)
            : base(viewCreator, menusManager, model, popupsManager)
        {
        }

        private GameActions Actions => Model.GameActions;

        /// <inheritdoc/>
        public override void OnReturnButtonDown()
        {
            if (!Model.IsRebinding)
                base.OnReturnButtonDown();
        }

        /// <inheritdoc/>
        protected override void SetupElements()
        {
            View.ReturnButton.AddOnClickListener(OnReturnButtonDown);
            View.ResetButton.AddOnClickListener(OnResetToDefaultButtonDown);

            View.MoveLeft.AddOnClickListener(() =>
            {
                OnButtonDown(View.MoveLeft, Actions.Gameplay.Left, InputsData.KeyboardEventIndex);
            });
            View.MoveLeftJoystick.AddOnClickListener(() =>
            {
                OnButtonDown(View.MoveLeftJoystick, Actions.Gameplay.Left, InputsData.JoystickEventIndex);
            });

            View.MoveRight.AddOnClickListener(() =>
            {
                OnButtonDown(View.MoveRight, Actions.Gameplay.Right, InputsData.KeyboardEventIndex);
            });
            View.MoveRightJoystick.AddOnClickListener(() =>
            {
                OnButtonDown(View.MoveRightJoystick, Actions.Gameplay.Right, InputsData.JoystickEventIndex);
            });

            View.Jump.AddOnClickListener(() =>
            {
                OnButtonDown(View.Jump, Actions.Gameplay.Jump, InputsData.KeyboardEventIndex);
            });
            View.JumpJoystick.AddOnClickListener(() =>
            {
                OnButtonDown(View.JumpJoystick, Actions.Gameplay.Jump, InputsData.JoystickEventIndex);
            });

            UpdateAllButtonViews();
        }

        /// <inheritdoc/>
        protected override void UpdateAllViewValues()
        {
            UpdateAllButtonViews();
        }

        private void UpdateButtonView(RebindableButtonView button, InputAction action, int index)
        {
            action.GetBindingDisplayString(index, out string device, out string path);

            Sprite sprite = null;
            if (index == InputsData.JoystickEventIndex)
            {
                sprite = Model.IconsType switch
                {
                    ControllerIconsType.Xbox => XboxIcons.GetIcon(path),
                    ControllerIconsType.Ps5 => PS5Icons.GetIcon(path),
                    _ => XboxIcons.GetIcon(path),
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
            if (Model.IsRebinding)
                return;
            button.Icon.sprite = Resources.Load<Sprite>(EllipsisPath);
            View.SetLastSelectedElement(button);
            SwitchInteractability(false);

            Model.StartRebinding(action, index, () =>
            {
                SwitchRebindingButtonInteractability(button, true);
                UpdateButtonView(button, action, index);
                SwitchInteractability(true);
            });
        }

        private void SwitchRebindingButtonInteractability(IInteractableElement button, bool allowFocus)
        {
            SwitchInteractability(allowFocus);
            if (allowFocus)
            {
                View.SetLastSelectedElement(button);
            }
        }

        private void UpdateAllButtonViews()
        {
            UpdateButtonView(View.MoveLeft, Actions.Gameplay.Left, InputsData.KeyboardEventIndex);
            UpdateButtonView(View.MoveLeftJoystick, Actions.Gameplay.Left, InputsData.JoystickEventIndex);

            UpdateButtonView(View.MoveRight, Actions.Gameplay.Right, InputsData.KeyboardEventIndex);
            UpdateButtonView(View.MoveRightJoystick, Actions.Gameplay.Right, InputsData.JoystickEventIndex);

            UpdateButtonView(View.Jump, Actions.Gameplay.Jump, InputsData.KeyboardEventIndex);
            UpdateButtonView(View.JumpJoystick, Actions.Gameplay.Jump, InputsData.JoystickEventIndex);
        }
    }
}
