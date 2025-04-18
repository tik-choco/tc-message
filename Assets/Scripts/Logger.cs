using UnityEngine;
using System;
using System.Runtime.CompilerServices;

namespace TC
{
    public class Logger
    {
        public static Level LogLevel = Level.Info;

        public enum Level
        {
            None,
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }

        [Serializable]
        private class LogFormat
        {
            public string timestamp;
            public string level;
            public string message;
            public string caller;

            public void Set(string timestamp, string level, string caller, string message)
            {
                this.timestamp = timestamp;
                this.level = level;
                this.caller = caller;
                this.message = message;
            }
        }

        private static readonly LogFormat LOGFormat = new();

        private static string FormatJson(
            Level level,
            object message,
            string filePath,
            int lineNumber)
        {
            LOGFormat.Set(
                timestamp:DateTime.UtcNow.ToString("o"),
                level:GetLevelText(level),
                caller:$"{ShortenPath(filePath)}:{lineNumber}",
                message:message?.ToString()
            );
            return JsonUtility.ToJson(LOGFormat);
        }

        private static string GetLevelText(Level level)
        {
            switch (level)
            {
                case Level.Debug:
                    return "<color=#00ff00>DEBUG</color>";
                case Level.Info:
                    return "<color=#00ffff>INFO</color>";
                case Level.Warning:
                    return "<color=#ffff00>WARNING</color>";
                case Level.Error:
                    return "<color=#ffb6b7>ERROR</color>";
                case Level.Fatal:
                    return "<color=#c8c8ff>FATAL</color>";
                default:
                    return "";
            }
        }

        private static string ShortenPath(string path)
        {
            var parts = path.Replace("\\", "/").Split('/');
            if (parts.Length >= 2)
            {
                return parts[^2] + "/" + parts[^1];
            }
            return parts[^1];
        }

        private static bool ShouldLog(Level level)
        {
            return level >= LogLevel;
        }

        public static void Info(object message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            if (ShouldLog(Level.Info))
                UnityEngine.Debug.Log(FormatJson(Level.Info, message, file, line));
        }

        public static void Debug(object message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            if (ShouldLog(Level.Debug))
                UnityEngine.Debug.Log(FormatJson(Level.Debug, message, file, line));
        }

        public static void Warning(object message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            if (ShouldLog(Level.Warning))
                UnityEngine.Debug.LogWarning(FormatJson(Level.Warning, message, file, line));
        }

        public static void Error(object message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            if (ShouldLog(Level.Error))
                UnityEngine.Debug.LogError(FormatJson(Level.Error, message, file, line));
        }

        public static void Fatal(object message,
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            if (ShouldLog(Level.Fatal))
            {
                UnityEngine.Debug.LogError(FormatJson(Level.Fatal, message, file, line));
            }
        }
    }
}
