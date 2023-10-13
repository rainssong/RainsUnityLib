
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;


public class GetEnvironmentVars : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("RainsUnityLib/GetEnvironmentVariables")]
#endif
public static void GetVariables()
    {
        IDictionary environment = Environment.GetEnvironmentVariables();
        string allKeyName = "";
        foreach (string environmentKey in environment.Keys)
        {
            allKeyName = allKeyName + " | " + environmentKey;
        }
        Debug.Log("EnvironmentVariables:" + allKeyName);

        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        Debug.Log("BuildTarget£º" + buildTargetGroup);

        var symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        Debug.Log("ScriptingDefineSymbols£º" + symbols);

#if UNITY_ANDROID
        Debug.Log("UNITY_ANDROID");
#elif UNITY_IPHONE
        Debug.Log("UNITY_IPHONE");
#elif UNITY_STANDALONE_WIN
        Debug.Log("UNITY_STANDALONE_WIN");
#elif UNITY_WEBGL
        Debug.Log("UNITY_WEBGL");
#elif UNITY_EDITOR
        Debug.Log("UNITY_EDITOR");
#endif
    }
}