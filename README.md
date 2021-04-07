# ComponentToggle
Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, Pens, Chairs, Mirrors, Post Processing, and Avatar Pedestals)

### MelonLoader
Need to install MelonLoader?<br>
Click [this link](https://melonwiki.xyz/) to get started!

### Prerequisites
MelonLoader: v0.3.0+ (Alpha)<br>
Game: VRChat (build 1069+)<br>

### MelonPreferences.cfg (Default Values)
```ini
[ComponentToggle]
EnablePickup = true
ShowPickupObjects = true
ShowVideoPlayers = true
ShowPens = true
EnableChairs = true
ShowMirrors = true
EnablePostProcessing = true
ShowAvatarsPedestals = true
```
<br>
EnablePickup - Allow Pickups<br>
ShowPickupObjects - Show Pickups<br>
ShowVideoPlayers - Show Video Players<br>
ShowPens - Show Pens & Erasers<br>
EnableChairs - Allow yourself to sit in chairs<br>
ShowMirrors - Show Mirrors<br>
EnablePostProcessing - Show PostProcessing<br>
ShowAvatarsPedestals - Show Avatar Pedestals<br>

### Preview
![Preview 1 - Menu Location](https://kortyboi.com/img/upload/VRChat_ZmRFcJMvyb.jpg)<br>
![Preview 2 - Menu Content](https://kortyboi.com/img/upload/VRChat_sojfrXy4Gy.png)<br>

# Change Log
### v1.5.0
* Added UIExpansionKit Menu option
* Added SDK3 Video Player toggle
* Fixed an issue that would cause all toggles to be set to false

### v1.4.0
* Removed Custom Config because corruptions are horrible
* * Made config system go through MelonPrefs for ease
* Removed WebAdded GameObject List

### v1.3.0
* Added Avatar Pedestal Toggle
* Fixed errors caused by adding GameObjects gathered from the Web

### v1.2.2
* Changed how to get WebAdded GameObject List (no longer 9 files, now one file)
* Fixed where VideoPlayer Toggle button would not re-enable on world change (into an SDK2 world)

### v1.2.1
* Added WebHosted GameObject List - will this allow me to add a game object of a pen that isn't baked in the code of the mod

### v1.2.0
* Added a Blocked World List - joining certain worlds will disable some actions<br>
-- i.e. If you join Murder 4, VRC_Pickup toggle buttons cannot be toggled, and pickups are forced to be shown and interactable
* Fixed Chairs toggle's action being opposite

### v1.1.0
* Added Custom Configuration - this is to reduce the amount of console spam for saving the preferences

### v1.0.0
* Initial Release

# Credits
DubyaDude, Emilia, Tritn - [RubyButtonAPI](https://github.com/DubyaDude/RubyButtonAPI)



## Stay Updated
Stay update to date with all my mods by joining my [discord server](https://discord.gg/qkycuAMUGS) today.
