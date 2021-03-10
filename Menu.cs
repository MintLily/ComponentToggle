using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using RubyButtonAPI;
using UnityEngine;
using ComponentToggle.Components;

namespace ComponentToggle
{
    class Menu
    {
        private static QMNestedButton menu;
        public static QMToggleButton TogglePickup;
        public static QMToggleButton TogglePickupObject;
        public static QMToggleButton ToggleVideoPlayers;
        public static QMToggleButton TogglePens;
        public static QMToggleButton ToggleStation;
        public static QMToggleButton ToggleMirror;
        public static QMToggleButton TogglePostProcessing;

        public static void Init()
        {
            menu = new QMNestedButton("UIElementsMenu", 0, 0, "Component\nToggle", "Opens a menu for toggling various components in VRChat.");
            menu.getBackButton().setLocation(0, 0);

            TogglePickup = new QMToggleButton(menu, 1, 0, "VRC_Pickup", () =>
            {
                Main.VRC_Pickup.Value = true;
                MelonLoader.MelonPreferences.Save();
                VRCPickup.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_Pickup.Value = false;
                MelonLoader.MelonPreferences.Save();
                VRCPickup.Toggle();
            }, "TOGGLE: Keep Objects visible, but disable you being able to pick them up.");

            TogglePickupObject = new QMToggleButton(menu, 2, 0, "Pickup Objects", () =>
            {
                Main.VRC_Pickup_Objects.Value = true;
                MelonLoader.MelonPreferences.Save();
                VRCPickup.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_Pickup_Objects.Value = false;
                MelonLoader.MelonPreferences.Save();
                VRCPickup.Toggle();
            }, "TOGGLE: Change the visibility of pickup-able objects");

            ToggleVideoPlayers = new QMToggleButton(menu, 3, 0, "Video Players", () =>
            {
                Main.VRC_SyncVideoPlayer.Value = true;
                MelonLoader.MelonPreferences.Save();
                _VRCSyncVideoPlayer.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_SyncVideoPlayer.Value = false;
                MelonLoader.MelonPreferences.Save();
                _VRCSyncVideoPlayer.Toggle();
            }, "TOGGLE: Video Players\n<color=red>Does not work in Udon Worlds</color>");

            TogglePens = new QMToggleButton(menu, 4, 0, "Pens", () =>
            {
                Main.Pens.Value = true;
                MelonLoader.MelonPreferences.Save();
                Pens.Toggle();
            }, "Disabled", () =>
            {
                Main.Pens.Value = false;
                MelonLoader.MelonPreferences.Save();
                Pens.Toggle();
            }, "TOGGLE: Pens & Erasers");

            ToggleStation = new QMToggleButton(menu, 1, 1, "Chairs", () =>
            {
                Main.VRC_Station.Value = true;
                MelonLoader.MelonPreferences.Save();
            }, "Disabled", () =>
            {
                Main.VRC_Station.Value = false;
                MelonLoader.MelonPreferences.Save();
            }, "TOGGLE: Ability to sit in chairs");

            ToggleMirror = new QMToggleButton(menu, 2, 1, "Mirrors", () =>
            {
                Main.VRC_MirrorReflect.Value = true;
                MelonLoader.MelonPreferences.Save();
                VRCMirrorReflect.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_MirrorReflect.Value = false;
                MelonLoader.MelonPreferences.Save();
                VRCMirrorReflect.Toggle();
            }, "TOGGLE: All Mirrors");

            TogglePostProcessing = new QMToggleButton(menu, 3, 1, "PostProcessing", () =>
            {
                Main.PostProcessing.Value = true;
                MelonLoader.MelonPreferences.Save();
                PostProcessing.Toggle();
            }, "Disabled", () =>
            {
                Main.PostProcessing.Value = false;
                MelonLoader.MelonPreferences.Save();
                PostProcessing.Toggle();
            }, "TOGGLE: Post Processing");

            // Sets Toggle States on UI Init
            TogglePickup.setToggleState(Main.VRC_Pickup.Value);
            TogglePickupObject.setToggleState(Main.VRC_Pickup_Objects.Value);
            ToggleVideoPlayers.setToggleState(Main.VRC_SyncVideoPlayer.Value);
            TogglePens.setToggleState(Main.Pens.Value);
            ToggleStation.setToggleState(Main.VRC_Station.Value);
            ToggleMirror.setToggleState(Main.VRC_MirrorReflect.Value);
            TogglePostProcessing.setToggleState(Main.PostProcessing.Value);
        }

        public static bool WorldWasChanged = false;

        public static IEnumerator OnLevelLoad()
        {
            yield return new WaitForSeconds(5);
            if (WorldWasChanged)
            {
                WorldWasChanged = false;
                TogglePickup.setToggleState(Main.VRC_Pickup.Value, true);
                TogglePickupObject.setToggleState(Main.VRC_Pickup_Objects.Value, true);
                ToggleVideoPlayers.setToggleState(Main.VRC_SyncVideoPlayer.Value, true);
                TogglePens.setToggleState(Main.Pens.Value, true);
                ToggleStation.setToggleState(Main.VRC_Station.Value, true);
                ToggleMirror.setToggleState(Main.VRC_MirrorReflect.Value, true);
                TogglePostProcessing.setToggleState(Main.PostProcessing.Value, true);
            }
            yield break;
        }
    }
}
