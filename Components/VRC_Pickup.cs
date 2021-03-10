using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using VRC;
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

        public static void Toggle()
        {
            if (stored == null) Store();

            foreach (var gameObject in stored)
            {
                gameObject.GetComponent<VRC_Pickup>().pickupable = Main.VRC_Pickup.Value;
                gameObject.gameObject.SetActive(Main.VRC_Pickup_Objects.Value);
            }
        }
    }
}
