using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    public class RebindableButtonView : ButtonView
    {

        [SerializeField] private TextMeshProUGUI label;

        public TextMeshProUGUI Label => label;

    }
}
