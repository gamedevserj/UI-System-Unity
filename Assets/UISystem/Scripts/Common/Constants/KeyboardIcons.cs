using System.Collections.Generic;
using UnityEngine;

namespace UISystem.Constants
{
    public static class KeyboardIcons
    {
        private static string ItemsFolder => "Textures/Inputs/Keyboard/";

        private static readonly Dictionary<string, string> _buttons;

        static KeyboardIcons()
        {
            _buttons = new Dictionary<string, string>
            {
                { "escape", "keyboard_escape" },
                { "space", "keyboard_space" },
                { "enter", "keyboard_enter" },
                { "tab", "keyboard_tab_icon" },
                { "backquote", "" },
                { "quote", "keyboard_quote" },
                { "semicolon", "keyboard_semicolon" },
                { "comma", "keyboard_comma" },
                { "period", "keyboard_period" },
                { "slash", "keyboard_slash_forward" },
                { "backslash", "keyboard_slash_back" },
                { "leftBracket", "keyboard_bracket_open" },
                { "rightBracket", "keyboard_bracket_close" },
                { "minus", "keyboard_minus" },
                { "upArrow", "keyboard_arrow_up" },
                { "downArrow", "keyboard_arrow_down" },
                { "leftArrow", "keyboard_arrow_left" },
                { "rightArrow", "keyboard_arrow_right" },
                { "a", "keyboard_a" },
                { "b", "keyboard_b" },
                { "c", "keyboard_c" },
                { "d", "keyboard_d" },
                { "e", "keyboard_e" },
                { "f", "keyboard_f" },
                { "g", "keyboard_g" },
                { "h", "keyboard_h" },
                { "i", "keyboard_i" },
                { "j", "keyboard_j" },
                { "k", "keyboard_k" },
                { "l", "keyboard_l" },
                { "m", "keyboard_m" },
                { "n", "keyboard_n" },
                { "o", "keyboard_o" },
                { "p", "keyboard_p" },
                { "q", "keyboard_q" },
                { "r", "keyboard_r" },
                { "s", "keyboard_s" },
                { "t", "keyboard_t" },
                { "u", "keyboard_u" },
                { "w", "keyboard_w" },
                { "x", "keyboard_x" },
                { "y", "keyboard_y" },
                { "z", "keyboard_z" },
                { "1", "keyboard_1" },
                { "2", "keyboard_2" },
                { "3", "keyboard_3" },
                { "4", "keyboard_4" },
                { "5", "keyboard_5" },
                { "6", "keyboard_6" },
                { "7", "keyboard_7" },
                { "8", "keyboard_8" },
                { "9", "keyboard_9" },
                { "0", "keyboard_0" },
                { "leftShift", "keyboard_shift" },
                { "rightShift", "keyboard_shift" },
                { "shift", "keyboard_shift" },
                { "leftAlt", "keyboard_alt" },
                { "rightAlt", "keyboard_alt" },
                { "alt", "keyboard_alt" },
                { "leftCtrl", "keyboard_ctrl" },
                { "rightCtrl", "keyboard_ctrl" },
                { "ctrl", "keyboard_ctrl" },
                { "leftMeta", "" },
                { "rightMeta", "" },
                { "contextMenu", "" },
                { "backspace", "keyboard_backspace" },
                { "pageDown", "keyboard_page_down" },
                { "pageUp", "keyboard_page_up" },
                { "home", "keyboard_home" },
                { "end", "keyboard_end" },
                { "insert", "keyboard_insert" },
                { "delete", "keyboard_delete" },
                { "capsLock", "keyboard_capslock" },
                { "numLock", "keyboard_numlock" },
                { "printScreen", "keyboard_printscreen" },
                { "scrollLock", "" },
                { "pause", "" },
                { "numpadEnter", "keyboard_numpad_enter" },
                { "numpadDivide", "keyboard_slash_forward" },
                { "numpadMultiply", "keyboard_asterisk" },
                { "numpadPlus", "keyboard_plus" },
                { "numpadMinus", "keyboard_minus" },
                { "numpadPeriod", "keyboard_period" },
                { "numpadEquals", "keyboard_equals" },
                { "numpad1", "keyboard_1" },
                { "numpad2", "keyboard_2" },
                { "numpad3", "keyboard_3" },
                { "numpad4", "keyboard_4" },
                { "numpad5", "keyboard_5" },
                { "numpad6", "keyboard_6" },
                { "numpad7", "keyboard_7" },
                { "numpad8", "keyboard_8" },
                { "numpad9", "keyboard_9" },
                { "numpad0", "keyboard_0" },
                { "f1", "keyboard_f1" },
                { "f2", "keyboard_f2" },
                { "f3", "keyboard_f3" },
                { "f4", "keyboard_f4" },
                { "f5", "keyboard_f5" },
                { "f6", "keyboard_f6" },
                { "f7", "keyboard_f7" },
                { "f8", "keyboard_f8" },
                { "f9", "keyboard_f9" },
                { "f10", "keyboard_f10" },
                { "f11", "keyboard_f11" },
                { "f12", "keyboard_f12" },
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
