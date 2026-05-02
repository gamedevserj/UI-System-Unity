using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    /// <summary>
    /// Rebindable button view.
    /// </summary>
    public class RebindableButtonView : ButtonView
    {
        [SerializeField] private Image _icon;

        /// <summary>
        /// Gets the image component.
        /// </summary>
        public Image Icon => _icon;
    }
}
