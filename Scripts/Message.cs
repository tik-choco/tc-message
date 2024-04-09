using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TC
{
    /// <summary>
    /// メッセージ管理
    /// </summary>
    public class Message : MonoBehaviour
    {
        private static readonly Dictionary<string, Delegate> Functions = new();

        private void OnDestroy()
        {
            Functions.Clear();
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe(string key, Action function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe(string key, Delegate function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T>(string key, Action<T> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T1, T2>(string key, Action<T1, T2> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T1, T2, T3>(string key, Action<T1, T2, T3> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T1, T2, T3, T4>(string key, Action<T1, T2, T3, T4> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T>(string key, Func<T> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T1, T2>(string key, Func<T1, T2> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T1, T2, T3>(string key, Func<T1, T2, T3> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// 受信先追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function">ここでは「メソッド」を入力してください</param>
        public static void Subscribe<T1, T2, T3, T4>(string key, Func<T1, T2, T3, T4> function)
        {
            AddFunction(key, function);
        }

        /// <summary>
        /// Addの共通処理をまとめたメソッド
        /// </summary>
        /// <param name="key"></param>
        /// <param name="function"></param>
        private static void AddFunction(string key, Delegate function)
        {
            Functions.TryAdd(key, function);
            Logger.Log($"<color=#00f5ff>[Subscribe] {key}</color>");
        }
        
        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="key">keyword</param>
        /// <param name="args">データ1,データ2,データ3,...</param>
        /// <returns>true:成功 false:失敗</returns>
        public static bool Send(string key, params object[] args)
        {
            if (SendInternal(key))
            {
                Functions[key].DynamicInvoke(args);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 返り値ありSend
        /// </summary>
        /// <typeparam name="T">返り値の型</typeparam>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T Send<T>(string key, params object[] args)
        {
            if (SendInternal(key))
            {
                return (T)Functions[key].DynamicInvoke(args);
            }
            return default;
        }

        /// <summary>
        /// 返り値がある場合は、Sendを使用してください
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static UniTask SendAsync(string key, params object[] args)
        {
            if (SendInternal(key))
            {
                return (UniTask)Functions[key].DynamicInvoke(args);
            }
            return new UniTask();
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
                Logger.Log(log);
                return true;
            }

            //存在しない場合
            Logger.LogWarning($"<color=#ffC500>[Failed][Send] {key}</color>");
            return false;
        }

        /// <summary>
        /// 受信先削除
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true:成功 false:失敗</returns>
        public static bool Unsubscribe(string key)
        {
            if (Functions.ContainsKey(key))
            {
                Logger.Log($"<color=#ffbaf4>[Unsubscribe] {key}</color>");
                Functions.Remove(key);
                return true;
            }

            Logger.LogWarning($"<color=#ffc500>[Failed][Unsubscribe] {key}</color>");
            return false;
        }
    }
}
