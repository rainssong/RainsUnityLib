using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;

namespace com.rainssong.mvvm
{

    /// <summary>
    /// 属性绑定器，add(属性，回调)，bind(target)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PropertyBinder2
    {
        private delegate void BindHandler(Object model);
        private delegate void UnBindHandler(Object model);

        public Type type=typeof(Object);

        //binder应为一个双keyDictionary，避免重复添加
        private readonly List<BindHandler> _binders=new List<BindHandler>();
        private readonly List<UnBindHandler> _unbinders=new List<UnBindHandler>();

        public PropertyBinder2(Type t)
        {
            this.type=t;
        }

        public void Add<T>(string name,Action<T, T> valueChangedHandler )
        {

            var fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public);
            if (fieldInfo == null)
            {
                throw new Exception(string.Format("Unable to find ValueListener field '{0}.{1}'", type.Name, name));
            }

            _binders.Add(model =>
            {
                var listener = GetValueListener<T>(name, model, fieldInfo);
                listener.onValueChanged -= valueChangedHandler;
                listener.onValueChanged += valueChangedHandler;
            });

            _unbinders.Add(model =>
            {
                GetValueListener<T>(name, model, fieldInfo).onValueChanged -= valueChangedHandler;
            });

        }

        private  ValueListener<TProperty> GetValueListener<TProperty>(string name, Object viewModel,FieldInfo fieldInfo)
        {
            var value = fieldInfo.GetValue(viewModel);
            ValueListener<TProperty> ValueListener = value as ValueListener<TProperty>;
            if (ValueListener == null)
            {
                throw new Exception(string.Format("Illegal ValueListener field '{0}.{1}' ", type.Name, name));
            }

            return ValueListener;
        }

        //protected void BindProperty<TProperty>(object model,string name, Action<TProperty, TProperty> valueChangedHandler)
        //{
        //    var type=model.GetType();
        //    var fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public);
        //    if (fieldInfo == null)
        //    {
        //        throw new Exception(string.Format("Unable to find ValueListener field '{0}.{1}'", type.Name, name));
        //    }

        //    var listener = GetValueListener<TProperty>(name, model, fieldInfo);
        //    listener.onValueChanged -= valueChangedHandler;
        //    listener.onValueChanged += valueChangedHandler;
        //}

        //protected void UnbindProperty<TProperty>(object model, string name, Action<TProperty, TProperty> valueChangedHandler)
        //{
        //    var type = model.GetType();
        //    var fieldInfo = type.GetField(name, BindingFlags.Instance | BindingFlags.Public);
        //    if (fieldInfo == null)
        //    {
        //        throw new Exception(string.Format("Unable to find ValueListener field '{0}.{1}'", type.Name, name));
        //    }

        //    GetValueListener<TProperty>(name, model, fieldInfo).onValueChanged -= valueChangedHandler;
        //}

        public void Bind(object model)
        {
            if (model != null)
            {
                for (int i = 0; i < _binders.Count; i++)
                {
                    _binders[i](model);
                }
            }
        }

        public void Unbind(Object model)
        {
            if (model!=null)
            {
                for (int i = 0; i < _unbinders.Count; i++)
                {
                    _unbinders[i](model);
                }
            }
        }

    }
}
