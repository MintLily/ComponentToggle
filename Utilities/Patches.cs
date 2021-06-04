using System;
using System.Reflection;
using Harmony;

namespace ComponentToggle.Utilities
{
    class Patches
    {
        private static readonly HarmonyInstance ComponentTogglePatch = HarmonyInstance.Create("ComponentToggle");

        public static void PatchVRC_Station()
        {
            try
            {
                ComponentTogglePatch.Patch(typeof(VRC_StationInternal).GetMethod("Method_Public_Boolean_Player_Boolean_0"),
              new HarmonyMethod(typeof(Patches).GetMethod("CanUseStation", BindingFlags.Static | BindingFlags.NonPublic)));
            }
            catch (Exception e) { MelonLoader.MelonLogger.Error("VRC_StationInternal Patch Error:\n" + e.ToString()); }
        }

        private static bool CanUseStation(ref bool __result, VRC.Player __0, bool __1)
        {
            if (__0 != null && __0 == VRCPlayer.field_Internal_Static_VRCPlayer_0._player && !Main.VRC_Station.Value)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
