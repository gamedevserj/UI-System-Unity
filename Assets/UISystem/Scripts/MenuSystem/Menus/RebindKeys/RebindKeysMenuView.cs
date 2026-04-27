using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;

namespace UISystem.MenuSystem.Views
{
    /// <summary>
    /// Rebind keys menu view.
    /// </summary>
    public partial class RebindKeysMenuView : SettingsMenuView
    {
        [SerializeField] private RebindableButtonView _moveLeft;
        [SerializeField] private RebindableButtonView _moveLeftJoystick;
        [SerializeField] private RebindableButtonView _moveRight;
        [SerializeField] private RebindableButtonView _moveRightJoystick;
        [SerializeField] private RebindableButtonView _jump;
        [SerializeField] private RebindableButtonView _jumpJoystick;
        [SerializeField] private RectTransform _panel;

        /// <summary>
        /// Gets move left key.
        /// </summary>
        public RebindableButtonView MoveLeft => _moveLeft;

        /// <summary>
        /// Gets joystick move left key.
        /// </summary>
        public RebindableButtonView MoveLeftJoystick => _moveLeftJoystick;

        /// <summary>
        /// Gets move right key.
        /// </summary>
        public RebindableButtonView MoveRight => _moveRight;

        /// <summary>
        /// Gets joystick move right key.
        /// </summary>
        public RebindableButtonView MoveRightJoystick => _moveRightJoystick;

        /// <summary>
        /// Gets jump key.
        /// </summary>
        public RebindableButtonView Jump => _jump;

        /// <summary>
        /// Gets joystick jump key.
        /// </summary>
        public RebindableButtonView JumpJoystick => _jumpJoystick;

        /// <inheritdoc/>
        protected override IViewTransition CreateTransition()
        {
            return new PanelSizeTransition(FadeObjectsContainer, _panel);
        }

        /// <inheritdoc/>
        protected override void SetInteractableElements()
        {
            InteractableElements = new IInteractableElement[]
            {
                MoveLeft,
                MoveLeftJoystick,
                MoveRight,
                MoveRightJoystick,
                Jump,
                JumpJoystick,
                ResetButton,
                ReturnButton,
            };
        }
    }
}
