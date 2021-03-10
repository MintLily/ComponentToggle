using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Harmony;

namespace ComponentToggle.Utilities
{
    class Patches
    {
        private static HarmonyMethod GetLocalPatch(string name) { return new HarmonyMethod(typeof(Patches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)); }

        private static HarmonyInstance ComponentTogglePatch = HarmonyInstance.Create("ComponentToggle");

        public static void PatchVRC_Station()
        {
            ComponentTogglePatch.Patch(AccessTools.Property(typeof(VRC_StationInternal), "Method_Public_Boolean_Player_Boolean_0").GetMethod, null, GetLocalPatch("CanUseStation"), null);
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
