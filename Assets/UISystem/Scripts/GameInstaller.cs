using UISystem.Constants;
using UISystem.PhysicalInput;
using UnityEngine;

namespace UISystem
{
    public class GameInstaller : MonoBehaviour
    {

        private void Start()
        {
            var config = new INIParser();
            config.Open(Application.persistentDataPath + ConfigData.ConfigLocation);
            var actions = new GameActions();
            GameSettings settings = new(config, actions);

            UiInstaller.Instance.Init(settings);
        }
    }
}
