using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace com.rainssong.utils
{
    public class ObjectCore
    {
        static public object getValue (object obj, string expression)
        {
            object result = obj;
            string[] arr = expression.Split (new char[] { ',' });
            Type type = obj.GetType ();
            for (int i = 0; i < arr.Length; i++)
            {
                result = type.GetProperty (arr[i]).GetValue (obj, null);
                string Value = Convert.ToString (result);
                if (string.IsNullOrEmpty (Value)) return null;
            }

            return result;
        }

        static public void printDetials (object obj)
        {
            // Type type = obj.GetType();//获取类型
            // FieldInfo[] fields = type.GetFields ();
            // PropertyInfo[] properties = type.GetProperties();
            // // ProductInfo pro = new ProductInfo ();
            // // Type type = typeof (ProductInfo);
            // // FieldInfo[] fields = type.GetFields ();
            // foreach (FieldInfo f in fields)
            // {
            //     Debug.Log ("属性 " + f.Name + "  " + f + "=" + f.GetValue (obj)); // Debug.Log(f.Name);
            // }
            // foreach (PropertyInfo p in properties)
            // {
            //     Debug.Log ("属性 " + p.Name + "  " + p + "=" + p.GetValue (obj)); // Debug.Log(f.Name);
            // }
            //Debug.Log (ToJson (obj));

            var t = TypeDescriptor.GetProperties (obj);
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties (obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue (obj);
                Debug.Log (string.Format ("{0}={1}", name, value));
            }
            if (obj is IEnumerable<object>)
            {
                foreach (var item in (obj as IEnumerable<object>))
                {
                    Debug.Log (item);
                }
            }
        }

        public static T Clone<T>(T source) where T:ISerializable
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }
    
            // Don't serialize a null object, simply return the default for that object
            if (System.Object.ReferenceEquals(source, null))
            {
                return default(T);
            }
    
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }


    }

}