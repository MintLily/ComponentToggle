using MelonLoader;
using UnityEngine;
using System;
using UnityEngine.XR;
using ComponentToggle.Components;

namespace ComponentToggle
{
    public static class BuildInfo
    {
        public const string Name = "ComponentToggle"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Korty (Lily)"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = "https://github.com/KortyBoi/ComponentToggle"; // Download Link for the Mod.  (Set as null if none)
        public const string Description = "Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, and Pens, Chairs, Mirrors, Post Processing)";
    }

    public class Main : MelonMod
    {
        public static bool isDebug;
        public static MelonPreferences_Category melon;
        public static MelonPreferences_Entry<bool> VRC_Pickup;
        public static MelonPreferences_Entry<bool> VRC_Pickup_Objects;
        public static MelonPreferences_Entry<bool> VRC_SyncVideoPlayer;
        public static MelonPreferences_Entry<bool> Pens;
        public static MelonPreferences_Entry<bool> VRC_Station;
        public static MelonPreferences_Entry<bool> VRC_MirrorReflect;
        public static MelonPreferences_Entry<bool> PostProcessing;

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            if (Environment.CommandLine.Contains("--ct.debug"))
            {
                isDebug = true;
                MelonLogger.Msg("Debug mode is active");
            }

            melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            VRC_Pickup = (MelonPreferences_Entry<bool>)melon.CreateEntry("vrcpickup", true, "Allow Pickups");
            VRC_Pickup_Objects = (MelonPreferences_Entry<bool>)melon.CreateEntry("show_vrcpickup", true, "Show Pickups");
            VRC_SyncVideoPlayer = (MelonPreferences_Entry<bool>)melon.CreateEntry("show_syncvideoplayer", true, "Show Video Players");
            Pens = (MelonPreferences_Entry<bool>)melon.CreateEntry("show_pensAndErasers", true, "Show Pens & Erasers");
            VRC_Station = (MelonPreferences_Entry<bool>)melon.CreateEntry("show_vrcstation", true, "Allow yourself to sit in chairs");
            VRC_MirrorReflect = (MelonPreferences_Entry<bool>)melon.CreateEntry("show_vrcmirrorreflect", true, "Show Mirrors");
            PostProcessing = (MelonPreferences_Entry<bool>)melon.CreateEntry("show_postprocessing", true, "Show PostProcessing");

            MelonLogger.Msg("Initialized!");
        }

        public override void VRChat_OnUiManagerInit() { Menu.Init(); }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (buildIndex == -1)
            {
                Menu.WorldWasChanged = true;
                Components.PostProcessing.WorldWasChanged = true;
                Components.VRCPickup.OnLevelLoad();
                Components._VRCSyncVideoPlayer.OnLevelLoad();
                Components.Pens.OnLevelLoad();
                Utilities.Patches.PatchVRC_Station();
                Components.VRCMirrorReflect.OnLevelLoad();
                Components.PostProcessing.OnLevelLoad();
                MelonCoroutines.Start(Menu.OnLevelLoad());
            }
        }

        public override void OnPreferencesSaved() 
        {
            if (isDebug)
            {
                MelonLogger.Msg("[Debug] \n" +
                    " ================= Preferences Values: ============== \n" +
                    " ============== bool VRC_Pickup            = " + VRC_Pickup.Value.ToString() + "\n" +
                    " ============== bool VRC_Pickup_Objects    = " + VRC_Pickup_Objects.Value.ToString() + "\n" +
                    " ============== bool VRC_SyncVideoPlayer   = " + VRC_SyncVideoPlayer.Value.ToString() + "\n" +
                    " ============== bool Pens                  = " + Pens.Value.ToString() + "\n" +
                    " ============== bool VRC_Station           = " + VRC_Station.Value.ToString() + "\n" +
                    " ============== bool VRC_MirrorReflect     = " + VRC_MirrorReflect.Value.ToString() + "\n" +
                    " ============== bool PostProcessing        = " + PostProcessing.Value.ToString() + "\n" +
                    " ====================================================");
            }
            // Sets Toggle States on Pref Save
            Menu.TogglePickup.setToggleState(Main.VRC_Pickup.Value);
            Menu.TogglePickupObject.setToggleState(Main.VRC_Pickup_Objects.Value);
            Menu.ToggleVideoPlayers.setToggleState(Main.VRC_SyncVideoPlayer.Value);
            Menu.TogglePens.setToggleState(Main.Pens.Value);
            Menu.ToggleStation.setToggleState(Main.VRC_Station.Value);
            Menu.ToggleMirror.setToggleState(Main.VRC_MirrorReflect.Value);
            Menu.TogglePostProcessing.setToggleState(Main.PostProcessing.Value);
        }
    }
}