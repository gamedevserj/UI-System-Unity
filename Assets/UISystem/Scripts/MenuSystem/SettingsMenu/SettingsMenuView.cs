using UISystem.Common.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.SettingsMenu
{
    public abstract partial class SettingsMenuView : MenuView
    {

        [SerializeField] private ButtonView returnButton;
        [SerializeField] private ButtonView resetButton;

        public ButtonView ReturnButton => returnButton;
        public ButtonView ResetButton => resetButton;

        protected override Selectable DefaultSelectedElement => ReturnButton.Button;

    }
}