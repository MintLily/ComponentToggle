using System.Collections.Generic;
using UnityEngine;
using VRC.SDKBase;

namespace ComponentToggle.Components
{
    public class Pedestals
    {
        public GameObject parentPedestal;
        public bool status;
    }

    class VRCAvatarPedestal
    {
        public static List<Pedestals> originalPedestals;

        public static void OnLevelLoad()
        {
            originalPedestals = new List<Pedestals>();
            foreach (VRC_AvatarPedestal vrc_AvatarPedestal in Resources.FindObjectsOfTypeAll<VRC_AvatarPedestal>())
            {
                originalPedestals.Add(new Pedestals { 
                    parentPedestal = vrc_AvatarPedestal.gameObject,
                    status = vrc_AvatarPedestal.gameObject.activeSelf });
            }
        }

        public static void Disable()
        {
            if (originalPedestals.Count != 0)
            {
                foreach (Pedestals originalPedestal in originalPedestals)
                {
                    originalPedestal.parentPedestal.SetActive(false);
                }
            }
        }

        public static void Revert()
        {
            if (originalPedestals.Count != 0)
            {
                foreach (Pedestals originalPedestal in originalPedestals)
                {
                    originalPedestal.parentPedestal.SetActive(originalPedestal.status);
                }
            }
        }
    }
}
