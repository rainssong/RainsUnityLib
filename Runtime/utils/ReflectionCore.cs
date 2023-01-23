using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

namespace com.rainssong.utils
{
    public class ReflectionCore
    {


        ///获取属性(封装)
        static public object GetProperty (object target, string propertyName)
        {
            Type type = target.GetType (); //获取类型
            PropertyInfo propertyInfo = type.GetProperty (propertyName);
            object result = propertyInfo.GetValue (target, null);
            return result;
        }

        //获取开放字段
        static public object GetField (object target, string fieldName)
        {
            Type type = target.GetType ();
            var fieldInfo = type.GetField (fieldName);
            if (fieldInfo == null) return null;

            return fieldInfo.GetValue (target);
        }

        static public object GetFieldOrProperty(object target, string propertyName)
        {
            return GetField(target,propertyName)??GetProperty(target,propertyName);
        }

        static public void SetProperty (object target, string propertyName, object value)
        {
            Type type = target.GetType (); //获取类型
            PropertyInfo propertyInfo = type.GetProperty (propertyName);
            propertyInfo.SetValue (target, value, null);
        }

        static public void SetField (object target, string propertyName, object value)
        {
            target.GetType ().GetField (propertyName).SetValue (target, value);
        }

        static public void SetFieldsAndProperties (object target, object param)
        {
            Type t = param.GetType ();
            var proArr = t.GetProperties ();
            var fieldArr = t.GetFields ();

            foreach (var pn in proArr)
            {

                if (ReflectionCore.HasProperty (target, pn.Name))
                {
                    var r = ReflectionCore.GetProperty (param, pn.Name);
                    ReflectionCore.SetProperty (target, pn.Name, r);
                }
            }

            foreach (var fn in fieldArr)
            {
                if (ReflectionCore.HasField (target, fn.Name))
                {
                    var r = ReflectionCore.GetField (param, fn.Name);
                    ReflectionCore.SetField (target, fn.Name, r);
                }
            }
        }

        static public bool HasProperty (object target, string propertyName)
        {
            return target.GetType ().GetProperty (propertyName) != null;
        }

        static public bool HasField (object target, string propertyName)
        {
            return target.GetType ().GetField (propertyName) != null;
        }

        static public void InvokeFunction (object target, string funName, string pams)
        {
            Type t = target.GetType ();
            MethodInfo mi = t.GetMethod (funName);
            var jsonString = "[" + pams + "]";
            var jsonObjs = JsonConvert.DeserializeObject<object[]> (jsonString);
            var ps = mi.GetParameters ();

            for (int i = 0; i < ps.Length; i++)
                jsonObjs[i] = Convert.ChangeType (jsonObjs[i], ps[i].ParameterType);

            mi.Invoke (target, jsonObjs);
        }

        static public void InvokeFunction (object target, string statement)
        {
            string funName = statement.Split ('(') [0];
            string pams = statement.Split ('(') [1];
            pams = pams.Split (')') [0];
            InvokeFunction (target, funName, pams);
        }
    }
}