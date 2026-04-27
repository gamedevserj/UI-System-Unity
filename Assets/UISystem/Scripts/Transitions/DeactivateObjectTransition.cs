using System.Threading.Tasks;
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

        public Task Hide(bool instant = false)
        {
            _gameObject.SetActive(false);
            return Task.CompletedTask;
        }

        public Task Show(bool instant = false)
        {
            _gameObject.SetActive(true);
            return Task.CompletedTask;
        }
    }
}
