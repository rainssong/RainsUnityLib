using System;
   namespace com.rainssong.utils
   {
       public class Singleton<T> where T : new ()
       {
           public static T Instance
           {
               get
               {
                //    if(typeof(T).IsSubclassOf(typeof(Singleton<T>)))
                //    {
                //        return Singleton<T>.Instance;
                //    }
                   return Nested.instance;
               }
           }

           class Nested
           {
               static Nested () { }

               internal static readonly T instance = new T ();
           }
       }
   }