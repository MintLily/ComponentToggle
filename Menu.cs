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
using ComponentToggle.Utilities.Config;

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
                CustomConfig.Get().VRC_Pickup = true;
                CustomConfig.Save();
                VRCPickup.Toggle();
            }, "Disabled", () =>
            {
                CustomConfig.Get().VRC_Pickup = false;
                CustomConfig.Save();
                VRCPickup.Toggle();
            }, "TOGGLE: Keep Objects visible, but disable you being able to pick them up.");

            TogglePickupObject = new QMToggleButton(menu, 2, 0, "Pickup Objects", () =>
            {
                CustomConfig.Get().VRC_Pickup_Objects = true;
                CustomConfig.Save();
                VRCPickup.Toggle();
            }, "Disabled", () =>
            {
                CustomConfig.Get().VRC_Pickup_Objects = false;
                CustomConfig.Save();
                VRCPickup.Toggle();
            }, "TOGGLE: Change the visibility of pickup-able objects");

            ToggleVideoPlayers = new QMToggleButton(menu, 3, 0, "Video Players", () =>
            {
                CustomConfig.Get().VRC_SyncVideoPlayer = true;
                CustomConfig.Save();
                _VRCSyncVideoPlayer.Toggle();
            }, "Disabled", () =>
            {
                CustomConfig.Get().VRC_SyncVideoPlayer = false;
                CustomConfig.Save();
                _VRCSyncVideoPlayer.Toggle();
            }, "TOGGLE: Video Players\n<color=red>Does not work in Udon Worlds</color>");

            TogglePens = new QMToggleButton(menu, 4, 0, "Pens", () =>
            {
                CustomConfig.Get().Pens = true;
                CustomConfig.Save();
                Pens.Toggle();
            }, "Disabled", () =>
            {
                CustomConfig.Get().Pens = false;
                CustomConfig.Save();
                Pens.Toggle();
            }, "TOGGLE: Pens & Erasers");

            ToggleStation = new QMToggleButton(menu, 1, 1, "Chairs", () =>
            {
                CustomConfig.Get().VRC_Station = true;
                CustomConfig.Save();
            }, "Disabled", () =>
            {
                CustomConfig.Get().VRC_Station = false;
                CustomConfig.Save();
            }, "TOGGLE: Ability to sit in chairs");

            ToggleMirror = new QMToggleButton(menu, 2, 1, "Mirrors", () =>
            {
                CustomConfig.Get().VRC_MirrorReflect = true;
                CustomConfig.Save();
                VRCMirrorReflect.Toggle();
            }, "Disabled", () =>
            {
                CustomConfig.Get().VRC_MirrorReflect = false;
                CustomConfig.Save();
                VRCMirrorReflect.Toggle();
            }, "TOGGLE: All Mirrors");

            TogglePostProcessing = new QMToggleButton(menu, 3, 1, "PostProcessing", () =>
            {
                CustomConfig.Get().PostProcessing = true;
                CustomConfig.Save();
                PostProcessing.Toggle();
            }, "Disabled", () =>
            {
                CustomConfig.Get().PostProcessing = false;
                CustomConfig.Save();
                PostProcessing.Toggle();
            }, "TOGGLE: Post Processing");

            // Sets Toggle States on UI Init
            TogglePickup.setToggleState(CustomConfig.Get().VRC_Pickup);
            TogglePickupObject.setToggleState(CustomConfig.Get().VRC_Pickup_Objects);
            ToggleVideoPlayers.setToggleState(CustomConfig.Get().VRC_SyncVideoPlayer);
            TogglePens.setToggleState(CustomConfig.Get().Pens);
            ToggleStation.setToggleState(CustomConfig.Get().VRC_Station);
            ToggleMirror.setToggleState(CustomConfig.Get().VRC_MirrorReflect);
            TogglePostProcessing.setToggleState(CustomConfig.Get().PostProcessing);
        }

        public static bool WorldWasChanged = false;

        public static IEnumerator OnLevelLoad()
        {
            yield return new WaitForSeconds(5);
            if (WorldWasChanged)
            {
                WorldWasChanged = false;
                TogglePickup.setToggleState(CustomConfig.Get().VRC_Pickup, true);
                TogglePickupObject.setToggleState(CustomConfig.Get().VRC_Pickup_Objects, true);
                ToggleVideoPlayers.setToggleState(CustomConfig.Get().VRC_SyncVideoPlayer, true);
                TogglePens.setToggleState(CustomConfig.Get().Pens, true);
                ToggleStation.setToggleState(CustomConfig.Get().VRC_Station, true);
                ToggleMirror.setToggleState(CustomConfig.Get().VRC_MirrorReflect, true);
                TogglePostProcessing.setToggleState(CustomConfig.Get().PostProcessing, true);
            }
            yield break;
        }
    }
}
