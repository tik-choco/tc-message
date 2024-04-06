using System.Collections.Generic;
using UnityEngine;

namespace TC
{
    /// <summary>
    /// C#の処理時間を計測する機能
    /// </summary>
    public class CodeTimer : MonoBehaviour
    {
        private static readonly Dictionary<string, System.Diagnostics.Stopwatch> TimeDict = new();

        private void Start()
        {
#if UNITY_EDITOR
            TimeDict.Clear();
#endif
        }

        public static void TimeStart(string key)
        {
#if UNITY_EDITOR
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            TimeDict[key] = sw;
#endif
        }

        public static void TimeEnd(string key)
        {
#if UNITY_EDITOR
            if (!TimeDict.TryGetValue(key, out var sw)) return;
            sw.Stop();
            var ts = sw.Elapsed;
            Logger.Log($"<color=#ffa500>{key}</color> <color=#7af6b4>実行時間:</color> <b>{ts.TotalMilliseconds}ms</b>");
            TimeDict.Remove(key);
#endif
        }
    }
}