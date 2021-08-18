using ComponentToggle.Utilities;
using VRC.SDKBase;

namespace ComponentToggle.Components
{
    class Portals
    {
        public static void Toggle()
        {
            if (Utilities.GetBlockedWorlds.allWorldGameObjects == null) Utilities.GetBlockedWorlds.ReCacheAllObjects();
            foreach (var gameobject in Utilities.GetBlockedWorlds.allWorldGameObjects)
                if (gameobject.GetComponentOrInChildren<VRC_PortalMarker>())
                    gameobject.SetActive(Main.VRC_Portal.Value);
        }
    }
}
