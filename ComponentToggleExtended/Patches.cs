using System;
using HarmonyLib;
using MelonLoader;

namespace ComponentToggleExtended.Utilities {
    class Patches {
        private static void applyPatches(Type type) {
            if (Main.isDebug)
                MelonLogger.Msg($"Applying {type.Name} patches!");
            try {
                HarmonyLib.Harmony.CreateAndPatchAll(type, BuildInfo.Name);
            } catch (Exception e) {
                MelonLogger.Error($"Failed while patching {type.Name}!\n{e}");
            }
        }

        public static void PatchVRC_Station() => applyPatches(typeof(VRC_Station));
    }

    [HarmonyPatch(typeof(VRC_StationInternal))]
    class VRC_Station {
        [HarmonyPrefix]
        [HarmonyPatch("Method_Public_Boolean_Player_Boolean_0")]
        static bool PlayerCanUseStation(ref bool __result, VRC.Player __0, bool __1) {
            if (__0 != null && __0 == VRCPlayer.field_Internal_Static_VRCPlayer_0._player && !Main.VRC_Station.Value) {
                __result = false;
                return false;
            }
            return true;
        }
    }
}
