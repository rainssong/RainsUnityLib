using System.Collections;
using System.Collections.Generic;
using com.rainssong.mono.valueMono;
using UnityEngine;

/// <summary>
/// 将Int等属性封装为Mono，目的有两个，1实现响应式，2CodeLess，3运算符重载，无感替换
/// </summary>
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
