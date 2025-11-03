using PrimeTween;
using System;
using UnityEngine.UI;

namespace UISystem.ScreenFade
{
    public class ScreenFadeManager
    {

        private const float Duration = 0.5f;

        private bool _isFading;
        private readonly Image _image;

        public ScreenFadeManager(Image image)
        {
            _image = image;
        }

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