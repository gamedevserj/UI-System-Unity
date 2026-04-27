using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MenuSystem
{
    /// <summary>
    /// Menu background controller.
    /// </summary>
    public class MenuBackgroundController
    {
        private const float Duration = 0.1f;
        private readonly Image _background;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuBackgroundController"/> class.
        /// </summary>
        /// <param name="background">Background image.</param>
        public MenuBackgroundController(Image background)
        {
            _background = background;
        }

        /// <summary>
        /// Shows background.
        /// </summary>
        /// <param name="instant">Whether transition should happen instantly.</param>
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

        /// <summary>
        /// Hides background.
        /// </summary>
        /// <param name="instant">Whether transition should happen instantly.</param>
        public void HideBackground(bool instant = false)
        {
            if (instant)
            {
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 0);
                _background.enabled = false;
                return;
            }

            Tween.Alpha(_background, 0, Duration).OnComplete(target: this, target => target._background.enabled = false);
        }
    }
}
