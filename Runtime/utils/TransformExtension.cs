using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    // Start is called before the first frame update
    static public Transform FindChildInTransform(this Transform parent, string child)
    {
        Transform childTF = parent.Find(child);
        if (childTF != null)
        {
            return childTF;
        }
        for (int i = 0; i < parent.childCount; i++)
        {
            childTF = FindChildInTransform(parent.GetChild(i), child);
            if (childTF != null)
                return childTF;
        }
        return null;
    }

    public static void SetX(this Transform transform, float x)
    {
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
    }
    public static void SetY(this Transform transform, float y)
    {
        Vector3 position = transform.position;
        position.y = y;
        transform.position = position;
    }

    public static void SetZ(this Transform transform, float z)
    {
        Vector3 position = transform.position;
        position.z = z;
        transform.position = position;
    }

}