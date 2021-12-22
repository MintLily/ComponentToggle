using System;
using ComponentToggleExtended.Utilities;
using MelonLoader;

namespace ComponentToggleExtended {
    public class BuildInfo {
        public const string Name = "ComponentToggle";
        public const string Author = "Lily";
        public const string Company = null;
        public const string Version = "2.0.1";
        public const string DownloadLink = "https://github.com/MintLily/ComponentToggle";
        public const string Description = "Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, Pens, Chairs, Mirrors, Post Processing, and Avatar Pedestals)";
    }

    public class Main : MelonMod {
        private MelonMod Instance;
        public static bool isDebug;
        public static MelonPreferences_Category melon;
        public static MelonPreferences_Entry<bool> VRC_Pickup, VRC_Pickup_Objects, VRC_SyncVideoPlayer, Pens, VRC_Station, VRC_MirrorReflect, PostProcessing, VRC_AvatarPedestal;
        public static readonly MelonLogger.Instance Log = new MelonLogger.Instance(BuildInfo.Name, ConsoleColor.Magenta);

        public override void OnApplicationStart() {
            Instance = this;
            if (MelonDebug.IsEnabled() || Environment.CommandLine.Contains("--ct.debug")) {
                isDebug = true;
                Logs("Debug mode is active", isDebug);
            }

            melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            VRC_Pickup = melon.CreateEntry("EnablePickup", true, "Enable Pickup");
            VRC_Pickup_Objects = melon.CreateEntry("ShowPickupObjects", true, "Show Pickup Objects");
            VRC_SyncVideoPlayer = melon.CreateEntry("ShowVideoPlayers", true, "Show Video Players");
            Pens = melon.CreateEntry("ShowPens", true, "Show Pens / Erasers");
            VRC_Station = melon.CreateEntry("EnableChairs", true, "Enable Chairs");
            VRC_MirrorReflect = melon.CreateEntry("ShowMirrors", true, "Show Mirrors");
            PostProcessing = melon.CreateEntry("EnablePostProcessing", true, "Enable Post Processing");
            VRC_AvatarPedestal = melon.CreateEntry("ShowAvatarsPedestals", true, "Show Avatars Pedestals");

            Menu.Init();
            Patches.PatchVRC_Station(); // VRC_Station with HarmonyX
        }

        public static void Logs(string message, bool debug = false) {
            if (debug)
                Log.Msg(ConsoleColor.Green, message);
            else Log.Msg(message);
        }

        public static bool WorldWasChanged = false;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            if (buildIndex == -1 && buildIndex != 0 && buildIndex != 1) {
                WorldWasChanged = true;
                WorldObjects.DoActions();
            }
            MelonCoroutines.Start(WorldLogic.CheckWorld());
        }
    }
}
