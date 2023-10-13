using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;

namespace com.rainssong.mvvm
{

    ///FIXME:查重

    /// <summary>
    /// 属性绑定器，获取ViewModel中的ValueListener，然后绑定事件
    /// ，add(属性，回调)，bind(target)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PropertyBinder<T>
    {
        private delegate void BindHandler(T model);
        private delegate void UnBindHandler(T model);

        //binder应为一个双keyDictionary，避免重复添加
        private readonly List<BindHandler> _binders = new List<BindHandler>();
        private readonly List<UnBindHandler> _unbinders = new List<UnBindHandler>();

        public bool strict = false;

        // private  readonly Dictionary<string,Action<object,object>> _bindersDic=new Dictionary<string, Action<object,object>>();

        public void Add<TProperty>(string name, Action<TProperty, TProperty> valueChangedHandler)
        {
            // _bindersDic.Add(name, (Action<object,object>)valueChangedHandler);

            _binders.Add(model =>
            {
                var fieldInfo = model.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public);
                if (fieldInfo == null)
                {
                    if (strict)
                        throw new Exception(string.Format("Unable to find ValueListener field '{0}.{1}'", typeof(T).Name, name));
                    else
                        return;
                }
                var listener = GetValueListener<TProperty>(name, model, fieldInfo);
                listener.onValueChanged -= valueChangedHandler;
                listener.onValueChanged += valueChangedHandler;
            });

            _unbinders.Add(model =>
            {
                var fieldInfo = model.GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public);
                if (fieldInfo == null)
                {
                    if (strict)
                        throw new Exception(string.Format("Unable to find ValueListener field '{0}.{1}'", typeof(T).Name, name));
                    else
                        return;
                }
                GetValueListener<TProperty>(name, model, fieldInfo).onValueChanged -= valueChangedHandler;
            });

        }

        private ValueListener<TProperty> GetValueListener<TProperty>(string name, T viewModel, FieldInfo fieldInfo)
        {
            var value = fieldInfo.GetValue(viewModel);
            ValueListener<TProperty> ValueListener = value as ValueListener<TProperty>;
            if (ValueListener == null)
            {
                throw new Exception(string.Format("Illegal ValueListener field '{0}.{1}' ", typeof(T).Name, name));
            }

            return ValueListener;
        }

        public void Bind(T model)
        {
            if (model != null)
            {
                for (int i = 0; i < _binders.Count; i++)
                {
                    _binders[i](model);
                }
            }
        }

        public void Unbind(T model)
        {
            if (model != null)
            {
                for (int i = 0; i < _unbinders.Count; i++)
                {
                    _unbinders[i](model);
                }
            }
        }

    }
}
