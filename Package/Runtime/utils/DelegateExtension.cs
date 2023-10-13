using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class DelegateExtension
{

    public static void TryInvoke (this Action ac)
    {
        if (ac != null)
            ac.Invoke ();
    }


    // public static void TryInvoke (this Delegate<T> ac)
    // {
    //     if (ac != null)
    //         return ac.Invoke ();
    //     return null;
    // }

}