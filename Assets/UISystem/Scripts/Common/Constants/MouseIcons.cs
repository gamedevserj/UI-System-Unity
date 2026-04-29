using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Constants
{
    /// <summary>
    /// Class containing paths to mouse icons.
    /// </summary>
    public static class MouseIcons
    {
        /*
         * Names are taken from here
         * https://discussions.unity.com/t/list-of-all-inputcontrolpath/909946/10
         */
        private static readonly Dictionary<string, string> _buttons = new Dictionary<string, string>
            {
                { "leftButton", "mouse_left" },
                { "rightButton", "mouse_right" },
                { "middleButton", "mouse_scroll" },
            };

        private static string ItemsFolder => "Textures/Inputs/Keyboard/";

        /// <summary>
        /// Gets button sprite.
        /// </summary>
        /// <param name="button">Button name.</param>
        /// <returns>Sprite.</returns>
        public static Sprite GetIcon(string button)
        {
            return Resources.Load<Sprite>(ItemsFolder + _buttons[button]);
        }
    }
}
