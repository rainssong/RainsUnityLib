using com.rainssong.mvvm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorTest : MonoBehaviour
{
    [SerializeField]
    public TestModel model=new();

    
    [SerializeField]
    public ValueListener<string> aaa = new ValueListener<string>();

    public ValueListener<string> someListener;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
