using UISystem.Constants;
using UISystem.PhysicalInput;
using UISystem.Saving;
using UnityEngine;

namespace UISystem
{
    public class GameInstaller : MonoBehaviour
    {

        private void Start()
        {
            var saver = new IniSaver(ConfigData.ConfigLocation);
            var actions = new GameActions();
            GameSettings settings = new(saver, actions);

            UiInstaller.Instance.Init(settings);
        }
    }
}
