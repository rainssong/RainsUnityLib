using System;
using System.Collections;
using UnityEngine;

namespace com.rainssong.ui.prefs
{
    /// <summary>
    /// 绑定了PlayerPrefs和Value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoPrefs<T> : MonoBehaviour
    {
        [SerializeField]
        public string key;
        [SerializeField]
        protected T def;


        public T Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        virtual public void SetValue(T value)
        {
            throw new NotImplementedException();
        }

        virtual public T GetValue()
        {
            return def;
        }

    }
}