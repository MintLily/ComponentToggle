using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using ComponentToggle.Utilities.Config;

namespace ComponentToggle.Components
{
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
                    if (CustomConfig.Get().PostProcessing != cam.GetComponent<PostProcessLayer>().enabled)
                    {
                        if (Main.isDebug)
                            MelonLogger.Msg(CustomConfig.Get().PostProcessing ? "Removed Post Processing" : "Re-added Post Processing");
                        cam.GetComponent<PostProcessLayer>().enabled = CustomConfig.Get().PostProcessing;
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
                        if (CustomConfig.Get().PostProcessing != cam.GetComponent<PostProcessLayer>().enabled)
                        {
                            if (Main.isDebug)
                                MelonLogger.Msg(CustomConfig.Get().PostProcessing ? "Auto Removed Post Processing" : "Auto Re-added Post Processing");
                            cam.GetComponent<PostProcessLayer>().enabled = CustomConfig.Get().PostProcessing;
                        }
                    }
                }
            }
            yield break;
        }
    }
}
