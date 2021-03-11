using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using ComponentToggle.Utilities.Config;

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
                    gameObject.GetComponent<VRC_Pickup>().pickupable = CustomConfig.Get().VRC_Pickup;
                    gameObject.gameObject.SetActive(CustomConfig.Get().VRC_Pickup_Objects);
                }
            }
        }
    }
}
