using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseEditor : MonoBehaviour
{
    public static bool NEED_PAUSE = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        while (NEED_PAUSE)
        {
            EditorApplication.isPlaying = false;
        }
    }
}
