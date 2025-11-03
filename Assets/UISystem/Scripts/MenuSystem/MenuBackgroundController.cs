using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    public class MenuBackgroundController
    {

        private const float Duration = 0.1f;

        private readonly Image _background;

        public MenuBackgroundController(Image background)
        {
            _background = background;
        }

        // in case some menus need to have different background color
        public void SetBackgroundColor(Color color) => _background.color = color;

        public void ShowBackground(bool instant = false)
        {
            _background.enabled = true;
            if (instant)
            {
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 1);
                return;
            }
            Tween.Alpha(_background, 1, Duration);
        }

        public void HideBackground(bool instant = false)
        {
            if (instant)
            {
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 0);
                _background.enabled = false;
                return;
            }
            Tween.Alpha(_background, 1, Duration).OnComplete(target: this, target => target._background.enabled = false);
        }

    }
}