using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;

namespace ComponentToggle.Utilities
{
    public class RoomExtensions
    {
        public static bool IsInWorld() { return GetWorld() != null || GetWorldInstance() != null; }

        public static ApiWorld GetWorld() { return RoomManager.field_Internal_Static_ApiWorld_0; }

        public static ApiWorldInstance GetWorldInstance() { return RoomManager.field_Internal_Static_ApiWorldInstance_0; }
    }

    public class Menu
    {
        public static IEnumerator AllowToolTipTextColor()
        {
            try
            {
                GameObject TooltipText = GameObject.Find("UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_ToolTip/_ToolTipPanel/Text");
                TooltipText.GetComponentInChildren<Text>().supportRichText = true;
            }
            catch { MelonLoader.MelonLogger.Error("Failed to make ToolipText supportRichText"); }
            try
            {
                GameObject ALTTooltipText = GameObject.Find("UserInterface/QuickMenu/QuickMenu_NewElements/_CONTEXT/QM_Context_User_Hover/_ToolTipPanel/Text");
                ALTTooltipText.GetComponentInChildren<Text>().supportRichText = true;
            }
            catch { MelonLoader.MelonLogger.Error("Failed to make ALTTooltipText supportRichText"); }
            yield break;
        }
    }

    static class Extensions
    {
        public static T GetComponentOrInChildren<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null) return gameObject.GetComponentInChildren<T>();
            return component;
        }
    }
}
