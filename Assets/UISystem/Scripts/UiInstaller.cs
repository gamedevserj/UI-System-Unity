using System;
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
using UISystem.ScriptableObjects;
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
        [SerializeField] private MenuViewsDatabase menuViewsDatabase;
        [SerializeField] private PopupViewsDatabase popupViewsDatabase;
        [SerializeField] private GameActions gameActions;

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
            var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupView(typeof(YesPopupView)), popupsParent);
            var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupView(typeof(YesNoPopupView)), popupsParent);
            var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupView(typeof(YesNoCancelPopupView)), popupsParent);
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
            var mainMenuViewCreator = new ViewCreator<MainMenuView>(GetMenuView(typeof(MainMenuView)), menusParent);
            var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(GetMenuView(typeof(InGameMenuView)), menusParent);
            var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuView(typeof(PauseMenuView)), menusParent);
            var optionsViewCreator = new ViewCreator<OptionsMenuView>(GetMenuView(typeof(OptionsMenuView)), menusParent);
            var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(GetMenuView(typeof(AudioSettingsMenuView)), menusParent);
            var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuView(typeof(VideoSettingsMenuView)), menusParent);
            var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuView(typeof(RebindKeysMenuView)), menusParent);
            var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(GetMenuView(typeof(InterfaceSettingsMenuView)), menusParent);
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

        private ViewBase GetMenuView(Type type) => menuViewsDatabase.GetView(type);
        private ViewBase GetPopupView(Type type) => popupViewsDatabase.GetView(type);
    }
}
