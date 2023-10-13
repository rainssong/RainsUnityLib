using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefsFloat : ValueListener<float>
{
    public readonly string key;
    private readonly float def;

    public PrefsFloat(string key,float def)
    {
        this.key = key;
        this.def = def;
        Load();
    }

    public void Load()
    {
        this.Value = PlayerPrefs.GetFloat(key,def);
    }

    override public float Value
    {
        get=>PlayerPrefs.GetFloat(key, def);
        set
        {
            if (Value != value)
            {
                onValueChanged?.Invoke(Value, value);
                PlayerPrefs.SetFloat(key, value);
            }

        }
    }
}
