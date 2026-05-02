using UnityEngine;

namespace UISystem.Common.Elements
{
    /// <summary>
    /// Defines the contract for resizable element.
    /// </summary>
    public interface IResizableElement
    {
        /// <summary>
        /// Gets the reference for resizable element.
        /// </summary>
        RectTransform Reference { get; }

        /// <summary>
        /// Gets the RectTransform of resizable element.
        /// </summary>
        RectTransform Resizable { get; }
    }
}
