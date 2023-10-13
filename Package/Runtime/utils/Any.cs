using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[SerializeField]
public class Any : ScriptableObject
{
    public List<Any> list;
    public int i;
    public string s;
    public float f;
    public bool b;
    public Type type;

    public void SetData(string v)
    {
        this.s = v;
        type = s.GetType();
    }

    public void SetData(int v)
    {
        this.i =v;
        type =v.GetType();
    }

    public void SetBool(bool v)
    {
        this.b= v;
        type = v.GetType();
    }

    public bool IsType<T>()
    {
        if (typeof(T)== type)
            return true;
        else
            return false;
    }

}