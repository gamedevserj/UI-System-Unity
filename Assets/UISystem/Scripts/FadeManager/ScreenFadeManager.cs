using DG.Tweening;
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

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_image.DOFade(1, Duration).OnComplete(() =>
            {
                onFadeOutComplete?.Invoke();
            })).Append(_image.DOFade(0, Duration).OnComplete(() =>
            {
                _isFading = false;
                _image.enabled = false;
            })).Play();
        }
    }
}