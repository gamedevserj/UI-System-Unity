using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Constants
{
    public static class PS5Icons
    {

        private static string ItemsFolder => "Textures/Inputs/PS5/";

        private static readonly Dictionary<string, string> _buttons;

        static PS5Icons()
        {
            _buttons = new Dictionary<string, string>
            {
                { "dpad/left", "playstation_dpad_left" },
                { "dpad/up", "playstation_dpad_up" },
                { "dpad/right", "playstation_dpad_right" },
                { "dpad/down", "playstation_dpad_down" },
                { "buttonSouth", "playstation_button_color_cross" },
                { "buttonEast", "playstation_button_color_circle" },
                { "buttonWest", "playstation_button_color_square" },
                { "buttonNorth", "playstation_button_color_triangle" },

                { "leftShoulder", "playstation_trigger_l1_alternative" },// L1
                { "rightShoulder", "playstation_trigger_r1_alternative" },// R1
                { "select", "playstation_button_create" },// share
                { "start", "playstation_button_options" },// options
                { "leftStickPress", "playstation_button_l3" },// L3
                { "rightStickPress", "playstation_button_r3" },// R3
                { "leftTrigger", "playstation_trigger_l2_alternative" }, // L2
                { "rightTrigger", "playstation_trigger_r2_alternative" }, // R2

                { "leftStick/right", "playstation_stick_l_right" },
                { "leftStick/down", "playstation_stick_l_down" },
                { "leftStick/left", "playstation_stick_l_left" },
                { "leftStick/up", "playstation_stick_l_up" },

                { "rightStick/right", "playstation_stick_r_right" },
                { "rightStick/down", "playstation_stick_r_down" },
                { "rightStick/left", "playstation_stick_r_left" },
                { "rightStick/up", "playstation_stick_r_up" },
            };
        }

        public static Sprite GetIcon(string button)
        {
            return Resources.Load<Sprite>(ItemsFolder + _buttons[button]);
        }
    }
}
