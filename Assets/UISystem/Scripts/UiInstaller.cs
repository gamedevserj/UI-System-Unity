using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;
using UnityEngine;

namespace UISystem
{
    public partial class UiInstaller : MonoBehaviour
    {

        public static UiInstaller Instance { get; private set; }

        //[Export] private TextureRect menuBackground;
        [SerializeField] private Transform menusParent;
        [SerializeField] private Transform popupsParent;
        [SerializeField] ViewBase mainMenuPrefab;
        [SerializeField] ViewBase inGameMenuPrefab;
        [SerializeField] ViewBase optionMenuPrefab;
        [SerializeField] ViewBase audioSettingsMenuPrefab;
        [SerializeField] ViewBase interfaceSettingsMenuPrefab;
        //[Export] private ScreenFadeManager screenFadeManager;

        //IInputProcessor<InputEvent> _inputProcessor;

        private void Awake()
        {
            Instance ??= this;
        }


        //public override void _Input(InputEvent inputEvent)
        //{
        //    _inputProcessor?.ProcessInput(inputEvent);
        //}

        public void Init(GameSettings settings)
        {
            //SceneTree tree = GetTree();

            //_inputProcessor = new InputProcessor();

            //var popupsManager = new PopupsManager<InputEvent, PopupType, PopupResult>();
            //var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupPath(PopupType.Yes), popupsParent);
            //var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupPath(PopupType.YesNo), popupsParent);
            //var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupPath(PopupType.YesNoCancel), popupsParent);
            //var popups = new IPopupController<InputEvent, PopupType, PopupResult>[]
            //{
            //new YesPopupController(yesPopupViewCreator, popupsManager),
            //new YesNoPopupController(yesNoPopupViewCreator, popupsManager),
            //new YesNoCancelPopupController(yesNoCancelPopupViewCreator, popupsManager)
            //};
            //popupsManager.Init(popups);

            //var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);

            var menusManager = new MenusManager<KeyCode, MenuType>();
            var mainMenuViewCreator = new ViewCreator<MainMenuView>(mainMenuPrefab, menusParent);
            var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(inGameMenuPrefab, menusParent);
            //var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuPath(MenuType.Pause), menusParent);
            var optionsViewCreator = new ViewCreator<OptionsMenuView>(optionMenuPrefab, menusParent);
            var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(audioSettingsMenuPrefab, menusParent);
            //var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuPath(MenuType.VideoSettings), menusParent);
            //var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuPath(MenuType.RebindKeys), menusParent);
            var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(interfaceSettingsMenuPrefab, menusParent);
            var menus = new IMenuController<KeyCode, MenuType>[]
            {
            new MainMenuController(mainMenuViewCreator, null, menusManager),//, tree, popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController(inGameMenuViewCreator, new InGameMenuModel(), menusManager),
            //new PauseMenuController(pauseViewCreator, null, menusManager, popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController(optionsViewCreator, null, menusManager),
            new AudioSettingsMenuController(audioSettingsViewCreator, new AudioSettingsMenuModel(settings), menusManager),//, popupsManager),
            //new VideoSettingsMenuController(videoSettingsViewCreator, new VideoSettingsMenuModel(settings), menusManager, popupsManager),
            //new RebindKeysMenuController(rebindKeysViewCreator, new RebindKeysMenuModel(settings), menusManager, popupsManager),
            new InterfaceSettingsMenuController(interfaceMenuViewCreator, new InterfaceSettingsMenuModel(settings), menusManager)//, popupsManager),
            };
            menusManager.Init(menus);
            menusManager.ShowMenu(MenuType.Main, StackingType.Clear);
        }

    }
}
