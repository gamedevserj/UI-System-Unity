using System;
using System.Collections.Generic;
using AsyncAwaitBestPractices;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;
using UISystem.ScriptableObjects;
using UISystem.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    /// <summary>
    /// UI installer.
    /// </summary>
    public class UiInstaller : MonoBehaviour
    {
        [SerializeField] private Image _menuBackground;
        [SerializeField] private Image _fade;
        [SerializeField] private Transform _menusParent;
        [SerializeField] private Transform _popupsParent;
        [SerializeField] private MenuViewsDatabase _menuViewsDatabase;
        [SerializeField] private PopupViewsDatabase _popupViewsDatabase;

        private UIInputActions _inputActions;

        /// <summary>
        /// Gets the instance of the UiInstaller.
        /// </summary>
        public static UiInstaller Instance { get; private set; }

        /// <summary>
        /// Initialize UI.
        /// </summary>
        /// <param name="settings">Game settings.</param>
        public void Init(GameSettings settings)
        {
            var popupsManager = new PopupsManager();
            var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupPrefab(typeof(YesPopupView)), _popupsParent);
            var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupPrefab(typeof(YesNoPopupView)), _popupsParent);
            var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupPrefab(typeof(YesNoCancelPopupView)), _popupsParent);

            var popups = new IPopupController[]
            {
                new YesPopupController(yesPopupViewCreator, popupsManager),
                new YesNoPopupController(yesNoPopupViewCreator, popupsManager),
                new YesNoCancelPopupController(yesNoCancelPopupViewCreator, popupsManager),
            };

            popupsManager.Init(popups);

            var fadeManager = new ScreenFadeManager(_fade);
            var backgroundController = new MenuBackgroundController(_menuBackground);

            var menusManager = new MenusManager();
            var mainMenuViewCreator = new ViewCreator<MainMenuView>(GetMenuPrefab(typeof(MainMenuView)), _menusParent);
            var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(GetMenuPrefab(typeof(InGameMenuView)), _menusParent);
            var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuPrefab(typeof(PauseMenuView)), _menusParent);
            var optionsViewCreator = new ViewCreator<OptionsMenuView>(GetMenuPrefab(typeof(OptionsMenuView)), _menusParent);
            var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(GetMenuPrefab(typeof(AudioSettingsMenuView)), _menusParent);
            var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuPrefab(typeof(VideoSettingsMenuView)), _menusParent);
            var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuPrefab(typeof(RebindKeysMenuView)), _menusParent);
            var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(GetMenuPrefab(typeof(InterfaceSettingsMenuView)), _menusParent);
            var menus = new IMenuController[]
            {
                new MainMenuController(mainMenuViewCreator, menusManager, popupsManager, fadeManager, backgroundController),
                new InGameMenuController(inGameMenuViewCreator, menusManager, backgroundController),
                new PauseMenuController(pauseViewCreator, menusManager, popupsManager, fadeManager, backgroundController),
                new OptionsMenuController(optionsViewCreator, menusManager),
                new AudioSettingsMenuController(audioSettingsViewCreator, menusManager, new AudioSettingsMenuModel(settings), popupsManager),
                new VideoSettingsMenuController(videoSettingsViewCreator, menusManager, new VideoSettingsMenuModel(settings), popupsManager),
                new RebindKeysMenuController(rebindKeysViewCreator, menusManager, new RebindKeysMenuModel(settings), popupsManager),
                new InterfaceSettingsMenuController(interfaceMenuViewCreator, menusManager, new InterfaceSettingsMenuModel(settings), popupsManager),
            };

            _ = new InputProcessor(_inputActions, menusManager, popupsManager);

            menusManager.Init(menus);
            menusManager.ShowMenu<MainMenuView>(StackingType.Clear).SafeFireAndForget();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _inputActions = new UIInputActions();
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private ViewBase GetMenuPrefab(Type type) => _menuViewsDatabase.GetPrefab(type);

        private ViewBase GetPopupPrefab(Type type) => _popupViewsDatabase.GetPrefab(type);
    }
}
