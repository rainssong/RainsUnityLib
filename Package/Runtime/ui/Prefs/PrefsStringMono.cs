using System;
using System.Collections;
using UnityEngine;

namespace com.rainssong.ui.prefs
{
    /// <summary>
    /// 绑定了PlayerPrefs和Value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrefsStringMono : MonoPrefs<string>
    {
        override public void SetValue(string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        override public string GetValue()
        {
            return PlayerPrefs.GetString(key);
        }

    }
}