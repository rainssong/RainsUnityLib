using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.rainssong.ui.prefs
{
    [RequireComponent(typeof(Slider))]
    public class SliderPrefs : UIPrefs<Slider, float>
    {

        protected override void BindEvent()
        {
            ui.onValueChanged.AddListener(ChangeHandler);
        }


        private void ChangeHandler(float value)
        {
            PlayerPrefs.SetFloat(key, value);

        }

        override public void Load()
        {
            ui.value = PlayerPrefs.GetFloat(key, defaultValue);
        }

    }
}