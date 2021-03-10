using MelonLoader;
using UnityEngine;
using System;
using UnityEngine.XR;
using ComponentToggle.Components;
using ComponentToggle.Utilities.Config;

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

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            if (Environment.CommandLine.Contains("--ct.debug"))
            {
                isDebug = true;
                MelonLogger.Msg("Debug mode is active");
            }

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
                    " ============== bool VRC_Pickup            = " + CustomConfig.Get().VRC_Pickup.ToString() + "\n" +
                    " ============== bool VRC_Pickup_Objects    = " + CustomConfig.Get().VRC_Pickup_Objects.ToString() + "\n" +
                    " ============== bool VRC_SyncVideoPlayer   = " + CustomConfig.Get().VRC_SyncVideoPlayer.ToString() + "\n" +
                    " ============== bool Pens                  = " + CustomConfig.Get().Pens.ToString() + "\n" +
                    " ============== bool VRC_Station           = " + CustomConfig.Get().VRC_Station.ToString() + "\n" +
                    " ============== bool VRC_MirrorReflect     = " + CustomConfig.Get().VRC_MirrorReflect.ToString() + "\n" +
                    " ============== bool PostProcessing        = " + CustomConfig.Get().PostProcessing.ToString() + "\n" +
                    " ====================================================");
            }
            // Sets Toggle States on Pref Save
            Menu.TogglePickup.setToggleState(CustomConfig.Get().VRC_Pickup);
            Menu.TogglePickupObject.setToggleState(CustomConfig.Get().VRC_Pickup_Objects);
            Menu.ToggleVideoPlayers.setToggleState(CustomConfig.Get().VRC_SyncVideoPlayer);
            Menu.TogglePens.setToggleState(CustomConfig.Get().Pens);
            Menu.ToggleStation.setToggleState(CustomConfig.Get().VRC_Station);
            Menu.ToggleMirror.setToggleState(CustomConfig.Get().VRC_MirrorReflect);
            Menu.TogglePostProcessing.setToggleState(CustomConfig.Get().PostProcessing);
        }
    }
}