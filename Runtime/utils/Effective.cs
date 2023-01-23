using System;
using UnityEngine;

namespace com.rainssong.utils
{
    class Effective
    {
        public static void FunctionTiming (Action f, int repeat = 1) {
            int t;
            for (var i = 0; i < repeat; i++) {
                t = System.Environment.TickCount;
                f.DynamicInvoke ();
                Debug.Log (System.Environment.TickCount - t);
            }
        }
    }
}
