using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using UIExpansionKit.API;
using UnityEngine;
using UnityEngine.UI;
using ComponentToggle.Components;

namespace ComponentToggle
{
    interface Layout : ICustomLayoutedMenu, IShowableMenu { }

    class UIXMenuReplacement
    {
        public static ICustomShowableLayoutedMenu menu = ExpansionKitApi.CreateCustomQuickMenuPage(LayoutDescription.QuickMenu4Columns);

        static string color(string c, string s) { return $"<color={c}>{s}</color> "; }
        static readonly string grey = "#8f8f8f";

        static Dictionary<string, Transform> buttons = new Dictionary<string, Transform>();

        internal static GameObject MainMenuBTN;
        internal static bool blockPickup, blockObject, blockVid, blockPens, blockChair, blockMirror, blockPP, blockAP, blockPortal;

        static void UIXButton(int UIXExpandedMenuENUM, string UIXGetMethod, string buttonText, Action action, Action<GameObject> goAction)
        {
            MelonHandler.Mods.First(i => i.Info.Name == "UI Expansion Kit").Assembly.GetType("UIExpansionKit.API.ExpansionKitApi").GetMethod(UIXGetMethod,
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, new object[] {
                    UIXExpandedMenuENUM, buttonText, new Action(() => action()), new Action<GameObject>((GameObject obj) => goAction(obj))
                });
        }

        public static void Init()
        {
            if (MelonHandler.Mods.Any(m => m.Info.Name.Equals("UI Expansion Kit"))) {
                try {
                    UIXButton(0, "RegisterSimpleMenuButton", "Component\nToggle", new Action(() => {
                        if (menu == null) {
                            TheMenu();
                            menu.Show();
                        } else menu.Show();
                        UpdateText();
                    }), new Action<GameObject>((GameObject obj) => {
                        MainMenuBTN = obj;
                        obj.SetActive(Main.UIXMenu.Value);
                    }));
                    TheMenu();
                } catch (Exception e) { MelonLogger.Error("UIXMenu:\n" + e.ToString()); }
            }
        }

        static void TheMenu()
        {
            buttons.Clear();
            menu.AddSimpleButton(color("red", "Close") + "Menu", () => menu.Hide());
            menu.AddSpacer();
            menu.AddSpacer();
            menu.AddSimpleButton("Refresh", () => {
                VRCPickup.OnLevelLoad();
                _VRCSyncVideoPlayer.OnLevelLoad();
                Pens.OnLevelLoad();
                PostProcessing.OnLevelLoad();
                _VRCSyncVideoPlayer.OnLevelLoad();
                VRCAvatarPedestal.OnLevelLoad();
                VRCMirrorReflect.OnLevelLoad();
                Utilities.WorldLogic.ReCacheAllObjects();
            });

            menu.AddSimpleButton($"VRC_Pickup\n{(Main.VRC_Pickup.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockPickup) return;
                Main.VRC_Pickup.Value = !Main.VRC_Pickup.Value;
                VRCPickup.Toggle();
                UpdateText();
            }, (button) => buttons["pickups"] = button.transform);
            menu.AddSimpleButton($"VRC_Pickup\nObjects\n{(Main.VRC_Pickup_Objects.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockObject) return;
                Main.VRC_Pickup_Objects.Value = !Main.VRC_Pickup_Objects.Value;
                VRCPickup.Toggle();
                UpdateText();
            }, (button) => buttons["objects"] = button.transform);
            menu.AddSimpleButton($"Video Players\n{(Main.VRC_SyncVideoPlayer.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockVid) return;
                Main.VRC_SyncVideoPlayer.Value = !Main.VRC_SyncVideoPlayer.Value;
                _VRCSyncVideoPlayer.Toggle();
                UpdateText();
            }, (button) => buttons["vid"] = button.transform);
            menu.AddSimpleButton($"Pens\n{(Main.Pens.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockPens) return;
                Main.Pens.Value = !Main.Pens.Value;
                //Pens.Toggle();
                Menu.TogglePens.setToggleState(Main.Pens.Value, true);
                UpdateText();
            }, (button) => buttons["pens"] = button.transform);

            menu.AddSimpleButton($"Chairs\n{(Main.VRC_Station.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockChair) return;
                Main.VRC_Station.Value = !Main.VRC_Station.Value;
                UpdateText();
            }, (button) => buttons["chairs"] = button.transform);
            menu.AddSimpleButton($"Mirrors\n{(Main.VRC_MirrorReflect.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockMirror) return;
                Main.VRC_MirrorReflect.Value = !Main.VRC_MirrorReflect.Value;
                VRCMirrorReflect.Toggle();
                UpdateText();
            }, (button) => buttons["mirrors"] = button.transform);
            menu.AddSimpleButton($"Post\nProcessing\n{(Main.PostProcessing.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockPP) return;
                Main.PostProcessing.Value = !Main.PostProcessing.Value;
                PostProcessing.Toggle();
                UpdateText();
            }, (button) => buttons["pp"] = button.transform);
            menu.AddSimpleButton($"Avatar\nPedestals\n{(Main.VRC_AvatarPedestal.Value ? color("#00ff00", "ON") : color("red", "OFF"))}", () => {
                if (blockAP) return;
                Main.VRC_AvatarPedestal.Value = !Main.VRC_AvatarPedestal.Value;
                TogglePedestals();
                UpdateText();
            }, (button) => buttons["ap"] = button.transform);
        }

        static void TogglePedestals()
        {
            if (Main.VRC_AvatarPedestal.Value) VRCAvatarPedestal.Revert();
            else VRCAvatarPedestal.Disable();
        }

        static string text(string buttonName, string text)
        {
            if (buttons[buttonName] != null)
                return buttons[buttonName].GetComponentInChildren<Text>().text = text;
            else return null;
        }

        static void UpdateText()
        {
            try
            {
                if (blockPickup) text("pickups", color(grey, "DISABLED"));
                else text("pickups", $"VRC_Pickup\n{(Main.VRC_Pickup.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockObject) text("objects", color(grey, "DISABLED"));
                else text("objects", $"VRC_Pickup\nObjects\n{(Main.VRC_Pickup_Objects.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockVid) text("vid", color(grey, "DISABLED"));
                else text("vid", $"Video Players\n{(Main.VRC_SyncVideoPlayer.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockPens) text("pens", color(grey, "DISABLED"));
                else {
                    if (Menu.TogglePens != null) text("pens", $"Pens\n{(Menu.TogglePens.btnOn.activeSelf ? color("#00ff00", "ON") : color("red", "OFF"))}");
                    else text("pens", $"Pens\n{(Main.Pens.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
                }
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockChair) text("chairs", color(grey, "DISABLED"));
                else text("chairs", $"Chairs\n{(Main.VRC_Station.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockMirror) text("mirrors", color(grey, "DISABLED"));
                else text("mirrors", $"Mirrors\n{(Main.VRC_MirrorReflect.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockPP) text("pp", color(grey, "DISABLED"));
                else text("pp", $"Post\nProcessing\n{(Main.PostProcessing.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
            
            try
            {
                if (blockAP) text("ap", color(grey, "DISABLED"));
                else text("ap", $"Avatar\nPedestals\n{(Main.VRC_AvatarPedestal.Value ? color("#00ff00", "ON") : color("red", "OFF"))}");
            }
            catch (Exception e) { MelonLogger.Error($"{e}"); }
        }
    }
}
