using System.Reflection;
using Harmony;

namespace ComponentToggle.Utilities
{
    class Patches
    {
        private static HarmonyInstance ComponentTogglePatch = HarmonyInstance.Create("ComponentToggle");

        public static void PatchVRC_Station()
        {
            ComponentTogglePatch.Patch(typeof(VRC_StationInternal).GetMethod("Method_Public_Boolean_Player_Boolean_0"),
                new HarmonyMethod(typeof(Patches).GetMethod("CanUseStation", BindingFlags.Static | BindingFlags.NonPublic)), null, null);
        }

        private static bool CanUseStation(ref bool __result, VRC.Player __0, bool __1)
        {
            if (__0 != null && __0 == VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_Player_0 && !Main.VRC_Station.Value)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
