using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace com.rainssong.utils
{
    public class DebugLogMono:SingletonMono<DebugLogMono>
    {
        public void LogError(string value)
        {
            Debug.LogError(value);
        }

        public void  Log(string value)
        {
            Debug.Log(value);
        }
        
        public void  LogWarning(string value)
        {
            Debug.LogWarning(value);
        }
    }
}