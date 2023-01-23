using System;
using System.Collections;
using UnityEngine;

namespace com.rainssong.utils
{
    public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("SingletonMono<" + typeof(T).Name + "> already exists in scene, destroy.");
                Destroy(this);
                return;
            }
            else
            {
                _instance = this.GetComponent<T>();
                DontDestroyOnLoad(this);
            }
        }

        static public T Instance
        {
            get
            {
                if (_instance == null)
                {

                    if (typeof(T).IsSubclassOf(typeof(MonoBehaviour)))
                    {
                        _instance = (T)GameObject.FindObjectOfType(typeof(T));
                        if (_instance == null)
                        {
                            GameObject gameObject = new GameObject(typeof(T).ToString());
                            _instance = (T)gameObject.AddComponent(typeof(T));
                            _instance.name = "(Singleton) " + typeof(T).ToString();
                            DontDestroyOnLoad(_instance);
                        }
                    }
                }
                return _instance;
            }
        }
        static private T _instance;
    }

}