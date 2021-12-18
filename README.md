# ComponentToggle
Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, Pens, Chairs, Mirrors, Post Processing, and Avatar Pedestals)

### MelonLoader
Need to install MelonLoader?<br>
Click [this link](https://melonwiki.xyz/) to get started!

### Prerequisites
MelonLoader: v0.5.2 (Alpha)<br>
Game: VRChat (build 1160)<br>
Mods: [UI Expansion Kit](https://github.com/knah/VRCMods)

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
ShowUIXMenuButton = true
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
ShowUIXMenuButton - Toggle a UIExpansionKit Menu Item for opening the menu

### Preview
![Preview - Menu Content](https://mintlily.lgbt/img/upload/waDaVE8kpjar.jpg)<br>

### For World Creators - Block Actions
Want users to not using certain features? Add empty gameobjects with certain names to block actions.<br>
```
CTBlockAction_1 = Block Pickup + Pickup Object Toggle
CTBlockAction_2 = Block Video Players Toggle
CTBlockAction_3 = Block Pens Toggle
CTBlockAction_4 = Block Stations (Chairs) Toggle
CTBlockAction_5 = Block Mirror Toggle
CTBlockAction_6 = Block Post Processing Toggle
CTBlockAction_7 = Block Avatar Pedestals Toggle
```
![GameObject List](https://mintlily.lgbt/img/upload/p5Mp5uigsMrx.png)
<br>

# Change Log
### v2.0.0
* Rewrote the entire mod
* Fixed issues with toggles not being consistent

### v1.10.0
* Added VRChat build 1160 compatibility
* Fixed other Errors

### v1.9.0
* Added VRChat build 1151 compatibility
* Removed RubyButtonAPI

### v1.8.2
* Rolled back portal toggling
* Fixed UIX menu inconsistency

### v1.8.1
* Bug Fixes

### v1.8.0
* Added a UIExpansionKit Menu Interface
* Added World Portal Toggle

### v1.7.1
* Recompiled for VRChat build 1121

### v1.7.0
* Added World creators to add a block for certain features within this mod [How to](https://github.com/MintLily/ComponentToggle#for-world-creators---block-actions)

### v1.6.0
* Upgraded to MelonLoader v0.4.0
* Added Null Checks for less errors
* Change Patches for new ML

### v1.5.4
* Added support for MelonLoader v0.4.0

### v1.5.3
* Internal Link Updates

### v1.5.2
* Fixed VRC_StationInternal for Chair toggles

### v1.5.1
* Fixed an issue where everyone would be able to pickup object in certain worlds

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
null



## Stay Updated
Stay update to date with all my mods by joining my [discord server](https://discord.gg/qkycuAMUGS) today.
