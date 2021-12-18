using UnityEngine;
using System.Collections;
using System.Linq;

namespace ComponentToggle.Utilities
{
    static class WorldLogic
    {
        internal static readonly string Base = "CTBlockAction_";
        internal static readonly string[] names = { $"{Base}1", $"{Base}2", $"{Base}3", $"{Base}4", $"{Base}5", $"{Base}6", $"{Base}7" };
        public static GameObject[] allWorldGameObjects;
        public static void ReCacheAllObjects() => allWorldGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();

        internal static IEnumerator CheckWorld()
        {
            if (RoomManager.field_Internal_Static_ApiWorld_0 == null) yield break;
            yield return new WaitForSecondsRealtime(2);
            allWorldGameObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
            if (allWorldGameObjects.Any(a => a.name == names[0])) ComponentToggle.Menu.BlockActions(1);
            if (allWorldGameObjects.Any(a => a.name == names[1])) ComponentToggle.Menu.BlockActions(2);
            if (allWorldGameObjects.Any(a => a.name == names[2])) ComponentToggle.Menu.BlockActions(3);
            if (allWorldGameObjects.Any(a => a.name == names[3])) ComponentToggle.Menu.BlockActions(4);
            if (allWorldGameObjects.Any(a => a.name == names[4])) ComponentToggle.Menu.BlockActions(5);
            if (allWorldGameObjects.Any(a => a.name == names[5])) ComponentToggle.Menu.BlockActions(6);
            if (allWorldGameObjects.Any(a => a.name == names[6])) ComponentToggle.Menu.BlockActions(7);
            if (allWorldGameObjects.Any(a => !a.name.Equals(names))) ComponentToggle.Menu.BlockActions(-99);
            yield break;
        }
    }
}
