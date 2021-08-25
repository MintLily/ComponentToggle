using MelonLoader;
using System;
using System.Reflection;
using System.Collections;
using System.Linq;
using ComponentToggle.Components;
using ComponentToggle.Utilities.Config;
using System.IO;
using UIExpansionKit.API;
using UnityEngine.UI;
using UnityEngine;

namespace ComponentToggle
{
    public static class BuildInfo
    {
        public const string Name = "ComponentToggle";
        public const string Author = "Lily";
        public const string Company = null;
        public const string Version = "1.8.2";
        public const string DownloadLink = "https://github.com/MintLily/ComponentToggle";
        public const string Description = "Toggle certain components with VRChat. (Toggle Pickup, Pickup Objects, Video Players, Pens, Chairs, Mirrors, Post Processing, and Avatar Pedestals)";
    }

    public class Main : MelonMod
    {
        private MelonMod Instance;
        public static bool isDebug;
        public static MelonPreferences_Category melon;
        public static MelonPreferences_Entry<bool> VRC_Pickup, VRC_Pickup_Objects, VRC_SyncVideoPlayer, Pens, VRC_Station, VRC_MirrorReflect, PostProcessing, VRC_AvatarPedestal, UIXMenu;

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            Instance = this;
            if (MelonDebug.IsEnabled() || Environment.CommandLine.Contains("--ct.debug")) {
                isDebug = true;
                MelonLogger.Msg("Debug mode is active");
            }

            MelonCoroutines.Start(GetAssembly());

            melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            VRC_Pickup = melon.CreateEntry("EnablePickup", true, "Enable Pickup");
            VRC_Pickup_Objects = melon.CreateEntry("ShowPickupObjects", true, "Show Pickup Objects");
            VRC_SyncVideoPlayer = melon.CreateEntry("ShowVideoPlayers", true, "Show Video Players");
            Pens = melon.CreateEntry("ShowPens", true, "Show Pens / Erasers");
            VRC_Station = melon.CreateEntry("EnableChairs", true, "Enable Chairs");
            VRC_MirrorReflect = melon.CreateEntry("ShowMirrors", true, "Show Mirrors");
            PostProcessing = melon.CreateEntry("EnablePostProcessing", true, "Enable Post Processing");
            VRC_AvatarPedestal = melon.CreateEntry("ShowAvatarsPedestals", true, "Show Avatars Pedestals");

            UIXMenu = melon.CreateEntry("ShowUIXMenuButton", true, "Use a Menu built by UIExpansionKit");

            try { CustomConfig.Load(); }
            catch {
                if (!File.Exists(CustomConfig.final) && isDebug)
                    MelonLogger.Msg("Not an error > Old Config file does not exist, ignoring function.");
            }

            UIXMenuReplacement.Init();
            Utilities.Patches.PatchVRC_Station(); // VRC_Station with HarmonyX

            MelonLogger.Msg("Initialized!");
        }

        private void OnUiManagerInit() 
        {
            CustomConfig.ConvertAndRemove();
            Menu.Init();
            //Utilities.GetBlockedWorlds.Init();
            MelonCoroutines.Start(Utilities.Menu.AllowToolTipTextColor());
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            switch (buildIndex) {
                case 0:
                case 1:
                    break;
                default:
                    Menu.WorldWasChanged = true;
                    Components.PostProcessing.WorldWasChanged = true;
                    Components.VRCPickup.OnLevelLoad();
                    Components._VRCSyncVideoPlayer.OnLevelLoad();
                    Components.Pens.OnLevelLoad();
                    //Utilities.Patches.PatchVRC_Station();
                    Components.VRCMirrorReflect.OnLevelLoad();
                    Components.PostProcessing.OnLevelLoad();
                    VRCAvatarPedestal.OnLevelLoad();
                    Utilities.WorldLogic.ReCacheAllObjects();

                    MelonCoroutines.Start(Menu.OnLevelLoad());
                    Menu.setAllButtonToggleStates(false);
                    break;
            }
            MelonCoroutines.Start(Utilities.WorldLogic.CheckWorld());
        }

        public override void OnPreferencesSaved()
        {
            if (isDebug) {
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
                    " ============== bool UIXMenu               = " + UIXMenu.Value.ToString() + "\n" +
                    " ====================================================");
            }

            Menu.setAllButtonToggleStates(true); // When saved, button toggle states are set, (the bool) the actions of the buttons are invoked

            try { UIXMenuReplacement.MainMenuBTN.SetActive(UIXMenu.Value); } catch { }
        }

        private IEnumerator GetAssembly()
        {
            Assembly assemblyCSharp = null;
            while (true) {
                assemblyCSharp = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "Assembly-CSharp");
                if (assemblyCSharp == null)
                    yield return null;
                else
                    break;
            }

            MelonCoroutines.Start(WaitForUiManagerInit(assemblyCSharp));
        }

        private IEnumerator WaitForUiManagerInit(Assembly assemblyCSharp)
        {
            Type vrcUiManager = assemblyCSharp.GetType("VRCUiManager");
            PropertyInfo uiManagerSingleton = vrcUiManager.GetProperties().First(pi => pi.PropertyType == vrcUiManager);
            while (uiManagerSingleton.GetValue(null) == null)
                yield return null;
            OnUiManagerInit();
        }

        public override void OnApplicationQuit() => MelonPreferences.Save();
    }
}