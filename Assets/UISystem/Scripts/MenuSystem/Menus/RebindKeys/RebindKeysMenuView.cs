using UISystem.Common.Elements;
using UISystem.Core.Transitions;
using UISystem.MenuSystem.SettingsMenu;
using UISystem.Transitions;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.Views
{
    public partial class RebindKeysMenuView : SettingsMenuView
    {

        [SerializeField] private RebindableButtonView moveLeft;
        [SerializeField] private RebindableButtonView moveLeftJoystick;
        [SerializeField] private RebindableButtonView moveRight;
        [SerializeField] private RebindableButtonView moveRightJoystick;
        [SerializeField] private RebindableButtonView jump;
        [SerializeField] private RebindableButtonView jumpJoystick;
        [SerializeField] private RectTransform panel;

        public RebindableButtonView MoveLeft => moveLeft;
        public RebindableButtonView MoveLeftJoystick => moveLeftJoystick;
        public RebindableButtonView MoveRight => moveRight;
        public RebindableButtonView MoveRightJoystick => moveRightJoystick;
        public RebindableButtonView Jump => jump;
        public RebindableButtonView JumpJoystick => jumpJoystick;
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