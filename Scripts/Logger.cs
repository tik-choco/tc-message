using UnityEngine;

namespace TC
{
    public class Logger
    {
        public static bool ShowLog = true;

        public static void Log(object message)
        {
            if (ShowLog)
            {
                Debug.Log(message);
            }
        }

        public static void LogWarning(object message)
        {
            if (ShowLog)
            {
                Debug.LogWarning(message);
            }
        }

        public static void LogError(object message)
        {
            if (ShowLog)
            {
                Debug.LogError(message);
            }
        }
    }
}