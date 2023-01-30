using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace com.rainssong.mono
{
    /// <summary>
    /// 允许免代码绑定事件
    /// </summary>
    public class MonoBehaviourAsistant : MonoBehaviour
    {
        public bool HideOnStart = false;

        public UnityEvent StartEvent;
        public UnityEvent EnableEvent;
        public UnityEvent AwakeEvent;

        private void Awake()
        {

            // if (HideOnAwake && Time.time < 0.1) this.Hide();
            AwakeEvent?.Invoke();
        }

        private void Start()
        {
            if (HideOnStart)
            {
                StartEvent.AddListener(() => this.gameObject.SetActive(false));
            }
            StartEvent?.Invoke();
        }

        private void OnEnable()
        {
            EnableEvent?.Invoke();
        }

    }
}