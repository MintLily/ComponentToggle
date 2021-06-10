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
        public const string Version = "1.5.4";
        public const string DownloadLink = "https://github.com/MintLily/ComponentToggle";
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

        public static MelonPreferences_Entry<bool> UIXMenu;
        private static GameObject UIXMenuGO;

        public override void OnApplicationStart() // Runs after Game Initialization.
        {
            if (MelonDebug.IsEnabled() || Environment.CommandLine.Contains("--ct.debug"))
            {
                isDebug = true;
                MelonLogger.Msg("Debug mode is active");
            }

            if (typeof(MelonMod).GetMethod("VRChat_OnUiManagerInit") == null)
                MelonCoroutines.Start(GetAssembly());

            melon = MelonPreferences.CreateCategory(BuildInfo.Name, BuildInfo.Name);
            VRC_Pickup = (MelonPreferences_Entry<bool>)melon.CreateEntry("EnablePickup", true, "Enable Pickup");
            VRC_Pickup_Objects = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowPickupObjects", true, "Show Pickup Objects");
            VRC_SyncVideoPlayer = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowVideoPlayers", true, "Show Video Players");
            Pens = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowPens", true, "Show Pens / Erasers");
            VRC_Station = (MelonPreferences_Entry<bool>)melon.CreateEntry("EnableChairs", true, "Enable Chairs");
            VRC_MirrorReflect = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowMirrors", true, "Show Mirrors");
            PostProcessing = (MelonPreferences_Entry<bool>)melon.CreateEntry("EnablePostProcessing", true, "Enable Post Processing");
            VRC_AvatarPedestal = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowAvatarsPedestals", true, "Show Avatars Pedestals");

            UIXMenu = (MelonPreferences_Entry<bool>)melon.CreateEntry("ShowUIXMenuButton", true, "Put Menu on UIExpansionKit's Quick Menu");

            try { CustomConfig.Load(); }
            catch {
                if (!File.Exists(CustomConfig.final) && isDebug)
                    MelonLogger.Msg("Not an error > Old Config file does not exist, ignoring function.");
            }

            try {
                ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Component\nToggle", () =>
                {
                    Menu.menu.getMainButton().getGameObject().GetComponent<Button>().onClick.Invoke();
                }, new Action<GameObject>((GameObject obj) =>
                {
                    UIXMenuGO = obj;
                    obj.SetActive(UIXMenu.Value);
                }));
            }
            catch (Exception e) { MelonLogger.Error("UIXMenu:\n" + e.ToString()); }

            MelonLogger.Msg("Initialized!");
        }

        private void OnUiManagerInit() 
        {
            CustomConfig.ConvertAndRemove();
            Menu.Init();
            Utilities.GetBlockedWorlds.Init();
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
                    Utilities.Patches.PatchVRC_Station();
                    Components.VRCMirrorReflect.OnLevelLoad();
                    Components.PostProcessing.OnLevelLoad();
                    VRCAvatarPedestal.OnLevelLoad();

                    MelonCoroutines.Start(Menu.OnLevelLoad());
                    Menu.setAllButtonToggleStates(false);
                    break;
            }
            MelonCoroutines.Start(Utilities.GetBlockedWorlds.DelayedLoad());
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

            try { UIXMenuGO.SetActive(UIXMenu.Value); } catch { }
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
    }
}