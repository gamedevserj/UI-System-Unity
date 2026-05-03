using System.Threading.Tasks;
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
        private const float Duration = 3.1f;
        private readonly Image _background;

        private Tween _tween;

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
        public async Task ShowBackground(bool instant = false)
        {
            if (_tween.isAlive)
            {
                _tween.Stop();
            }

            _background.enabled = true;
            if (Mathf.Approximately(_background.color.a, 1))
                return;

            if (instant)
            {
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 1);
                return;
            }

            _tween = Tween.Alpha(_background, 1, Duration, ease: Ease.Linear);
            await _tween;
        }

        /// <summary>
        /// Hides background.
        /// </summary>
        /// <param name="instant">Whether transition should happen instantly.</param>
        public async Task HideBackground(bool instant = false)
        {
            if (_tween.isAlive)
            {
                _tween.Stop();
            }

            if (instant)
            {
                _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, 0);
                _background.enabled = false;
                return;
            }

            _tween = Tween.Alpha(_background, 0, Duration, ease: Ease.Linear).OnComplete(() => _background.enabled = false);
            await _tween;
        }
    }
}
