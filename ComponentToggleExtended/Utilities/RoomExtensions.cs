using VRC.Core;

namespace ComponentToggleExtended.Utilities {
    internal static class RoomExtensions {
        public static bool IsInWorld() { return GetWorld() != null || GetWorldInstance() != null; }

        public static ApiWorld GetWorld() { return RoomManager.field_Internal_Static_ApiWorld_0; }

        public static ApiWorldInstance GetWorldInstance() { return RoomManager.field_Internal_Static_ApiWorldInstance_0; }
    }
}
