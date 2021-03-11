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
        public bool VRC_Pickup = true;
        public bool VRC_Pickup_Objects = true;
        public bool VRC_SyncVideoPlayer = true;
        public bool Pens = true;
        public bool VRC_Station = true;
        public bool VRC_MirrorReflect = true;
        public bool PostProcessing = true;
    }

    static class CustomConfig
    {
        private static readonly string final = Path.Combine(Environment.CurrentDirectory, "UserData/ComponentToggleConfig.json");

        private static Config _Config { get; set; }

        public static void Save()
        {
            File.WriteAllText(final, JsonConvert.SerializeObject(_Config, Formatting.Indented));
        }

        public static void CheckExistence()
        {
            if (!File.Exists(final))
                File.WriteAllText(final, JsonConvert.SerializeObject(new Config(), Formatting.Indented));
            Load();
        }

        public static void Load() { _Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(final)); }

        public static Config Get() { return _Config; }
    }
}
