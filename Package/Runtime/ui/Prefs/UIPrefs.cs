using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


namespace com.rainssong.ui.prefs
{
    /// <summary>
    /// 将UI与Prefs绑定，必须继承并实现Load和bind
    /// </summary>
    /// <typeparam name="UIT"></typeparam>
    /// <typeparam name="VALUET"></typeparam>
    public class UIPrefs<UIT, VALUET> : MonoBehaviour where UIT : UIBehaviour
    {
        protected UIT ui;

        [SerializeField]
        protected VALUET defaultValue;

        [SerializeField]
        protected string key;



        virtual protected void Awake()
        {
            ui = GetComponent<UIT>();
            Load();
            BindEvent();
        }


        protected void OnUIValueChanged(VALUET value)
        {

            //PlayerPrefs.SetInt(key, value ? 1 : 0);


            //switch (typeof(VALUET))
            //{
            //    case bool:
            //        PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
            //        break;
            //    case typeof(int):
            //        PlayerPrefs.SetInt(key, (int)value);
            //        break;
            //}


            switch (value)
            {
                case int intValue:
                    PlayerPrefs.SetInt(key, intValue);
                    break;
                case bool boolValue:
                    PlayerPrefs.SetInt(key, (bool)boolValue ? 1 : 0);
                    break;
                case string stringValue:
                    PlayerPrefs.SetString(key, stringValue);
                    break;
                case float floatValue:
                    PlayerPrefs.SetFloat(key, floatValue);
                    break;
                default:
                    Console.WriteLine("The value is of an unknown type.");
                    break;
            }

        }


        protected void OnEnable()
        {
            Load();
        }

        virtual protected void BindEvent()
        {
            //throw new NotImplementedException();
        }

        virtual public void Load()
        {
            throw new NotImplementedException();
        }

        virtual public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
