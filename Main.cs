using MelonLoader;
using UnityEngine;
using System;
using UnityEngine.XR;
using ComponentToggle.Components;
using ComponentToggle.Utilities.Config;
using System.IO;

namespace ComponentToggle
{
    public static class BuildInfo
    {
        public const string Name = "ComponentToggle"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Korty (Lily)"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.4.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = "https://github.com/KortyBoi/ComponentToggle"; // Download Link for the Mod.  (Set as null if none)
        public const string Description = "Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, Pens, Chairs, Mirrors, Post Processing, and Avatar Pedestals)";
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
        public static MelonPreferences_Entry<bool> VRC_AvatarPedestal;

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            if (MelonDebug.IsEnabled() || Environment.CommandLine.Contains("--ct.debug"))
            {
                isDebug = true;
                MelonLogger.Msg("Debug mode is active");
            }

            melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            VRC_Pickup = (MelonPreferences_Entry<bool>)melon.CreateEntry("EnablePickup", true, "Enable Pickup");
            VRC_Pickup_Objects = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowPickupObjects", true, "Show Pickup Objects");
            VRC_SyncVideoPlayer = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowVideoPlayers", true, "Show Video Players");
            Pens = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowPens", true, "Show Pens / Erasers");
            VRC_Station = (MelonPreferences_Entry<bool>)melon.CreateEntry("EnableChairs", true, "Enable Chairs");
            VRC_MirrorReflect = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowMirrors", true, "Show Mirrors");
            PostProcessing = (MelonPreferences_Entry<bool>)melon.CreateEntry("EnablePostProcessing", true, "Enable Post Processing");
            VRC_AvatarPedestal = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowAvatarsPedestals", true, "Show Avatars Pedestals");

            try { CustomConfig.Load(); }
            catch
            {
                if (!File.Exists(CustomConfig.final) && isDebug)
                    MelonLogger.Msg("Not an error > Old Config file does not exist, ignoring function.");
            }
            
            MelonLogger.Msg("Initialized!");
        }

        public override void VRChat_OnUiManagerInit() 
        {
            CustomConfig.ConvertAndRemove();
            Menu.Init();
            Utilities.GetBlockedWorlds.Init();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            switch (buildIndex)
            {
                case 0:
                case 1:
                    break;
                default:
                    Menu.WorldWasChanged = true;
                    Components.PostProcessing.WorldWasChanged = true;
                    Components.VRCPickup.OnLevelLoad();
                    Components._VRCSyncVideoPlayer.OnLevelLoad();
                    Components.Pens.OnLevelLoad();
                    Utilities.Patches.PatchVRC_Station();
                    Components.VRCMirrorReflect.OnLevelLoad();
                    Components.PostProcessing.OnLevelLoad();
                    MelonCoroutines.Start(Menu.OnLevelLoad());
                    VRCAvatarPedestal.OnLevelLoad();

                    Menu.setAllButtonToggleStates(false);
                    break;
            }
            MelonCoroutines.Start(Utilities.GetBlockedWorlds.DelayedLoad());
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
                    " ============== bool VRC_AvatarPedestal    = " + VRC_AvatarPedestal.Value.ToString() + "\n" +
                    " ====================================================");
            }

            Menu.setAllButtonToggleStates(false);
        }
    }
}