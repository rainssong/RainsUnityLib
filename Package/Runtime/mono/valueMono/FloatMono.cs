using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace com.rainssong.mono.valueMono
{
    public class FloatMono : ValueMono<float>
    {
        [ContextMenu("Force Invoke")]
        protected void ForceInvoke()
        {
            onValueChanged?.Invoke(0,Value);
        }

    }
}