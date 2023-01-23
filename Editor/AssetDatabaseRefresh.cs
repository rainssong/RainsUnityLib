using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using UnityEditor;

namespace com.rainssong.editor
{
    public class AssetDatabaseRefresh : Editor
    { 
        [MenuItem("RainsUnityLib/AssetDatabase/Refresh")]
        static void Refresh()
        {
            UnityEditor.AssetDatabase.Refresh();
        }
    }

}