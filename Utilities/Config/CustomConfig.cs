using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using Newtonsoft.Json;

namespace ComponentToggle.Utilities.Config
{
    class Config
    {
        public bool VRC_Pickup { get; }
        public bool VRC_Pickup_Objects { get; }
        public bool VRC_SyncVideoPlayer { get; }
        public bool Pens { get; }
        public bool VRC_Station { get; }
        public bool VRC_MirrorReflect { get; }
        public bool PostProcessing { get; }
        public bool VRC_AvatarPedestal { get; }
    }

    static class CustomConfig
    {
        public static readonly string final = Path.Combine(Environment.CurrentDirectory, "UserData/ComponentToggleConfig.json");

        private static Config _Config { get; set; }

        public static void Load() { _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(final)); }

        public static Config Get() { return _Config; }

        public static void ConvertAndRemove()
        {
            try
            {
                bool faulted = true;
                try
                {
                    Main.VRC_Pickup.Value = Get().VRC_Pickup;
                    Main.VRC_Pickup_Objects.Value = Get().VRC_Pickup_Objects;
                    Main.VRC_SyncVideoPlayer.Value = Get().VRC_SyncVideoPlayer;
                    Main.Pens.Value = Get().Pens;
                    Main.VRC_Station.Value = Get().VRC_Station;
                    Main.VRC_MirrorReflect.Value = Get().VRC_MirrorReflect;
                    Main.PostProcessing.Value = Get().PostProcessing;
                    Main.VRC_AvatarPedestal.Value = Get().VRC_AvatarPedestal;
                    faulted = false;
                }
                catch { faulted = true; }

                if (!faulted)
                {
                    File.Delete(final);
                    Menu.setAllButtonToggleStates(false); // Set States After Conversion
                }
            }
            catch 
            {
                if (!File.Exists(final) && Main.isDebug)
                    MelonLogger.Msg("Not an error > Old Config file does not exist, ignoring function.");
            }
        }
    }
}
