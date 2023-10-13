using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OpenPersistentData : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem ("RainsUnityLib/OpenPersistentDictionary #&_d")]
    private static void OpenPersistentDictionary()
    {
// #if UNITY_WIN
        System.Diagnostics.Process.Start (Application.persistentDataPath);
// #endif
    }
#endif
}