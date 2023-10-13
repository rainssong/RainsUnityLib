using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.rainssong.mvvm
{
    public static class MVObserver
    {
        private static Dictionary<object, List<MonoBehaviour>> mvDic = new();
        public static Dictionary<MonoBehaviour, object> vmDic = new();


        public static MonoBehaviour GetView(object model)
        {
            return GetViews(model)?[0];
        }

        public static List<MonoBehaviour> GetViews(object model)
        {
            if (!mvDic.ContainsKey(model))
                return null;
            if (mvDic[model] == null)
                return null;
            if (mvDic[model].Count == 0)
                return null;

            return mvDic[model];
        }

        public static bool SetView(object model, MonoBehaviour view)
        {
            if (!mvDic.ContainsKey(model))
                mvDic[model] = new List<MonoBehaviour>();

            foreach (var item in mvDic[model])
            {
                if (item == view)
                    return false;
            }

            mvDic[model].Add(view);
            return true;
        }


        public static void Bind(object model, MonoBehaviour view)
        {
            if (model == null || view == null)
            {
                //Debug.LogError("bind null");
                return;
            }
                

            vmDic[view] = model;
            SetView(model, view);
        }

        public static void UnBind(object model, MonoBehaviour view)
        {
            if(model==null || view==null)
                return;

            if(vmDic.ContainsKey(view))
                if (vmDic[view] == model)
                    mvDic.Remove(view);

            if (!mvDic.ContainsKey(model))
                return;

            for (int i = mvDic[model].Count - 1; i >= 0; i--)
            {
                var item = mvDic[model][i];
                if (view == item)
                    mvDic[model].Remove(item);
            }

            if (mvDic[model].Count == 0)
                mvDic.Remove(model);
        }
    }
}
