using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using VRCSDK2;
using RenderHeads.Media.AVProVideo;
using VRC_Pickup = VRC.SDKBase.VRC_Pickup;
using Object = UnityEngine.Object;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using MelonLoader;
using ComponentToggleExtended.Utilities;
using UnhollowerBaseLib;

namespace ComponentToggleExtended {
    public class PedestalManager {
        public GameObject parentPedestal;
        public bool status;
    }

    internal class WorldObjects {
        public static List<PedestalManager> theWorldPedestals;
        private static GameObject[] Pens;
        private static int countVidCom, countMirrors, countPedestals;

        private static Il2CppArrayBase<VRC_Pickup> pickups;

        public static void DoActions() {
            WorldLogic.ReCacheAllObjects();
            countVidCom = 0;
            countMirrors = 0;
            countPedestals = 0;

            pickups = Object.FindObjectsOfType<VRC_Pickup>();
            foreach (var obj in pickups) {
                obj.GetComponent<VRC_Pickup>().pickupable = Main.VRC_Pickup.Value;
                obj.gameObject.SetActive(Main.VRC_Pickup_Objects.Value);
            }

            if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0) {
                var SyncVidPl = Resources.FindObjectsOfTypeAll<SyncVideoPlayer>();
                var MediaPl = Resources.FindObjectsOfTypeAll<MediaPlayer>();
                var VidPl = Resources.FindObjectsOfTypeAll<VideoPlayer>();
                var Mirror = Object.FindObjectsOfType<VRC.SDK3.Components.VRCMirrorReflection>();
                foreach (var obj in SyncVidPl) {
                    obj.GetComponent<SyncVideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                    obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    countVidCom++;
                }
                foreach (var obj in MediaPl) {
                    obj.GetComponent<MediaPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                    obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    countVidCom++;
                }
                foreach (var obj in VidPl) {
                    obj.GetComponent<VideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                    obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    countVidCom++;
                }
                foreach (var obj in Mirror) {
                    obj.GetComponent<VRC.SDK3.Components.VRCMirrorReflection>().enabled = Main.VRC_MirrorReflect.Value;
                    countMirrors++;
                }
            } else {
                var SyncVidPl = Resources.FindObjectsOfTypeAll<VRC_SyncVideoPlayer>();
                var VidPl = Resources.FindObjectsOfTypeAll<VideoPlayer>();
                var Mirror = Object.FindObjectsOfType<VRC_MirrorReflection>();
                foreach (var obj in SyncVidPl) {
                    obj.GetComponent<VRC_SyncVideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                    obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    countVidCom++;
                }
                foreach (var obj in VidPl) {
                    obj.GetComponent<VideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                    obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                    countVidCom++;
                }
                foreach (var obj in Mirror) {
                    obj.GetComponent<VRC_MirrorReflection>().enabled = Main.VRC_MirrorReflect.Value;
                    countMirrors++;
                }
            }

            if (theWorldPedestals != null)
                theWorldPedestals.Clear();
            theWorldPedestals = new List<PedestalManager>();
            var AvatarPedestals = Resources.FindObjectsOfTypeAll<VRC.SDKBase.VRC_AvatarPedestal>();
            foreach (var ap in AvatarPedestals) {
                theWorldPedestals.Add(new PedestalManager {
                    parentPedestal = ap.gameObject,
                    status = ap.gameObject.activeSelf
                });
                countPedestals++;
            }

            Pens = (from x in Object.FindObjectsOfType<GameObject>()
                     where x.name.ToLower().Contains("pen") | x.name.ToLower().Contains("marker") | x.name.ToLower().Contains("grip")
                     select x).ToArray();

            MelonCoroutines.Start(DelayedEvent());

            Main.Logs($"Mirrors Counted:                  {countMirrors}", Main.isDebug);
            Main.Logs($"Video Player Components Counted:  {countVidCom}", Main.isDebug);
            Main.Logs($"Pedestals Counted:                {countPedestals}", Main.isDebug);
            Main.Logs($"Pens Counted:                     {Pens.Length}", Main.isDebug);
            Main.Logs($"Pickups Counted:                  {pickups.Count}", Main.isDebug);
        }

        private static IEnumerator DelayedEvent() {
            yield return new WaitForSeconds(5);
            if (Main.WorldWasChanged) {
                Main.WorldWasChanged = false;
                foreach (Camera cam in Camera.allCameras) {
                    if (cam.GetComponent<PostProcessLayer>() != null) {
                        if (Main.PostProcessing.Value != cam.GetComponent<PostProcessLayer>().enabled) {
                            Main.Logs(Main.PostProcessing.Value ? "Auto Removed Post Processing" : "Auto Re-added Post Processing", Main.isDebug);
                            cam.GetComponent<PostProcessLayer>().enabled = Main.PostProcessing.Value;
                        }
                    }
                }
            }
            yield break;
        }

        public static void ToggleAction(int step) {
            switch (step) {
                case 0: // Disable Pedestals
                    if (theWorldPedestals.Count != 0) {
                        foreach (var original in theWorldPedestals) {
                            original.parentPedestal.SetActive(false);
                        }
                    }
                    break;

                case 1: // Revert Pedestals
                    if (theWorldPedestals.Count != 0) {
                        foreach (var original in theWorldPedestals) {
                            original.parentPedestal.SetActive(original.status);
                        }
                    }
                    break;

                case 2: // Toggle Post Processing
                    foreach (Camera cam in Camera.allCameras) {
                        if (cam.GetComponent<PostProcessLayer>() != null) {
                            if (Main.PostProcessing.Value != cam.GetComponent<PostProcessLayer>().enabled) {
                                Main.Logs(Main.PostProcessing.Value ? "Auto Removed Post Processing" : "Auto Re-added Post Processing", Main.isDebug);
                                cam.GetComponent<PostProcessLayer>().enabled = Main.PostProcessing.Value;
                            }
                        }
                    }
                    break;

                case 3: // Toggle Pens
                    foreach (var p in Pens)
                        p.gameObject.SetActive(Main.Pens.Value);
                    break;

                case 4: // Toggle Pickup / Pickup Objects
                    foreach (var obj in pickups) {
                        obj.GetComponent<VRC_Pickup>().pickupable = Main.VRC_Pickup.Value;
                        obj.gameObject.SetActive(Main.VRC_Pickup_Objects.Value);
                    }
                    break;

                case 5: // Toggle Video Players
                    if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0) {
                        foreach (var obj in Resources.FindObjectsOfTypeAll<SyncVideoPlayer>()) {
                            obj.GetComponent<SyncVideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                            obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                        }
                        foreach (var obj in Resources.FindObjectsOfTypeAll<MediaPlayer>()) {
                            obj.GetComponent<MediaPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                            obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                        }
                        foreach (var obj in Resources.FindObjectsOfTypeAll<VideoPlayer>()) {
                            obj.GetComponent<VideoPlayer>().enabled = Main.VRC_SyncVideoPlayer.Value;
                            obj.gameObject.SetActive(Main.VRC_SyncVideoPlayer.Value);
                        }
                    }

                        break;

                case 6: // Toggle Mirrors
                    if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0) {
                        foreach (var obj in Object.FindObjectsOfType<VRC.SDK3.Components.VRCMirrorReflection>()) {
                            obj.GetComponent<VRC.SDK3.Components.VRCMirrorReflection>().enabled = Main.VRC_MirrorReflect.Value;
                        }
                    }
                    else {
                        foreach (var obj in Object.FindObjectsOfType<VRC_MirrorReflection>()) {
                            obj.GetComponent<VRC_MirrorReflection>().enabled = Main.VRC_MirrorReflect.Value;
                        }
                    }
                    break;
            }
        }
    }
}
