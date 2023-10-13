using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using com.rainssong.mvvm;
using UnityEngine;
using UnityEngine.UI;

namespace com.rainssong.mvvm.sample
{
    public class TestView : ViewBase<TestModel>
    {
        public Button btn1;
        public Button btn2;
        public Text atkText;
        public Text nameText;

        public ValueListener<string> someListener;

        //public string playerName
        //{
        //    get => someListener.Value;
        //    set => someListener.Value = value;
        //}

        public string playerName { get => someListener.Value; set=> someListener.Value = value; }

        // Start is called before the first frame update
        void Start()
        {
            //someListener=new ValueListener<string>("SB");
            someListener.onValueChanged+= onNameChanged;
            //binder2.type= typeof(TestModel);
            binder.Add<int>(nameof(model.atk), onAtkChanged);

            //这里就会自动bind
            model = new TestModel();

            // binder.Bind(model);
        }

        public void ChangeAtk(int value)
        {
            
            model.atk.Value = value;
        }

        public void ChangeName(string value)
        {

            playerName = "cao";
        }

        private void onAtkChanged(int arg1, int arg2)
        {
            Debug.Log("ChangeAtk:" + arg2);
            
            atkText.text = model.atk.Value.ToString();
        }

        private void onNameChanged(string arg1, string arg2)
        {
            Debug.Log("ChangeName:" + arg2);
            nameText.text = model.name.Value;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
