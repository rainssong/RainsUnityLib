using System;
using System.Collections;
using UnityEngine;

namespace com.rainssong.ui.prefs
{

    /// <summary>
    /// 绑定了PlayerPrefs和Value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrefsBoolMono : MonoPrefs<bool>
    {
        override public void SetValue(bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        override public bool GetValue()
        {
            return PlayerPrefs.GetInt(key) == 1;
        }

    }
}