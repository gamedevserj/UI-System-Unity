using System;
using System.Collections.Generic;
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
    public class UiInstaller : MonoBehaviour
    {

        [SerializeField] private Image menuBackground;
        [SerializeField] private Image fade;
        [SerializeField] private Transform menusParent;
        [SerializeField] private Transform popupsParent;
        [SerializeField] private MenuViewsDatabase menuViewsDatabase;
        [SerializeField] private PopupViewsDatabase popupViewsDatabase;
        [SerializeField] private GameActions gameActions;

        private InputProcessor _inputProcessor;
        private UIInputActions _inputActions;

        public static UiInstaller Instance { get; private set; }

        private void Awake()
        {
            Instance = Instance != null ? Instance : this;
            _inputActions = new UIInputActions();
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
            var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupPrefab(typeof(YesPopupView)), popupsParent);
            var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupPrefab(typeof(YesNoPopupView)), popupsParent);
            var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupPrefab(typeof(YesNoCancelPopupView)), popupsParent);

            var popups = new Dictionary<Type, IPopupController<PopupResult>>
            {
                {
                    typeof(YesPopupView),
                    new YesPopupController(yesPopupViewCreator, popupsManager)
                },
                {
                    typeof(YesNoPopupView),
                    new YesNoPopupController(yesNoPopupViewCreator, popupsManager)
                },
                {
                    typeof(YesNoCancelPopupView),
                    new YesNoCancelPopupController(yesNoCancelPopupViewCreator, popupsManager)
                },
            };

            popupsManager.Init(popups);

            var fadeManager = new ScreenFadeManager(fade);
            var backgroundController = new MenuBackgroundController(menuBackground);

            var menusManager = new MenusManager();
            var mainMenuViewCreator = new ViewCreator<MainMenuView>(GetMenuPrefab(typeof(MainMenuView)), menusParent);
            var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(GetMenuPrefab(typeof(InGameMenuView)), menusParent);
            var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuPrefab(typeof(PauseMenuView)), menusParent);
            var optionsViewCreator = new ViewCreator<OptionsMenuView>(GetMenuPrefab(typeof(OptionsMenuView)), menusParent);
            var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(GetMenuPrefab(typeof(AudioSettingsMenuView)), menusParent);
            var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuPrefab(typeof(VideoSettingsMenuView)), menusParent);
            var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuPrefab(typeof(RebindKeysMenuView)), menusParent);
            var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(GetMenuPrefab(typeof(InterfaceSettingsMenuView)), menusParent);
            var menus = new Dictionary<Type, IMenuController>
            {
                {
                    typeof(MainMenuView),
                    new MainMenuController(
                        mainMenuViewCreator,
                        menusManager,
                        popupsManager,
                        fadeManager,
                        backgroundController)
                },
                {
                    typeof(InGameMenuView),
                    new InGameMenuController(inGameMenuViewCreator, menusManager)
                },
                {
                    typeof(PauseMenuView),
                    new PauseMenuController(
                        pauseViewCreator,
                        menusManager,
                        popupsManager,
                        fadeManager,
                        backgroundController)
                },
                {
                    typeof(OptionsMenuView),
                    new OptionsMenuController(optionsViewCreator, menusManager)
                },
                {
                    typeof(AudioSettingsMenuView),
                    new AudioSettingsMenuController(
                        audioSettingsViewCreator,
                        menusManager,
                        new AudioSettingsMenuModel(settings),
                        popupsManager)
                },
                {
                    typeof(VideoSettingsMenuView),
                    new VideoSettingsMenuController(
                        videoSettingsViewCreator,
                        menusManager,
                        new VideoSettingsMenuModel(settings),
                        popupsManager)
                },
                {
                    typeof(RebindKeysMenuView),
                    new RebindKeysMenuController(
                        rebindKeysViewCreator,
                        menusManager,
                        new RebindKeysMenuModel(settings),
                        popupsManager)
                },
                {
                    typeof(InterfaceSettingsMenuView),
                    new InterfaceSettingsMenuController(
                        interfaceMenuViewCreator,
                        menusManager,
                        new InterfaceSettingsMenuModel(settings),
                        popupsManager)
                },
            };
            menusManager.Init(menus);
            menusManager.ShowMenu(typeof(MainMenuView), StackingType.Clear);

            _inputProcessor = new InputProcessor(_inputActions, menusManager, popupsManager);
        }

        private ViewBase GetMenuPrefab(Type type) => menuViewsDatabase.GetPrefab(type);

        private ViewBase GetPopupPrefab(Type type) => popupViewsDatabase.GetPrefab(type);
    }
}
