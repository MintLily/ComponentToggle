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
using ComponentToggle.Utilities.Config;

namespace ComponentToggle.Components
{
    class VRCMirrorReflect
    {
        public static VRCSDK2.VRC_MirrorReflection[] stored_sdk2;
        public static MirrorReflection[] stored_sdk3;

        public static void OnLevelLoad()
        {
            Store();
        }

        private static void Store()
        {
            stored_sdk2 = UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_MirrorReflection>();
            stored_sdk3 = UnityEngine.Object.FindObjectsOfType<MirrorReflection>();
        }

        public static void Toggle(bool tempOn = false)
        {
            if (stored_sdk2 == null || stored_sdk3 != null) Store();

            foreach (var gameObject in stored_sdk2)
            {
                if (tempOn)
                    gameObject.GetComponent<VRCSDK2.VRC_MirrorReflection>().enabled = true;
                else
                    gameObject.GetComponent<VRCSDK2.VRC_MirrorReflection>().enabled = Main.VRC_MirrorReflect.Value;
            }

            foreach (var gameObject in stored_sdk3)
            {
                if (tempOn)
                    gameObject.GetComponent<MirrorReflection>().enabled = true;
                else
                    gameObject.GetComponent<MirrorReflection>().enabled = Main.VRC_MirrorReflect.Value;
            }
        }
    }
}
