using System;
using UnityEngine;
using System.Collections;

namespace com.rainssong.mvvm
{
    [System.Serializable]
    public class TestModel
    {
         [SerializeField]
        public ValueListener<string> name = new ValueListener<string>();
         [SerializeField]
        public ValueListener<int> atk = new ValueListener<int>();

    }
}