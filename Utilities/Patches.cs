using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using ComponentToggle.Utilities.Config;

namespace ComponentToggle.Utilities
{
    class Patches
    {
        private static HarmonyInstance ComponentTogglePatch = HarmonyInstance.Create("ComponentToggle");

        public static void PatchVRC_Station()
        {
            ComponentTogglePatch.Patch(typeof(VRC_StationInternal).GetMethod("Method_Public_Boolean_Player_Boolean_0"), new HarmonyMethod(typeof(Patches).GetMethod("CanUseStation", BindingFlags.Static | BindingFlags.NonPublic)), null, null);
        }

        private static bool CanUseStation(ref bool __result, VRC.Player __0, bool __1)
        {
            if (__0 != null && __0 == VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_Player_0 && CustomConfig.Get().VRC_Station)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
