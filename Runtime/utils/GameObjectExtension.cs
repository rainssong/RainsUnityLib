using UnityEngine;
using System.Collections;

public static class GameObjectExtension
{
    public static T GetAddComponent<T>(this GameObject go)where T:Component
    {
        T c=go.GetComponent<T>();
        if(c==null)
        c= go.AddComponent<T>();
        return c;

    }


}
