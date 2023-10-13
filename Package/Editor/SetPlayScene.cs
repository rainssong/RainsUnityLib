using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class SetPlaySceneWindow
{
    static SetPlaySceneWindow()
    {
        LoadStartScene();
    }


    //[InitializeOnLoadMethod]
    private static void InitializeOnEditorLoad()
    {
        LoadStartScene();
    }

    static void LoadStartScene()
    {
        Debug.Log($"SetStartScene {editorMasterScene}");
        EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(editorMasterScene);
    }


    [MenuItem("RainsUnityLib/SetStartScene")]
    static public void SetStartScene()
    {
        string masterScene = EditorUtility.OpenFilePanel("Select Master Scene", Application.dataPath, "unity");
        editorMasterScene = masterScene.Replace(Application.dataPath, "Assets/"); //project relative instead of absolute path 
        SceneAsset myWantedStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(editorMasterScene);
        EditorSceneManager.playModeStartScene = myWantedStartScene;
        Debug.Log($"SetStartScene {myWantedStartScene}");
        //EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"), EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);
    }


    private static string editorMasterScene
    {
        get { return EditorPrefs.GetString("MasterScene", null); }
        set { EditorPrefs.SetString("MasterScene", value); }
    }


}