# UI-System-Unity

UI system for Unity engine that uses Dependency Injection and MVC patterns.  
Core of this system is a submodule that can be found here : https://github.com/gamedevserj/UI-System-Core 

To add new menu:
1. Add your menu type to the MenuType enum
2. Create your menu view script that either inherits from MenuView or from SettingsMenuView (it has functionality to reset setting to default via model and reset view to default)
3. If your view has interactable elements (buttons, sliders, etc.), they should have scripts attached to them that implement IFocusableControl to disable elements during menu transitions. Some of the elements already included in the repo at UISystem/Common/Elements, there are also prefabs for them in the UISystem/Common/Prefabs, so you can use those
4. Create your menu model script that implements IMenuModel interface (it is just a marker interface) or ISettingsMenuModel (which has methods to save, discard and reset to default)
5. Create your menu controller script that inherits from MenuControllerBase providing your view and model
6. In your menu controller implement the MenuType property by providing your menu type and implement SetupElements()
7. Create field in your UiInstaller for the menu view prefab
8. Add your menus to the menus array created in the UiInstaller that passes them to the MenusManager
9. After that you should be able to call _menusManager.ShowMenu(...) to show your new menu

### Menu background controller
A simple script that handles menu's background, look at MainMenu and PauseMenu controllers for example.

ℹ️ Audio, video, and interface menus are setup to have a popup if some settings were not saved before quitting. Key rebinding menu saves binds when new key is assigned that's why it has empty Save and DiscardChanges methods. If you want to save changes only when pressing save button, you need to make some changes - look at audio/video settings for example how it can be done.  
ℹ️ For saving settings the repo uses Advanced INI parser asset from asset store. You can replace it with any other method of your choosing.

#### Rebinding menu example

Rebinding menu uses the new Input system and parts of the script for rebinding are taken from the official Rebind UI sample. There are some limitations with the Input system version used in this project, mainly - you can't cancel out rebinding of a device from another device. Meaning that if you start rebinding a button on a gamepad, then you'll have to cancel out by pressing a button on a gamepad, pressing Escape won't do anything. See solution in this
[unity discuttion](https://discussions.unity.com/t/impossible-to-get-cancelling-of-control-rebind-working-with-both-keyboard-and-gamepad/1576251/3).

 ⚠️ The example rebinding menu uses image names from https://kenney.nl/assets/input-prompts, but they are not included in this repository. You'll need to download them separately.

## Popup system
Some of the popups are already setup and some menus already use them. The steps to create a new type of popup are the same as creating a new menu. 

## Screen fade
A simple script that controls fading, call FadeOut() with an optional action as a parameter that you want to happen when screen is completely black.

## Transitions  
Transition control the way view is shown/hidden. The repo includes few transitions as example. Note that resizing canvas elements require redrawing the whole canvas which means that some complex transitions may not be very suitable for mobile platforms. 
Transitions use DOTween package, the default settings are changed. Major changes are - tweens use unscale time and they don't autostart.
