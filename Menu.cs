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
    internal class Menu
    {
        public static QMNestedButton menu;
        public static QMToggleButton TogglePickup;
        public static QMToggleButton TogglePickupObject;
        public static QMToggleButton ToggleVideoPlayers;
        public static QMToggleButton TogglePens;
        public static QMToggleButton ToggleStation;
        public static QMToggleButton ToggleMirror;
        public static QMToggleButton TogglePostProcessing;
        public static QMToggleButton TogglePedestal;

        public static void Init()
        {
            menu = new QMNestedButton("UIElementsMenu", 0, 0, "Component\nToggle", "Opens a menu for toggling various components in VRChat.");
            menu.getBackButton().setLocation(0, 0);

            TogglePickup = new QMToggleButton(menu, 1, 0, "VRC_Pickup", () =>
            {
                Main.VRC_Pickup.Value = true;
                VRCPickup.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_Pickup.Value = false;
                VRCPickup.Toggle();
            }, "TOGGLE: Keep Objects visible, but disable you being able to pick them up.");

            TogglePickupObject = new QMToggleButton(menu, 2, 0, "Pickup Objects", () =>
            {
                Main.VRC_Pickup_Objects.Value = true;
                VRCPickup.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_Pickup_Objects.Value = false;
                VRCPickup.Toggle();
            }, "TOGGLE: Change the visibility of pickup-able objects");

            ToggleVideoPlayers = new QMToggleButton(menu, 3, 0, "Video Players", () =>
            {
                Main.VRC_SyncVideoPlayer.Value = true;
                _VRCSyncVideoPlayer.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_SyncVideoPlayer.Value = false;
                _VRCSyncVideoPlayer.Toggle();
            }, "TOGGLE: Video Players\n<color=red>Does not work in Udon Worlds</color>");

            TogglePens = new QMToggleButton(menu, 4, 0, "Pens", () =>
            {
                Main.Pens.Value = true;
                Pens.Toggle();
            }, "Disabled", () =>
            {
                Main.Pens.Value = false;
                Pens.Toggle();
            }, "TOGGLE: Pens & Erasers");

            ToggleStation = new QMToggleButton(menu, 1, 1, "Chairs", () =>
            {
                Main.VRC_Station.Value = true;
            }, "Disabled", () =>
            {
                Main.VRC_Station.Value = false;
            }, "TOGGLE: Ability to sit in chairs");

            ToggleMirror = new QMToggleButton(menu, 2, 1, "Mirrors", () =>
            {
                Main.VRC_MirrorReflect.Value = true;
                VRCMirrorReflect.Toggle();
            }, "Disabled", () =>
            {
                Main.VRC_MirrorReflect.Value = false;
                VRCMirrorReflect.Toggle();
            }, "TOGGLE: All Mirrors");

            TogglePostProcessing = new QMToggleButton(menu, 3, 1, "PostProcessing", () =>
            {
                Main.PostProcessing.Value = true;
                PostProcessing.Toggle();
            }, "Disabled", () =>
            {
                Main.PostProcessing.Value = false;
                PostProcessing.Toggle();
            }, "TOGGLE: Post Processing");

            TogglePedestal = new QMToggleButton(menu, 4, 1, "Avatar\nPedestals", () =>
            {
                Main.VRC_AvatarPedestal.Value = true;
                VRCAvatarPedestal.Revert();
            }, "Disabled", () =>
            {
                Main.VRC_AvatarPedestal.Value = false;
                VRCAvatarPedestal.Disable();
            }, "TOGGLE: Avatar Pedestals throughout the world");

            // Sets Toggle States on UI Init
            setAllButtonToggleStates(false);
        }

        public static void setAllButtonToggleStates(bool Invoke)
        {
            TogglePickup.setToggleState(Main.VRC_Pickup.Value, Invoke);
            TogglePickupObject.setToggleState(Main.VRC_Pickup_Objects.Value, Invoke);
            ToggleVideoPlayers.setToggleState(Main.VRC_SyncVideoPlayer.Value, Invoke);
            TogglePens.setToggleState(Main.Pens.Value, Invoke);
            ToggleStation.setToggleState(Main.VRC_Station.Value, Invoke);
            ToggleMirror.setToggleState(Main.VRC_MirrorReflect.Value, Invoke);
            TogglePostProcessing.setToggleState(Main.PostProcessing.Value, Invoke);
            TogglePedestal.setToggleState(Main.VRC_AvatarPedestal.Value, Invoke);
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
