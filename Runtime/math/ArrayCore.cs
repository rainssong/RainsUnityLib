using System.Collections.Generic;
using System;

namespace com.rainssong.Math
{
    public class ArrayCore
    {

        public static T RandomChoose<T> (IList<T> array)
        {
            var r = new Random ();
            return array[r.Next (array.Count)];
        }

        public static void FullFill(IList<object> array,object content)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if(array[i] is IList<object>)
                    ArrayCore.FullFill(array[i] as IList<object>,content);
                else
                    array[i]=content;
            }
        }

        public static IList<T> RandomSort<T> (IList<T> array)
        {
            var r = new Random ();
            for (int i = 0; i < array.Count; i++)
            {
                var index = r.Next (array.Count);
                var temp = array[i];
                array[i] = array[index];
                array[index] = temp;
            }
            return array;

        }

    }
}