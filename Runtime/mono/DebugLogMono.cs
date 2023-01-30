using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace com.rainssong.utils
{
    /// <summary>
    /// TODO:加入Channel设定，由ScriptObject设定Channel和Color，
    /// </summary>
    public class DebugR<T>
    {
        public static void LogError(string value)
        {
            Debug.LogError(value);
        }

        public void  Log(string value)
        {
            Debug.Log(value);
        }

        public void Log(string value,string type)
        {
            Debug.Log(value);
        }

        public void  LogWarning(string value)
        {
            Debug.LogWarning(value);
        }
    }
}