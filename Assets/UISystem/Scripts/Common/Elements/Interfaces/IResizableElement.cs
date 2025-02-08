using UnityEngine;

namespace UISystem.Common.Elements
{
    public interface IResizableElement
    {
        RectTransform ButtonTransform { get; }
        RectTransform Resizable { get; }
    }
}