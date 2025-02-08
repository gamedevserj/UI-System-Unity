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
            _background.DOFade(1, Duration).Play();
        }

        public void HideBackground(bool instant = false)
        {
            _background.DOFade(0, Duration).OnComplete(()=> _background.enabled = false).Play();
        }

    }
}