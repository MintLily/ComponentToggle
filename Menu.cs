using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using RubyButtonAPICT;
using UnityEngine;
using ComponentToggle.Components;
using ComponentToggle.Utilities.Config;

namespace ComponentToggle
{
    public class Menu
    {
        public static QMNestedButton menu;
        private static QMToggleButton TogglePickup;
        private static QMToggleButton TogglePickupObject;
        private static QMToggleButton ToggleVideoPlayers;
        private static QMToggleButton TogglePens;
        private static QMToggleButton ToggleStation;
        private static QMToggleButton ToggleMirror;
        private static QMToggleButton TogglePostProcessing;
        private static QMToggleButton TogglePedestal;

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

            TogglePedestal = new QMToggleButton(menu, 4, 1, "Avatar\nPedestals", () =>
            {
                CustomConfig.Get().VRC_AvatarPedestal = true;
                CustomConfig.Save();
                VRCAvatarPedestal.Revert();
            }, "Disabled", () =>
            {
                CustomConfig.Get().VRC_AvatarPedestal = false;
                CustomConfig.Save();
                VRCAvatarPedestal.Disable();
            }, "TOGGLE: Avatar Pedestals throughout the world");

            // Sets Toggle States on UI Init
            setAllButtonToggleStates(false);
        }

        public static void setAllButtonToggleStates(bool Invoke)
        {
            TogglePickup.setToggleState(CustomConfig.Get().VRC_Pickup, Invoke);
            TogglePickupObject.setToggleState(CustomConfig.Get().VRC_Pickup_Objects, Invoke);
            ToggleVideoPlayers.setToggleState(CustomConfig.Get().VRC_SyncVideoPlayer, Invoke);
            TogglePens.setToggleState(CustomConfig.Get().Pens, Invoke);
            ToggleStation.setToggleState(CustomConfig.Get().VRC_Station, Invoke);
            ToggleMirror.setToggleState(CustomConfig.Get().VRC_MirrorReflect, Invoke);
            TogglePostProcessing.setToggleState(CustomConfig.Get().PostProcessing, Invoke);
            TogglePedestal.setToggleState(CustomConfig.Get().VRC_AvatarPedestal , Invoke);
        }

        public static bool WorldWasChanged = false;

        public static IEnumerator OnLevelLoad()
        {
            yield return new WaitForSeconds(5);
            if (WorldWasChanged)
            {
                WorldWasChanged = false;
                setAllButtonToggleStates(true);
            }
            yield break;
        }

        public static void BlockActions(int buttonNumber)
        {
            switch (buttonNumber)
            {
                case 1:
                    TogglePickup.Disabled(true);
                    TogglePickupObject.Disabled(true);
                    VRCPickup.Toggle(true);
                    break;
                case 2:
                    ToggleVideoPlayers.Disabled(true);
                    _VRCSyncVideoPlayer.Toggle(true);
                    break;
                case 3:
                    TogglePens.Disabled(true);
                    Pens.Toggle(true);
                    break;
                case 4:
                    ToggleStation.Disabled(true);
                    break;
                case 5:
                    ToggleMirror.Disabled(true);
                    VRCMirrorReflect.Toggle(true);
                    break;
                case 6:
                    TogglePostProcessing.Disabled(true);
                    break;
                case 7:
                    TogglePedestal.Disabled(true);
                    break;
                default:
                    TogglePickup.Disabled(false);
                    TogglePickupObject.Disabled(false);
                    ToggleVideoPlayers.Disabled(false);
                    TogglePens.Disabled(false);
                    ToggleStation.Disabled(false);
                    ToggleMirror.Disabled(false);
                    TogglePostProcessing.Disabled(false);
                    TogglePedestal.Disabled(false);
                    break;
            }
        }
    }
}
