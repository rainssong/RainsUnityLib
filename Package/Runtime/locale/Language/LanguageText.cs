using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Language
{
    [RequireComponent(typeof(Text))]
    [AddComponentMenu("Language/LanguageText")]
    [ExecuteAlways]
    public class LanguageText : MonoBehaviour
    {
        [HideInInspector]
        public string Language;
        [HideInInspector]
        public string File;
        public string Key
        {
            get { return _key; }
            set { 
                if(_key==value)return;
                _key = value;Refresh(); }
        }
        //必须暴露否则无法在编辑器中修改
        [SerializeField]
        private string _key;
        [HideInInspector]
        public string Value;
        public string[] lanParams = new string[0];
        //public BindingList<String> lanParams = new BindingList<string>();
        public LanguageService Localization;

        //所有对于Txt的修改都视作Key
        //public bool forceEmbed = false;

        public Text label => GetComponent<Text>();

        void Awake()
        {
            LanguageService.Instance.changeLanCB += Refresh;

        }

        private void OnValidate()
        {
            if (_key == "")
                _key = GetComponent<Text>().text;
        }


        [ContextMenu("下拉菜单")]
        public void OpenJson()
        {
            Debug.Log("aaaa");
        }

        private void OnEnable()
        {
            Refresh();
        }


        public void Refresh()
        {
            //if(forceEmbed)
            //{
            //    var targetTxt =  LanguageService.Instance.GetFromFile(File, Key, label.text);
            //    if (label.text!=targetTxt)
            //    {
            //        _key = label.text;
            //    }
            //}

            if (_key == null) return;

            label.text = LanguageService.Instance.GetFromFile(File, Key, label.text);
            if (lanParams.Length>0)
            {
                label.text = string.Format(label.text, lanParams);
            }
        }

       public void SetParams(string[] paramArr)
        {
            lanParams = paramArr;
            Refresh();
        }

        public string text
        {
            get { return label.text; }
            set { Key= value;}
        }
	}
}

