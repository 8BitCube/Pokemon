!! This menu uses the new Unity UI system, it won`t work without it. !!

All you need to do is drag the "_QualitySettingsMenu" prefab into the first scene of your Game/Project.
!!Please make sure you have a "EventSystem" added to every scene or the canvas will not work.

If you want to pause time when the menu is open set the "Pause Time when menu open" bool on the SettingsMenu script in the inspector to "True".

To open the menu in game press Escape (but you can change this to any Key/Button you want).
Or you can use a UI button with the "OpenQualitySettingMenu()" function as an OnClickevent.

The menu lets you switch between the Quality Levels and lets you set the AA, Vcync and AF seperately,
Change resolution and toggle fullScreen/Windowed mode.
The Unity standard FPS counter asset is build in.
And a Vsync toggle.

Pressing the "QuitGame" button or closing the menu will save your setting.

The first time the game runs the way the menu is set will determen the default Quality setting, so set these the way you want them to be before building.

I recomend disabling/hiding the "Display Resolution Dialog" menu in the Player Settings, for these settings will be ignored.

there is a "Delete Playerprefs" button added but disabled, this may be usefull when testing settings.

When run in the Game View window the resolution dropbox will not show you much options,
but when build, this will be filled with all your monitors available resolutions.

for any questions feel free to contact me at:
PaulosCreations@Gmail.com
