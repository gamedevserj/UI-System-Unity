using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Constants
{
    public static class XboxIcons
    {

        private static string ItemsFolder => "Textures/Inputs/Xbox/";

        private static readonly Dictionary<string, string> _buttons;

        static XboxIcons()
        {
            _buttons = new Dictionary<string, string>
            {
                { "dpad/left", "xbox_dpad_left" },
                { "dpad/up", "xbox_dpad_up" },
                { "dpad/right", "xbox_dpad_right" },
                { "dpad/down", "xbox_dpad_down" },
                { "buttonSouth", "xbox_button_color_a" },
                { "buttonEast", "xbox_button_color_b" },
                { "buttonWest", "xbox_button_color_x" },
                { "buttonNorth", "xbox_button_color_y" },
                
                { "leftShoulder", "xbox_lb" },// L1
                { "rightShoulder", "xbox_rb" },// R1
                { "select", "xbox_button_share" },// share
                { "start", "xbox_button_menu" },// options
                { "leftStickPress", "xbox_ls" },// L3
                { "rightStickPress", "xbox_rs" },// R3
                { "leftTrigger", "xbox_lt" }, // L2
                { "rightTrigger", "xbox_rt" }, // R2

                { "leftStick/right", "xbox_stick_l_right" },
                { "leftStick/down", "xbox_stick_l_down" },
                { "leftStick/left", "xbox_stick_l_left" },
                { "leftStick/up", "xbox_stick_l_up" },

                { "rightStick/right", "xbox_stick_r_right" },
                { "rightStick/down", "xbox_stick_r_down" },
                { "rightStick/left", "xbox_stick_r_left" },
                { "rightStick/up", "xbox_stick_r_up" },
            };
        }

        public static Sprite GetIcon(string button)
        {
            return Resources.Load<Sprite>(ItemsFolder + _buttons[button]);
        }
    }
}
