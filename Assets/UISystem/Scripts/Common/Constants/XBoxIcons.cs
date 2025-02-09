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
            // L1
            { "leftShoulder", "xbox_lb" },
            // R1
            { "rightShoulder", "xbox_rb" },
            // share
            { "select", "xbox_button_share" },
            // options
            { "start", "xbox_button_menu" },
            // L3
            { "leftStickPress", "xbox_ls" },
            // R3
            { "rightStickPress", "xbox_rs" },
            { "leftTrigger", "xbox_lt" },
            { "rightTrigger", "xbox_rt" },
            { "leftStick/right", "xbox_stick_l_right" },
            { "leftStick/down", "xbox_stick_l_down" },
            { "leftStick/left", "xbox_stick_l_left" },
            { "leftStick/up", "xbox_stick_l_up" },

            { "rightStick/right", "xbox_stick_r_right" },
            { "rightStick/down", "xbox_stick_r_down" },
            { "rightStick/left", "xbox_stick_r_left" },
            { "rightStick/up", "xbox_stick_r_up" },
        };

        //    _triggersPositive = new Dictionary<JoyAxis, string>
        //{
        //    {JoyAxis.TriggerLeft, "xbox_lt.png" },
        //    {JoyAxis.TriggerRight, "xbox_rt.png" },
        //    {JoyAxis.LeftX, "xbox_stick_l_right.png" },
        //    {JoyAxis.LeftY, "xbox_stick_l_down.png" },
        //    {JoyAxis.RightX, "xbox_stick_r_right.png" },
        //    {JoyAxis.RightY, "xbox_stick_r_down.png" },
        //};

        //    _triggersNegative = new Dictionary<JoyAxis, string>
        //{
        //    {JoyAxis.LeftX, "xbox_stick_l_left.png" },
        //    {JoyAxis.LeftY, "xbox_stick_l_up.png" },
        //    {JoyAxis.RightX, "xbox_stick_r_left.png" },
        //    {JoyAxis.RightY, "xbox_stick_r_up.png" },
        //};

        }

        public static Sprite GetIcon(string button)
        {
            return Resources.Load<Sprite>(ItemsFolder + _buttons[button]);
        }

        //public static string GetIcon(JoyAxis axis, float positive = 1)
        //{
        //    string icon = positive > 0 ? _triggersPositive[axis] : _triggersNegative[axis];
        //    return ItemsFolder + icon;
        //}

    }
}
