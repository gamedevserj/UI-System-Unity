using TMPro;
using UISystem.Core.PopupSystem;
using UISystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.PopupSystem
{
    public abstract partial class PopupView : ViewBase, IPopupView
    {

        [SerializeField] protected RectTransform panel;
        [SerializeField] private TextMeshProUGUI messageLabel;

        public RectTransform Panel => panel;
        public TextMeshProUGUI Message { set => messageLabel = value; }
        public abstract Selectable DefaultSelectedElement { get; }

        public override void FocusElement()
        {
            if (DefaultSelectedElement != null)
            {
                DefaultSelectedElement.Select();
            }
        }

        public void SetMessage(string message)
        {
            messageLabel.text = message;
        }
    }
}