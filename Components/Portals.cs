using ComponentToggle.Utilities;
using VRC.SDKBase;

namespace ComponentToggle.Components
{
    class Portals
    {
        public static void Toggle(bool tempOn = false)
        {
            if (WorldLogic.allWorldGameObjects == null) WorldLogic.ReCacheAllObjects();
            foreach (var gameobject in WorldLogic.allWorldGameObjects)
            {
                if (tempOn) {
                    if (gameobject.GetComponentOrInChildren<VRC_PortalMarker>())
                        gameobject.transform.parent.gameObject.SetActive(true);
                } else {
                    if (gameobject.GetComponentOrInChildren<VRC_PortalMarker>())
                        gameobject.transform.parent.gameObject.SetActive(Main.VRC_Portal.Value);
                }
            }
        }
    }
}
