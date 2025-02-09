using UnityEngine;

namespace UISystem.Common.Elements
{
    public interface IResizableElement
    {
        RectTransform Reference { get; }
        RectTransform Resizable { get; }
    }
}