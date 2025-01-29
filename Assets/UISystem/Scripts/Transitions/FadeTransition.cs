using System;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    public class FadeTransition : IViewTransition
    {

        private readonly CanvasGroup _target;

        public FadeTransition(CanvasGroup target)
        {
            _target = target;
            //Fader.Init(_target);
        }

        public void Hide(Action onHidden, bool instant)
        {
            //if (instant)
            //{
                //_target.Modulate = new Color(_target.Modulate, 0);
                _target.alpha = 0;
                _target.interactable = false;
                _target.blocksRaycasts = false;
                onHidden?.Invoke();
                return;
            //}
            //Fader.Hide(SceneTree, _target, onHidden, instant);
        }

        public void Show(Action onShown, bool instant)
        {
            // should always hide before showing because awaiting for parameters shows menu for a split second
            //_target.Modulate = new Color(_target.Modulate, 0);
            _target.alpha = 0;
            _target.interactable = false;
            _target.blocksRaycasts = false;

            //if (instant)
            //{
                //_target.Modulate = new Color(_target.Modulate, 1);
                _target.alpha = 1;
                _target.interactable = true;
                _target.blocksRaycasts = true;
                onShown?.Invoke();
                return;
            //}
            //Fader.Show(SceneTree, _target, onShown, instant);
            //_target.alpha = 1;
            //_target.interactable = true;
            //_target.blocksRaycasts = true;
        }
    }
}