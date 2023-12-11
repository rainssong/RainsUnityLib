
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace com.rainssong.ui.prefs
{

    [RequireComponent(typeof(Toggle))]
    public class CheckboxPrefs : UIPrefs<Toggle,bool>
    {

        protected override void BindEvent()
        {
            ui.onValueChanged.AddListener(OnUIValueChanged);
        }



        override public void Load()
        {
            ui.isOn = PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        //public void Reset()
        //{
        //    PlayerPrefs.SetInt(key, defaultValue?1:0);
        //    Load();
        //}

    }
}