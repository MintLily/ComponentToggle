using System.Collections;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ComponentToggle.Components
{
    // came from and credits to https://github.com/Arion-Kun/PostProcessing (from an old version)
    class PostProcessing
    {
        public static bool WorldWasChanged;

        public static void OnLevelLoad()
        {
            MelonCoroutines.Start(DelayedEvent());
        }

        public static void Toggle()
        {
            foreach (Camera cam in Camera.allCameras)
            {
                if (cam.GetComponent<PostProcessLayer>() != null)
                {
                    if (Main.PostProcessing.Value != cam.GetComponent<PostProcessLayer>().enabled)
                    {
                        if (Main.isDebug)
                            MelonLogger.Msg(Main.PostProcessing.Value ? "Removed Post Processing" : "Re-added Post Processing");
                        cam.GetComponent<PostProcessLayer>().enabled = Main.PostProcessing.Value;
                    }
                }
            }
        }

        private static IEnumerator DelayedEvent()
        {
            yield return new WaitForSeconds(5);
            if (WorldWasChanged)
            {
                WorldWasChanged = false;
                foreach (Camera cam in Camera.allCameras)
                {
                    if (cam.GetComponent<PostProcessLayer>() != null)
                    {
                        if (Main.PostProcessing.Value != cam.GetComponent<PostProcessLayer>().enabled)
                        {
                            if (Main.isDebug)
                                MelonLogger.Msg(Main.PostProcessing.Value ? "Auto Removed Post Processing" : "Auto Re-added Post Processing");
                            cam.GetComponent<PostProcessLayer>().enabled = Main.PostProcessing.Value;
                        }
                    }
                }
            }
            yield break;
        }
    }
}
