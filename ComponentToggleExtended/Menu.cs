using ComponentToggleExtended.Utilities;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using UIExpansionKit.API;
using UnityEngine;
using UnityEngine.UI;

namespace ComponentToggleExtended {
    internal class Menu {
        static void SetValues(MelonPreferences_Entry en1, bool setEntryValue) => MelonPreferences.GetEntry<bool>(Main.melon.Identifier, en1.Identifier).Value = setEntryValue;

        public static ICustomShowableLayoutedMenu menu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescription.QuickMenu4Columns);

        static string color(string c, string s) => $"<color={c}>{s}</color> ";
        static readonly string grey = "#8f8f8f";

        private static Dictionary<string, Transform> buttons = new Dictionary<string, Transform>();

        internal static GameObject MainMenuBTN;
        private static bool runOnce_start;

        public static void Init() {
            if (MelonHandler.Mods.Any(m => m.Info.Name.Equals("UI Expansion Kit"))) {
                try {
                    ExpansionKitApi.GetExpandedMenu(ExpandedMenu.QuickMenu).AddSimpleButton("Component\nToggle", () => {
                        if (!runOnce_start) {
                            TheMenu();
                            menu.Show();
                            runOnce_start = true;
                        } else {
                            menu.Show();
                            UpdateText();
                        }
                    });
                } catch (Exception e) { MelonLogger.Error("UIXMenu:\n" + e.ToString()); }
            }
        }

        private static void TheMenu() {
            buttons.Clear();
            menu.AddSimpleButton(color("red", "Close") + "Menu", () => menu.Hide());
            menu.AddSpacer();
            menu.AddSpacer();
            menu.AddSpacer();

            menu.AddSimpleButton($"VRC_Pickup\n{(Main.VRC_Pickup.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockPickup)
                    return;
                SetValues(Main.VRC_Pickup, !Main.VRC_Pickup.Value);
                WorldObjects.ToggleAction(4);
                UpdateText();
            }, (button) => buttons["pickups"] = button.transform);
            menu.AddSimpleButton($"VRC_Pickup\nObjects\n{(Main.VRC_Pickup_Objects.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockObject)
                    return;
                SetValues(Main.VRC_Pickup_Objects, !Main.VRC_Pickup_Objects.Value);
                WorldObjects.ToggleAction(4);
                UpdateText();
            }, (button) => buttons["objects"] = button.transform);
            menu.AddSimpleButton($"Video Players\n{(Main.VRC_SyncVideoPlayer.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockVid)
                    return;
                SetValues(Main.VRC_SyncVideoPlayer, !Main.VRC_SyncVideoPlayer.Value);
                WorldObjects.ToggleAction(5);
                UpdateText();
            }, (button) => buttons["vid"] = button.transform);
            menu.AddSimpleButton($"Pens\n{(Main.Pens.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockPens)
                    return;
                SetValues(Main.Pens, !Main.Pens.Value);
                WorldObjects.ToggleAction(3);
                UpdateText();
            }, (button) => buttons["pens"] = button.transform);

            menu.AddSimpleButton($"Chairs\n{(Main.VRC_Station.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockChair)
                    return;
                SetValues(Main.VRC_Station, !Main.VRC_Station.Value);
                UpdateText();
            }, (button) => buttons["chairs"] = button.transform);
            menu.AddSimpleButton($"Mirrors\n{(Main.VRC_MirrorReflect.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockMirror)
                    return;
                SetValues(Main.VRC_MirrorReflect, !Main.VRC_MirrorReflect.Value);
                WorldObjects.ToggleAction(6);
                UpdateText();
            }, (button) => buttons["mirrors"] = button.transform);
            menu.AddSimpleButton($"Post\nProcessing\n{(Main.PostProcessing.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockPP)
                    return;
                SetValues(Main.PostProcessing, !Main.PostProcessing.Value);
                WorldObjects.ToggleAction(2);
                UpdateText();
            }, (button) => buttons["pp"] = button.transform);
            menu.AddSimpleButton($"Avatar\nPedestals\n{(Main.VRC_AvatarPedestal.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (WorldLogic.blockAP)
                    return;
                SetValues(Main.VRC_AvatarPedestal, !Main.VRC_AvatarPedestal.Value);
                if (Main.VRC_AvatarPedestal.Value)
                    WorldObjects.ToggleAction(1);
                else
                    WorldObjects.ToggleAction(0);
                UpdateText();
            }, (button) => buttons["ap"] = button.transform);
        }

        static string text(string buttonName, string text) {
            if (buttons[buttonName] != null)
                return buttons[buttonName].GetComponentInChildren<Text>().text = text;
            return null;
        }

        private static void UpdateText() {
            try {
                if (WorldLogic.blockPickup)
                    text("pickups", color(grey, "DISABLED"));
                else
                    text("pickups", $"VRC_Pickup\n{(Main.VRC_Pickup.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockObject)
                    text("objects", color(grey, "DISABLED"));
                else
                    text("objects", $"VRC_Pickup\nObjects\n{(Main.VRC_Pickup_Objects.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockVid)
                    text("vid", color(grey, "DISABLED"));
                else
                    text("vid", $"Video Players\n{(Main.VRC_SyncVideoPlayer.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockPens)
                    text("pens", color(grey, "DISABLED"));
                else
                    text("pens", $"Pens\n{(Main.Pens.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockChair)
                    text("chairs", color(grey, "DISABLED"));
                else
                    text("chairs", $"Chairs\n{(Main.VRC_Station.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockMirror)
                    text("mirrors", color(grey, "DISABLED"));
                else
                    text("mirrors", $"Mirrors\n{(Main.VRC_MirrorReflect.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockPP)
                    text("pp", color(grey, "DISABLED"));
                else
                    text("pp", $"Post\nProcessing\n{(Main.PostProcessing.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }

            try {
                if (WorldLogic.blockAP)
                    text("ap", color(grey, "DISABLED"));
                else
                    text("ap", $"Avatar\nPedestals\n{(Main.VRC_AvatarPedestal.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            } catch (Exception e) { MelonLogger.Error($"{e}"); }
        }
    }
}
