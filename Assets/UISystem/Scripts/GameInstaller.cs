using UISystem.Constants;
using UnityEngine;

namespace UISystem
{
    public class GameInstaller : MonoBehaviour
    {

        private void Start()
        {
            var config = new INIParser();
            config.Open(Application.persistentDataPath + ConfigData.ConfigLocation);
            GameSettings settings = new(config);

            UiInstaller.Instance.Init(settings);
        }
    }
}
