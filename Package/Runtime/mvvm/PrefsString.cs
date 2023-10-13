using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


/// <summary>
/// ��PlayerPrefs
/// TODO��
/// * ����һ������go��Ȼ��ص�
/// * ����
/// </summary>
/// <typeparam name="T"></typeparam>
public class PrefsString : ValueListener<string>
{
    public readonly string key;
    private string def;

    public PrefsString(string key,string def="")
    {
        this.key = key;
        this.def = def;
    }

    override public string Value
    {
        get { 
            return PlayerPrefs.GetString(key, def);
        }
        set
        {
            if(Value != value)
            {
                onValueChanged?.Invoke(Value, value);
                PlayerPrefs.SetString(key, value);
            }
            
        }
    }

    public override string ToString()
    {
        return Value;
    }
}
