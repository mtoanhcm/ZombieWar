using UnityEngine;

namespace ZombieWar.Core
{
    public static class DebugCustom
    {
        public static void Log(string message, Color color = default)
        {
#if ENABLE_BUGLOG
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>");
#endif
        }

        public static void LogWarning(string message)
        {
#if ENABLE_BUGLOG
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(Color.yellow)}>{message}</color>");
#endif
        }

        public static void LogError(string message)
        {
#if ENABLE_BUGLOG
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(Color.red)}>{message}</color>");
#endif
        }

        public static void DrawRay(Vector3 start, Vector3 direction, Color color = default, float duration = 0.1f)
        {
#if ENABLE_BUGLOG
            Debug.DrawRay(start, direction, color, duration);
#endif
        }
    }
}
