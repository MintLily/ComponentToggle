using System.Collections;
using MelonLoader;
//using RubyButtonAPICT;
using UnityEngine;
using ComponentToggle.Components;
using UnityEngine.UI;
using ComponentToggle.Utilities;

namespace ComponentToggle
{
    internal class Menu
    {
        public static bool WorldWasChanged = false;

        public static IEnumerator OnLevelLoad()
        {
            yield return new WaitForSeconds(5);
            if (WorldWasChanged)
                WorldWasChanged = false;
            yield break;
        }

        internal static void BlockActions(int buttonNumber)
        {
            switch (buttonNumber)
            {
                case 1:
                    VRCPickup.Toggle(true, true);
                    UIXMenuReplacement.blockPickup = true;
                    UIXMenuReplacement.blockObject = true;
                    break;
                case 2:
                    _VRCSyncVideoPlayer.Toggle(true);
                    UIXMenuReplacement.blockVid = true;
                    break;
                case 3:
                    Pens.Toggle(true);
                    UIXMenuReplacement.blockPens = true;
                    break;
                case 4:
                    UIXMenuReplacement.blockChair = true;
                    break;
                case 5:
                    VRCMirrorReflect.Toggle(true);
                    UIXMenuReplacement.blockMirror = true;
                    break;
                case 6:
                    UIXMenuReplacement.blockPP = true;
                    break;
                case 7:
                    UIXMenuReplacement.blockAP = true;
                    break;
                default:
                    UIXMenuReplacement.blockPickup = false;
                    UIXMenuReplacement.blockObject = false;
                    UIXMenuReplacement.blockVid = false;
                    UIXMenuReplacement.blockPens = false;
                    UIXMenuReplacement.blockChair = false;
                    UIXMenuReplacement.blockMirror = false;
                    UIXMenuReplacement.blockPP = false;
                    UIXMenuReplacement.blockAP = false;
                    UIXMenuReplacement.blockPortal = false;
                    break;
            }
        }
    }
}
