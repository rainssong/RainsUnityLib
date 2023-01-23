using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// 包装响应式数据
/// </summary>
/// <typeparam name="T"></typeparam>
public class ValueListener<T>
{
    [SerializeField]
    protected T _value = default;

    public ValueListener()
        : this(default)
    {
    }

    public ValueListener(T initialValue)
    {
        _value = initialValue;
    }

    public T Value
    {
        get { return _value; }
        set
        {
            if (!Equals(_value, value))
            {
                T old = _value;
                _value = value;
                onValueChanged?.Invoke(old, value);
            }
        }
    }

    public Action<T, T> onValueChanged;

    //支持隐式转换
    public static implicit operator T(ValueListener<T> valueMono)
    {
        return valueMono.Value;
    }

    public override string ToString()
    {
        return (Value != null ? Value.ToString() : "null");
    }
}
