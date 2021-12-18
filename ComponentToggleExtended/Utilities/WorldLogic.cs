using UnityEngine;
using System.Collections;
using System.Linq;

namespace ComponentToggleExtended.Utilities {
    static class WorldLogic {
        internal static readonly string Base = "CTBlockAction_";
        internal static readonly string[] names = { $"{Base}1", $"{Base}2", $"{Base}3", $"{Base}4", $"{Base}5", $"{Base}6", $"{Base}7" };
        public static GameObject[] allWorldGameObjects;
        public static void ReCacheAllObjects() => allWorldGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        internal static bool blockPickup, blockObject, blockVid, blockPens, blockChair, blockMirror, blockPP, blockAP;

        internal static IEnumerator CheckWorld() {
            if (RoomManager.field_Internal_Static_ApiWorld_0 == null)
                yield break;
            yield return new WaitForSecondsRealtime(2);
            allWorldGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            if (allWorldGameObjects.Any(a => a.name == names[0])) {
                blockPickup = true;
                blockObject = true;
            }
            if (allWorldGameObjects.Any(a => a.name == names[1])) {
                blockVid = true;
            }
            if (allWorldGameObjects.Any(a => a.name == names[2])) {
                blockPens = true;
            }
            if (allWorldGameObjects.Any(a => a.name == names[3])) {
                blockChair = true;
            }
            if (allWorldGameObjects.Any(a => a.name == names[4])) {
                blockMirror = true;
            }
            if (allWorldGameObjects.Any(a => a.name == names[5])) {
                blockPP = true;
            }
            if (allWorldGameObjects.Any(a => a.name == names[6])) {
                blockAP = true;
            }
            if (allWorldGameObjects.Any(a => !a.name.Equals(names))) {
                blockPickup = false;
                blockObject = false;
                blockVid = false;
                blockPens = false;
                blockChair = false;
                blockMirror = false;
                blockPP = false;
                blockAP = false;
            }
            yield break;
        }
    }
}
