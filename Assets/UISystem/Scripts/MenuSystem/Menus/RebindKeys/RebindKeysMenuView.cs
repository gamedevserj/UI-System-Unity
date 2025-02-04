using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class RebindKeysMenuView : SettingsMenuView
    {

        [SerializeField] private Button moveLeft;
        [SerializeField] private Button moveLeftJoystick;
        [SerializeField] private Button moveRight;
        [SerializeField] private Button moveRightJoystick;
        [SerializeField] private Button jump;
        [SerializeField] private Button jumpJoystick;
        [SerializeField] private RectTransform panel;

        public Button MoveLeft => moveLeft;
        public Button MoveLeftJoystick => moveLeftJoystick;
        public Button MoveRight => moveRight;
        public Button MoveRightJoystick => moveRightJoystick;
        public Button Jump => jump;
        public Button JumpJoystick => jumpJoystick;
        public RectTransform Panel => panel;

        protected override IViewTransition CreateTransition()
        {
            return new FadeTransition(FadeObjectsContainer);
            //return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
            //    new ITweenableMenuElement[] { ReturnButton, ResetButton,
            //        MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick, Jump, JumpJoystick,
            //        MoveLeftLabelResizableControl, MoveRightLabelResizableControl, JumpLabelResizableControl});
        }

        protected override void PopulateFocusableElements()
        {
            //_focusableElements = new IFocusableControl[]
            //{ MoveLeft, MoveLeftJoystick, MoveRight, MoveRightJoystick, Jump, JumpJoystick, ResetButton, ReturnButton };
        }

    }
}