using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace com.rainssong.utils
{
    /// <summary>
    /// 加强版Log，可以在编辑器中调用
    /// TODO:加入Channel设定，由ScriptObject设定Channel和Color，
    /// </summary>
    public class RainDebug : SingletonMono<RainDebug>
    {
        public bool showWhite = true;
        public bool showOrange = true;
        public bool showLightGreen = true;
        public bool showSkyBlue = true;
        public static void LogError(string value)
        {
            Debug.LogError(value);
        }

        public static void Log(string value)
        {
            Debug.Log(value);
        }

        public static void Log(string value, int color)
        {
            switch (color)
            {
                case ColorUtil.SKY_BLUE:
                    if (Instance.showSkyBlue)
                        Debug.Log(string.Format("<color={1}>{0}</color>", value, color.ToString().Replace("0x", "#")));
                    break;
                case ColorUtil.ORANGE:
                    if (Instance.showOrange)
                        Debug.Log(string.Format("<color={1}>{0}</color>", value, color.ToString().Replace("0x", "#")));
                    break;
                case ColorUtil.LIGHT_GREEN:
                    if (Instance.showLightGreen)
                        Debug.Log(string.Format("<color={1}>{0}</color>", value, color.ToString().Replace("0x","#")));
                    break;
                default:
                    Debug.Log(string.Format("<color={1}>{0}</color>", value, color.ToString().Replace("0x", "#")));
                    break;
            }

        }

        public void LogWarning(string value)
        {
            Debug.LogWarning(value);
        }
    }
}