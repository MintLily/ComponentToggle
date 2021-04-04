using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRCSDK2;
using ComponentToggle.Utilities;
using System.Collections;
using ComponentToggle.Utilities.Config;

namespace ComponentToggle.Components
{
    class _VRCSyncVideoPlayer
    {
        public static VRC_SyncVideoPlayer[] stored_sdk2;
        public static SyncVideoPlayer[] stored_sdk3;

        public static void OnLevelLoad()
        {
            Store();
        }

        private static void Store()
        {
            if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0)
                stored_sdk3 = Resources.FindObjectsOfTypeAll<SyncVideoPlayer>();
            else
                stored_sdk2 = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();
        }

        public static void Toggle(bool tempOn = false)
        {
            if (stored_sdk2 == null) Store();
            foreach (var gameObject in stored_sdk2)
            {
                if (tempOn)
                {
                    gameObject.GetComponent<VRC_SyncVideoPlayer>().enabled = true;
                    gameObject.gameObject.SetActive(true);
                }
                else
                {
                    gameObject.GetComponent<VRC_SyncVideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                    gameObject.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                }
            }
        }
    }
}
