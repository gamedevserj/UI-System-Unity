using System.Threading.Tasks;
using PrimeTween;
using UnityEngine.UI;

namespace UISystem.ScreenFade
{
    /// <summary>
    /// Class to manage screen fading.
    /// </summary>
    public class ScreenFadeManager
    {
        private const float Duration = 0.5f;

        private readonly Image _image;
        private bool _isFading;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenFadeManager"/> class.
        /// </summary>
        /// <param name="image">Image to fade.</param>
        public ScreenFadeManager(Image image)
        {
            _image = image;
        }

        /// <summary>
        /// Fades screen out.
        /// </summary>
        public async Task FadeOut()
        {
            if (_isFading)
                return;

            _isFading = true;
            _image.enabled = true;
            await Tween.Alpha(_image, 1, Duration);
        }

        /// <summary>
        /// Fades screen in.
        /// </summary>
        public async Task FadeIn()
        {
            await Tween.Alpha(_image, 0, Duration);
            _isFading = false;
            _image.enabled = false;
        }
    }
}
