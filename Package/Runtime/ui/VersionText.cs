using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VersionText : MonoBehaviour
{
    //public void InitText(Action<string> content)
    //{
    //    Text label = GetComponent<Text>();
    //    label.text = content.Invoke();
    //}
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Text>().text = Application.version;
    }
}
