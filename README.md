# ComponentToggle
Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, and Pens, Chairs, Mirrors, Post Processing)

### MelonLoader
Need to install MelonLoader?<br>
Click [this link](https://melonwiki.xyz/) to get started!

### Prerequisites
MelonLoader: v0.3.0+ (Alpha)<br>
Game: VRChat (build 1061+)<br>

### /UserData/ComponentToggleConfig.json (Default Values)
```
{
  "VRC_Pickup": false,
  "VRC_Pickup_Objects": true,
  "VRC_SyncVideoPlayer": true,
  "Pens": true,
  "VRC_Station": true,
  "VRC_MirrorReflect": true,
  "PostProcessing": true
}
```
<br>
VRC_Pickup - Allow Pickups<br>
VRC_Pickup_Objects - Show Pickups<br>
VRC_SyncVideoPlayer - Show Video Players<br>
Pens - Show Pens & Erasers<br>
VRC_Station - Allow yourself to sit in chairs<br>
VRC_MirrorReflect - Show Mirrors<br>
PostProcessing - Show PostProcessing<br>

### Preview
![Preview 1](https://kortyboi.com/img/upload/VRChat_ZmRFcJMvyb.jpg)<br>
![Preview 2](https://kortyboi.com/img/upload/VRChat_sojfrXy4Gy.png)<br>

# Change Log
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
