using UISystem.Common.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem.SettingsMenu
{
    /// <summary>
    /// Base class for menu controlling game settings.
    /// </summary>
    public abstract partial class SettingsMenuView : MenuView
    {
        [SerializeField] private ButtonView _returnButton;
        [SerializeField] private ButtonView _resetButton;

        /// <summary>
        /// Gets return button.
        /// </summary>
        public ButtonView ReturnButton => _returnButton;

        /// <summary>
        /// Gets reset to default button.
        /// </summary>
        public ButtonView ResetButton => _resetButton;

        /// <inheritdoc/>
        protected override Selectable DefaultSelectedElement => ReturnButton.Button;
    }
}
