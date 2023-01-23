using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class MonoBehaviourExtension
{

    //public bool Visible
    //{
    //    //return true;
    //    get{ return this.isActiveAndEnabled; }
    //    set{ this.gameObject.SetActive(value); }
    //}

    /// <summary>
    /// 自动绑定，todo：绑定按名称绑定gameobject/transform,递归查找component，包含非激活
    /// </summary>
    /// <param name="mb"></param>
    public static void BindFieldsWithChildren(this MonoBehaviour mb)
    {
        Type type = mb.GetType();
        var fields = type.GetFields();

        foreach (var field in fields)
        {
            var fieldType = field.FieldType;
            var fieldName = field.Name;
            var isMono = typeof(MonoBehaviour).IsAssignableFrom(fieldType);
            var isGO = typeof(GameObject).IsAssignableFrom(fieldType);

            if (!isMono && !isGO)
                continue;

            var v = field.GetValue(mb);
            if (Convert.ToString(v) == "null" || v == null)
            {
                var child = mb.transform.FindChildInTransform(fieldName);
                if (child != null)
                {
                    if (isGO)
                        field.SetValue(mb, child.gameObject);
                    else
                    {
                        var comp = child.GetComponent(fieldType);
                        if (typeof(MonoBehaviour).IsAssignableFrom(comp.GetType()))
                            field.SetValue(mb, comp);
                    }
                }
            }
        }
    }

    public static void Hide(this MonoBehaviour mb)
    {
        mb.gameObject.SetActive(false);
    }

    public static void Show(this MonoBehaviour mb)
    {
        mb.gameObject.SetActive(true);
    }

    public static void Hide(this GameObject go)
    {
        go.SetActive(false);
    }

    public static void Show(this GameObject go)
    {
        go.SetActive(true);
    }

    public static void SetActivateWithChildren(this GameObject go, bool state)
    {
        go.SetActive(state);
        foreach (Transform child in go.transform)
        {
            SetActivateWithChildren(child.gameObject, state);
        }
    }
 

    public static T GetAddComponent<T>(this MonoBehaviour mb) where T : Component
    {
        T c;
        mb.gameObject.TryGetComponent<T>(out c);
        if (c==null)
            return mb.gameObject.AddComponent<T>();
        return mb.gameObject.GetComponent<T>();

    }

    public static void RemoveComponent<T>(this MonoBehaviour mb) where T : Component
    {
        var c = mb.gameObject.GetComponent<T>();
        if (c == null) return;
        GameObject.Destroy(c);
    }

    public static Coroutine Invoke(this MonoBehaviour monoBehaviour, Action action, float time)
    {
        return monoBehaviour.StartCoroutine(InvokeImplementation(action, time));
    }

    private static IEnumerator InvokeImplementation(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    // public static Component GetAddComponent<T>(this MonoBehaviour mb) where T:Component
    // {
    //     Component c=mb.gameObject.GetComponent<T>();
    //     if(c=null)
    //     c= mb.gameObject.AddComponent<T>();
    //     return c;
    // }

}