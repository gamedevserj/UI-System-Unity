using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    public class RebindableButtonView : ButtonView
    {

        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image icon;

        public TextMeshProUGUI Label => label;
        public Image Icon => icon;

    }
}
