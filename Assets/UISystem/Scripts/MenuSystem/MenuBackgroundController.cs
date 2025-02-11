using DG.Tweening;
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
            _background.DOFade(1, Duration).Play();
        }

        public void HideBackground(bool instant = false)
        {
            if (instant)
            {
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 0);
                _background.enabled = false;
                return;
            }
            _background.DOFade(0, Duration).OnComplete(()=> _background.enabled = false).Play();
        }

    }
}