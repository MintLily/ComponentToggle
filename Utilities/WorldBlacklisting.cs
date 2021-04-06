using System.Collections;
using System.Linq;
using System.Net;
using MelonLoader;
using Newtonsoft.Json;
using UnityEngine;

namespace ComponentToggle.Utilities
{
    public class GetWorlds
    {
        public string WorldName;
        public string WorldID;
        public int buttonNumber;
    }

    public static class GetBlockedWorlds
    {
        private static string ParsedWorldList;

        public static GetWorlds[] Worlds { get; internal set; }

        public static void Init()
        {
            MelonCoroutines.Start(SummomList());
        }

        private static IEnumerator SummomList()
        {
            string url = "https://raw.githubusercontent.com/KortyBoi/ComponentToggle/master/Utilities/Blacklists/Worlds.json";
            WebClient WorldList = new WebClient();
            ParsedWorldList = WorldList.DownloadString(url);

            yield return new WaitForSeconds(0.5f);

            Worlds = JsonConvert.DeserializeObject<GetWorlds[]>(ParsedWorldList);
            yield break;
        }

        public static IEnumerator DelayedLoad()
        {
            yield return new WaitForSecondsRealtime(2);
            try
            {
                if (RoomManager.field_Internal_Static_ApiWorld_0 != null)
                {
                    if (Worlds.Any(x => x.WorldID.Equals(RoomManager.field_Internal_Static_ApiWorld_0.id)))
                    {
                        MelonLogger.Msg("You have entered a protected world. Some buttons will not be toggleable.");
                             if (Worlds.Any(x => x.buttonNumber.Equals(1))) ComponentToggle.Menu.BlockActions(1);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(2))) ComponentToggle.Menu.BlockActions(2);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(3))) ComponentToggle.Menu.BlockActions(3);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(4))) ComponentToggle.Menu.BlockActions(4);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(5))) ComponentToggle.Menu.BlockActions(5);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(6))) ComponentToggle.Menu.BlockActions(6);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(7))) ComponentToggle.Menu.BlockActions(7);
                    }
                    else
                    {
                        ComponentToggle.Menu.BlockActions(99);
                    }

                    if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0)
                    {
                        ComponentToggle.Menu.BlockActions(2);
                        if (Main.isDebug)
                            MelonLogger.Msg("Detected SDK3 World, VideoPlayer Toggle Blocked");
                    }
                    else
                    {
                        ComponentToggle.Menu.BlockActions(99);
                    }
                }
            }
            catch { MelonLogger.Error("Failed to Apply Actions or Read from list of worlds"); }
            yield break;
        }
    }
}
