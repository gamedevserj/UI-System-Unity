using System;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    public class DeactivateObjectTransition : IViewTransition
    {

        private readonly GameObject _gameObject;

        public DeactivateObjectTransition(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public void Hide(Action onHidden, bool instant)
        {
            _gameObject.SetActive(false);
            onHidden?.Invoke();
        }

        public void Show(Action onShown, bool instant)
        {
            _gameObject.SetActive(true);
            onShown?.Invoke();
        }
    }
}
