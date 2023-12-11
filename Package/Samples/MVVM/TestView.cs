using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using com.rainssong.mvvm;
using UnityEngine;
using UnityEngine.UI;

namespace com.rainssong.mvvm.sample
    
{
    /// <summary>
    /// 这个案例展示如何将Model与View绑定，主要是使用Binder将Model中的ValueLis和View中的方法进行捆绑。
    /// </summary>
    public class TestView : ViewBase<TestModel>
    {
        public Button btn1;
        public Button btn2;
        public Text atkText;
        public Text nameText;

        //封装Model的属性，目的是为了封装Model减少书写
        public string playerName { get => model.name.Value; set=> model.name.Value = value; }

        // Start is called before the first frame update
        void Start()
        {

            //自动将Model的Atk绑定事件，绑定后只需修改Model值
            binder.Add<int>(nameof(model.atk), onAtkChanged);
            binder.Add<string>(nameof(model.name), onNameChanged);

            //赋值后就会自动bind
            model = new TestModel();

            //不赋值需要手动Bind
            //binder.Bind(model);
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
