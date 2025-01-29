using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.SettingsMenu
{
    public abstract partial class SettingsMenuView : MenuView
    {

        [SerializeField] private Button returnButton;
        [SerializeField] private Button resetButton;

        public CanvasGroup FadeObjectsContainer => canvasGroup;
        public Button ReturnButton => returnButton;
        public Button ResetButton => resetButton;

        protected override Selectable DefaultSelectedElement => ReturnButton;

    }
}