using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransTest : MonoBehaviour
{
    public ClassA a;
    public ClassB b;
    // Start is called before the first frame update
    void Start()
    {
        a = new ClassB();

        print("a.name:"+a.name);
        print("((ClassB)a).name:"+((ClassB)a).name);
        print("(a as ClassB).name:" + (a as ClassB).name);

        print("a.printName:" + a.printName());
        print("((ClassB)a).printName:" + ((ClassB)a).printName());
        print("(a as ClassB).name:" + (a as ClassB).printName());
        //print(b.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class ClassA
{
    public string name = "aaa";
    public string sex = "woman";
    public string printName()
    {
        return name;
    }

    virtual public string printSex()
    {
        return "woman";
    }
}


public  class ClassB :ClassA
{
    new public string name = "bbb";
    new public string printName()
    {
        return name;
    }

    new public string printSex()
    {
        return "man";
    }
}