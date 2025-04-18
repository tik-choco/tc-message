using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TC
{
    /// <summary>
    /// メッセージ管理
    /// </summary>
    public class Message : IDisposable
    {
        private static readonly Dictionary<string, Delegate> Functions = new();



        #region Register
        public static void Register(string key, Action function)
        {
            AddFunction(key, function);
        }

        public static void Register(string key, Delegate function)
        {
            AddFunction(key, function);
        }

        public static void Register<T>(string key, Action<T> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T1, T2>(string key, Action<T1, T2> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T1, T2, T3>(string key, Action<T1, T2, T3> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T>(string key, Func<T> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T1, T2>(string key, Func<T1, T2> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T1, T2, T3>(string key, Func<T1, T2, T3> function)
        {
            AddFunction(key, function);
        }

        public static void Register<T1, T2, T3, T4>(string key, Func<T1, T2, T3, T4> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 共通処理
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        private static void AddFunction(string key, Delegate function)
        {
            Functions.TryAdd(key, function);
            Logger.Debug($"<color=#00f5ff>[Subscribe] {key}</color>");
        }

        public static bool Unregister(string key)
        {
            if (Functions.ContainsKey(key))
            {
                Logger.Debug($"<color=#ffbaf4>[Unsubscribe] {key}</color>");
                Functions.Remove(key);
                return true;
            }

            Logger.Warning($"<color=#ffc500>[Failed][Unsubscribe] {key}</color>");
            return false;
        }
        #endregion

        #region Call
        public static bool Call(string key, params object[] args)
        {
            if (SendInternal(key))
            {
                Functions[key].DynamicInvoke(args);
                return true;
            }

            return false;
        }

        public static T Call<T>(string key, params object[] args)
        {
            if (SendInternal(key))
            {
                return (T)Functions[key].DynamicInvoke(args);
            }
            return default;
        }

        public static Task CallAsync(string key, params object[] args)
        {
            if (SendInternal(key))
            {
                return (Task)Functions[key].DynamicInvoke(args);
            }
            return new Task(() => { });
        }

        /// <summary>
        /// Sendの共通処理をまとめたメソッド
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool SendInternal(string key)
        {
            if (Functions.ContainsKey(key))
            {
                var log = $"<color=#63ff00>[Send] {key}</color>";
                Logger.Debug(log);
                return true;
            }

            //存在しない場合
            Logger.Warning($"<color=#ffC500>[Failed][Send] {key}</color>");
            return false;
        }
        #endregion

        #region Obsolete
        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe(string key, Action function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe(string key, Delegate function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T>(string key, Action<T> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T1, T2>(string key, Action<T1, T2> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T1, T2, T3>(string key, Action<T1, T2, T3> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T>(string key, Func<T> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T1, T2>(string key, Func<T1, T2> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T1, T2, T3>(string key, Func<T1, T2, T3> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Register' instead of 'Subscribe'.", true)]
        public static void Subscribe<T1, T2, T3, T4>(string key, Func<T1, T2, T3, T4> function)
        {
            Register(key, function);
        }

        [Obsolete("Use 'Call' instead of 'Send'.", true)]
        public static bool Send(string key, params object[] args)
        {
            return Call(key, args);
        }

        [Obsolete("Use 'Call' instead of 'Send'.", true)]
        public static T Send<T>(string key, params object[] args)
        {
            return Call<T>(key, args);
        }

        [Obsolete("Use 'CallAsync' instead of 'SendAsync'.", true)]
        public static Task SendAsync(string key, params object[] args)
        {
            return CallAsync(key, args);
        }

        [Obsolete("Use 'Unregister' instead of 'Unsubscribe'.", true)]
        public static bool Unsubscribe(string key)
        {
            return Unregister(key);
        }
        #endregion

        public void Dispose()
        {
            foreach (var key in Functions.Keys)
            {
                Functions.Remove(key);
            }
            Functions.Clear();
        }
    }
}
