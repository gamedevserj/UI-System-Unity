using System.Threading.Tasks;
using UISystem.Core.Transitions;
using UnityEngine;

namespace UISystem.Transitions
{
    /// <summary>
    /// Simple transition that just deactivates object.
    /// </summary>
    public class DeactivateObjectTransition : IViewTransition
    {
        private readonly GameObject _gameObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateObjectTransition"/> class.
        /// </summary>
        /// <param name="gameObject">Game object to deactivate.</param>
        public DeactivateObjectTransition(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        /// <inheritdoc/>
        public Task Hide(bool instant = false)
        {
            _gameObject.SetActive(false);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task Show(bool instant = false)
        {
            _gameObject.SetActive(true);
            return Task.CompletedTask;
        }
    }
}
