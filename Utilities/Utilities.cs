using System.Collections.Generic;
using UnityEngine;
using VRC.Core;

namespace ComponentToggle.Utilities
{
    public class RoomExtensions
    {
        public static bool IsInWorld()
        {
            return GetWorld() != null || GetWorldInstance() != null;
        }

        public static ApiWorld GetWorld()
        {
            return RoomManager.field_Internal_Static_ApiWorld_0;
        }

        public static ApiWorldInstance GetWorldInstance()
        {
            return RoomManager.field_Internal_Static_ApiWorldInstance_0;
        }
    }
}
