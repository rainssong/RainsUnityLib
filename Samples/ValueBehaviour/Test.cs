using System.Collections;
using System.Collections.Generic;
using com.rainssong.mono.valueMono;
using UnityEngine;

public class Test : MonoBehaviour
{

    public IntMono intMono;
    public void LogInt(int old,int newI)
    {
        Debug.Log(old+" "+newI);
    }
    // Start is called before the first frame update
    void Start()
    {
        intMono.Value=33;
        intMono.Value=11+intMono+22;
        Debug.Log(intMono>22);
    }

}
