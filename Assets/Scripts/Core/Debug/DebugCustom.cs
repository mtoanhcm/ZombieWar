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
    }
}
