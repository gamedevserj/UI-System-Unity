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

        public async Task Hide(bool instant = false)
        {
            _gameObject.SetActive(false);
        }

        public async Task Show(bool instant = false)
        {
            _gameObject.SetActive(true);
        }
    }
}
