using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
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
        private static HttpClient client = new HttpClient();

        public static GetWorlds[] Worlds { get; set; }

        public static void Init()
        {
            MelonCoroutines.Start(SummomList());
        }

        private static IEnumerator SummomList()
        {
            string url = "https://raw.githubusercontent.com/KortyBoi/ComponentToggle/master/Utilities/Blacklists/Worlds.json";
            WebClient WorldList = new WebClient();
            ParsedWorldList = WorldList.DownloadString(url);
            //try { ParsedWorldList = client.GetStringAsync(url).GetAwaiter().GetResult(); }
            //catch { MelonLogger.Error("Failed to get Blacklisted World List from GitHub"); }

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
                             if (Worlds.Any(x => x.buttonNumber.Equals(1))) Menu.BlockActions(1);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(2))) Menu.BlockActions(2);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(3))) Menu.BlockActions(3);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(4))) Menu.BlockActions(4);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(5))) Menu.BlockActions(5);
                        else if (Worlds.Any(x => x.buttonNumber.Equals(6))) Menu.BlockActions(6);
                    }
                    else
                    {
                        Menu.BlockActions(99);
                    }
                    if (RoomExtensions.GetWorld() != null && Resources.FindObjectsOfTypeAll<VRC.SDK3.Components.VRCSceneDescriptor>().Count > 0)
                    {
                        Menu.BlockActions(2);
                        if (Main.isDebug)
                            MelonLogger.Msg("Detected SDK3 World, VideoPlayer Toggle Blocked");
                    }
                }
            }
            catch { MelonLogger.Error("Failed to Apply Actions or Read from list of worlds"); }
            yield break;
        }
    }
}
