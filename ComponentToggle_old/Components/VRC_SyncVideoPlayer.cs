using UnityEngine;
using UnityEngine.Video;
using VRCSDK2;
using ComponentToggle.Utilities;
using RenderHeads.Media.AVProVideo;

namespace ComponentToggle.Components
{
    class _VRCSyncVideoPlayer
    {
        public static VRC_SyncVideoPlayer[] stored_sdk2;
        public static SyncVideoPlayer[] stored_sdk3;
        public static MediaPlayer[] stored_sdk3_2;
        public static VideoPlayer[] stored_sdk3_3;

        public static void OnLevelLoad() { Store(); }

        private static void Store()
        {
            if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0)
            {
                stored_sdk3 = Resources.FindObjectsOfTypeAll<SyncVideoPlayer>();
                stored_sdk3_2 = Resources.FindObjectsOfTypeAll<MediaPlayer>();
                stored_sdk3_3 = Resources.FindObjectsOfTypeAll<VideoPlayer>();
            }
            else
                stored_sdk2 = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();
        }

        public static void Toggle(bool tempOn = false)
        {
            if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0)
            {
                if (stored_sdk3 == null || stored_sdk3_2 == null || stored_sdk3_3 == null) Store();
                foreach (var gameObject in stored_sdk3)
                {
                    if (tempOn)
                    {
                        gameObject.GetComponent<SyncVideoPlayer>().enabled = true;
                        gameObject.gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.GetComponent<SyncVideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                        gameObject.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    }
                }

                foreach (var gameObject in stored_sdk3_2)
                {
                    if (tempOn)
                    {
                        gameObject.GetComponent<MediaPlayer>().enabled = true;
                        gameObject.gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.GetComponent<MediaPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                        gameObject.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    }
                }

                foreach (var gameObject in stored_sdk3_3)
                {
                    if (tempOn)
                    {
                        gameObject.GetComponent<VideoPlayer>().enabled = true;
                        gameObject.gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.GetComponent<VideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                        gameObject.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    }
                }
            }
            else
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
}
