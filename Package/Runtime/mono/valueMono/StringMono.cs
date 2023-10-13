using System.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace com.rainssong.mono.valueMono
{
    public class StringMono : ValueMono<string>
    {
        [ContextMenu("Force Invoke")]
        protected void ForceInvoke()
        {
            onValueChanged?.Invoke("",Value);
        }
    }

}