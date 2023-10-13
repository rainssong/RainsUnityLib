using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace com.rainssong.mono
{
    /// <summary>
    /// 允许免代码绑定事件
    /// </summary>
    public class EventMono : MonoBehaviour
    {

        public UnityEvent StartEvent;
        public UnityEvent EnableEvent;
        public UnityEvent AwakeEvent;
        public UnityEvent UpdateEvent;

        private void Awake()
        {
            AwakeEvent?.Invoke();
        }

        private void Start()
        {
            StartEvent?.Invoke();
        }

        private void OnEnable()
        {
            EnableEvent?.Invoke();
        }

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

    }
}