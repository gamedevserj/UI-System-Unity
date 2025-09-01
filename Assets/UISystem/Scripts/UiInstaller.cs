using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;
using UISystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public partial class UiInstaller : MonoBehaviour
    {

        public static UiInstaller Instance { get; private set; }

        [SerializeField] private Image menuBackground;
        [SerializeField] private Image fade;
        [SerializeField] private Transform menusParent;
        [SerializeField] private Transform popupsParent;
        [SerializeField] private ViewBase mainMenuPrefab;
        [SerializeField] private ViewBase inGameMenuPrefab;
        [SerializeField] private ViewBase optionMenuPrefab;
        [SerializeField] private ViewBase audioSettingsMenuPrefab;
        [SerializeField] private ViewBase interfaceSettingsMenuPrefab;
        [SerializeField] private ViewBase videoSettingsMenuPrefab;
        [SerializeField] private ViewBase rebindKeysMenuPrefab;
        [SerializeField] private ViewBase pauseMenuPrefab;
        [SerializeField] private GameActions gameActions;

        [SerializeField] private ViewBase yesPopupPrefab;
        [SerializeField] private ViewBase yesNoPopupPrefab;
        [SerializeField] private ViewBase yesNoCancelPopupPrefab;

        private InputProcessor _inputProcessor;
        private UIInputActions _inputActions;

        private void Awake()
        {
            Instance ??= this;
            _inputActions = new UIInputActions();
            _inputProcessor = new InputProcessor(_inputActions);
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void Init(GameSettings settings)
        {
            var popupsManager = new PopupsManager<PopupResult>();
            var yesPopupViewCreator = new ViewCreator<YesPopupView>(yesPopupPrefab, popupsParent);
            var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(yesNoPopupPrefab, popupsParent);
            var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(yesNoCancelPopupPrefab, popupsParent);
            var popups = new IPopupController<PopupResult>[]
            {
                new YesPopupController(yesPopupViewCreator, popupsManager),
                new YesNoPopupController(yesNoPopupViewCreator, popupsManager),
                new YesNoCancelPopupController(yesNoCancelPopupViewCreator, popupsManager)
            };
            popupsManager.Init(popups);

            var fadeManager = new ScreenFadeManager(fade);
            var backgroundController = new MenuBackgroundController(menuBackground);

            var menusManager = new MenusManager();
            var mainMenuViewCreator = new ViewCreator<MainMenuView>(mainMenuPrefab, menusParent);
            var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(inGameMenuPrefab, menusParent);
            var pauseViewCreator = new ViewCreator<PauseMenuView>(pauseMenuPrefab, menusParent);
            var optionsViewCreator = new ViewCreator<OptionsMenuView>(optionMenuPrefab, menusParent);
            var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(audioSettingsMenuPrefab, menusParent);
            var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(videoSettingsMenuPrefab, menusParent);
            var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(rebindKeysMenuPrefab, menusParent);
            var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(interfaceSettingsMenuPrefab, menusParent);
            var menus = new IMenuController[]
            {
                new MainMenuController(mainMenuViewCreator, null, menusManager, popupsManager, fadeManager, backgroundController),
                new InGameMenuController(inGameMenuViewCreator, new InGameMenuModel(), menusManager),
                new PauseMenuController(pauseViewCreator, null, menusManager, popupsManager, fadeManager, backgroundController),
                new OptionsMenuController(optionsViewCreator, null, menusManager),
                new AudioSettingsMenuController(audioSettingsViewCreator, new AudioSettingsMenuModel(settings), menusManager, popupsManager),
                new VideoSettingsMenuController(videoSettingsViewCreator, new VideoSettingsMenuModel(settings), menusManager, popupsManager),
                new RebindKeysMenuController(rebindKeysViewCreator, new RebindKeysMenuModel(settings), menusManager, popupsManager),
                new InterfaceSettingsMenuController(interfaceMenuViewCreator, new InterfaceSettingsMenuModel(settings), menusManager, popupsManager),
            };
            menusManager.Init(menus);
            menusManager.ShowMenu(typeof(MainMenuController), StackingType.Clear);
        }

    }
}
