using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// TODO������һ������go��Ȼ��ص�
/// </summary>
/// <typeparam name="T"></typeparam>
public class PrefsInt : ValueListener<int>
{
    public readonly string key;
    private readonly int def;

    public PrefsInt(string key, int def)
    {
        this.key = key;
        this.def = def;
    }

    override public int Value
    {
        get => PlayerPrefs.GetInt(key, def);
        set
        {
            if (Value != value)
            {
                onValueChanged?.Invoke(Value, value);
                PlayerPrefs.SetInt(key, value);
            }
        }
    }
}
