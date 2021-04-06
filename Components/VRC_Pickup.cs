using VRC.SDKBase;

namespace ComponentToggle.Components
{
    class VRCPickup
    {
        public static VRC_Pickup[] stored;

        public static void OnLevelLoad()
        {
            Store();
        }

        private static void Store()
        {
            stored = UnityEngine.Object.FindObjectsOfType<VRC_Pickup>();
        }

        public static void Toggle(bool tempOn = false)
        {
            if (stored == null) Store();

            foreach (var gameObject in stored)
            {
                if (tempOn)
                {
                    gameObject.GetComponent<VRC_Pickup>().pickupable = true;
                    gameObject.gameObject.SetActive(true);
                }
                else
                {
                    gameObject.GetComponent<VRC_Pickup>().pickupable = Main.VRC_Pickup.Value;
                    gameObject.gameObject.SetActive(Main.VRC_Pickup_Objects.Value);
                }
            }
        }
    }
}
