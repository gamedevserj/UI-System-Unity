using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Constants
{
    public static class MouseIcons
    {

        private static string ItemsFolder => "Textures/Inputs/Keyboard/";

        private static readonly Dictionary<string, string> _buttons;

        static MouseIcons()
        {
            _buttons = new Dictionary<string, string>
            {
                { "leftButton", "mouse_left" },
                { "rightButton", "mouse_right" },
                { "middleButton", "mouse_scroll" },
            };
        }

        public static Sprite GetIcon(string button)
        {
            return Resources.Load<Sprite>(ItemsFolder + _buttons[button]);
        }

    }
}
/*
 * Names are taken from here
 * https://discussions.unity.com/t/list-of-all-inputcontrolpath/909946/10
 */