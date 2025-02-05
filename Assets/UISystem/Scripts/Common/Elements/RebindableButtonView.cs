using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.Common.Elements
{
    public class RebindableButtonView : MonoBehaviour
    {

        [SerializeField] private TextMeshProUGUI label;
        private Button button;

        public Button Button
        {
            get
            {
                if (button == null) { button = GetComponent<Button>(); }
                return button;
            }
        }

        public TextMeshProUGUI Label => label;

    }
}
