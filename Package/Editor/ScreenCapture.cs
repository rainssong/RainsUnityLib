using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScreenCaptureManager : MonoBehaviour
{
    [MenuItem("RainsUnityLib/Capture")]
    // Start is called before the first frame update
    static public void Capture()
    {
        var path = Application.dataPath + "/Doc/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss fff") + ".png";
        ScreenCapture.CaptureScreenshot(path);
        print(path);
    }


}
