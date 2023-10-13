using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace com.rainssong.mono.valueMono
{
    public class IntMono : ValueMono<int>
    {
        [ContextMenu("Force Invoke")]
        protected void ForceInvoke()
        {
            onValueChanged?.Invoke(0,Value);
        }

    }
}