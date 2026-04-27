using System;
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
        /// <param name="onFadeOutComplete">Action to perform when screen is finished fading out.</param>
        public void FadeOut(Action onFadeOutComplete = null)
        {
            if (_isFading)
                return;

            _isFading = true;
            _image.enabled = true;

            Sequence.Create()
                .Chain(Tween.Alpha(_image, 1, Duration).OnComplete(() => onFadeOutComplete?.Invoke()))
                .Chain(Tween.Alpha(_image, 0, Duration).OnComplete(target: this, target =>
                {
                    target._isFading = false;
                    target._image.enabled = false;
                }));
        }
    }
}