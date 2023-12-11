
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.rainssong.ui.prefs
{
    [RequireComponent(typeof(InputField))]
    public class InputPrefs : UIPrefs<InputField,string>
    {


        protected override void BindEvent()
        {
            ui.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        //void OnEnable()
        //{
        //    Load();
        //}

        private void OnInputFieldValueChanged(string value)
        {
            switch (ui.contentType)
            {
                case InputField.ContentType.IntegerNumber:
                    PlayerPrefs.SetInt(key, int.Parse(value));
                    break;
                case InputField.ContentType.DecimalNumber:
                    PlayerPrefs.SetFloat(key, float.Parse(value));
                    break;
                default:
                    PlayerPrefs.SetString(key, value);
                    break;
            }
        }


        override public void Load()
        {
            ui.text = ui.contentType switch
            {
                InputField.ContentType.IntegerNumber => PlayerPrefs.GetInt(key, int.Parse(defaultValue)).ToString(),
                InputField.ContentType.DecimalNumber => PlayerPrefs.GetFloat(key, float.Parse(defaultValue)).ToString(),
                _ => PlayerPrefs.GetString(key, defaultValue),
            };
        }

        override public void Reset()
        {
            ui.text = defaultValue;
            //field.ForceLabelUpdate();
        }
    }
}