using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.rainssong.mvvm
{
    [Serializable]
    /// <summary>
    /// 1.Binder.add预设绑定规则
    /// 2.添加model
    /// 3.使用泛型后，binder会无视类型封装拆箱，只以T为准。因此拆箱需要重写OnModelChange
    /// TODO：如果先添加model，再add则会失效。如何解决？1.add时考虑当前model。2.手动bandall
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ViewBase<T> : MonoBehaviour where T : new()
    {
        private bool _isInitialized;
        // public bool destroyOnHide;

        /// <summary>
        /// 使用binder自动绑定解绑属性,（否则需要在modelchange事件里手动解绑）
        /// 使用nameof解决名称问题
        /// 存在继承问题
        /// </summary>
        protected readonly PropertyBinder<T> binder = new();
        protected readonly PropertyBinder2 binder2 = new(typeof(T));

        [SerializeField]
        protected ValueListener<T> _modelListener = new();
        public ValueListener<T> modelListener => _modelListener;

        virtual public T model
        {
            get { return modelListener.Value; }
            set
            {
                if (!_isInitialized)
                {
                    OnInitialize();
                    _isInitialized = true;
                }
                //触发OnValueChanged事件
                modelListener.Value = value;
            }
        }



        /// <summary>
        /// 初始化View，当BindingContext改变时执行
        /// </summary>
        protected virtual void OnInitialize()
        {
            //无所ViewModel的Value怎样变化，只对OnValueChanged事件监听(绑定)一次
            modelListener.onValueChanged += OnModelChanged;
        }


        /// <summary>
        /// 当gameObject将被销毁时，这个方法被调用
        /// </summary>
        public virtual void OnDestroy()
        {
            model = default;
            modelListener.onValueChanged = null;
        }


        /// <summary>
        /// 绑定的上下文发生改变时的响应方法
        /// 利用反射+=/-=OnValuePropertyChanged
        /// </summary>
        public virtual void OnModelChanged(T oldValue, T newValue)
        {
            MVObserver.UnBind(oldValue, this);
            MVObserver.Bind(newValue, this);

            binder.Unbind(oldValue);
            binder.Bind(newValue);


            //binder2.Unbind(oldValue);
            //if (newValue != null)
            //    binder2.type = newValue.GetType();
            //binder2.Bind(newValue);
        }
    }
}
