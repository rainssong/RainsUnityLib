using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.rainssong.mono.valueMono
{
    /// <summary>
    /// Value with changeListener, you must addComponent and set reference
    ///1实现响应式，2CodeLess，3运算符重载，无感替换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// 
    public class ValueMono<T> : MonoBehaviour
    {
        [SerializeField]
        protected T value;

        public T Value
        {
            get { return value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    T old=this.value;
                    this.value = value;
                    onValueChanged?.Invoke(old,value);
                }
            }
        }

        public UnityEvent<T,T> onValueChanged;

        //支持隐式转换
        public static implicit operator T(ValueMono<T> valueMono)
        {
            return valueMono.Value;
        }

    }
}