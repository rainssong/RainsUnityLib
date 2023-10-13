using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class RainMonoBehaviour:MonoBehaviour
{
    public bool autoBind;
    public bool Visible
    {
        //return true;
        get { return this.isActiveAndEnabled; }
        set { this.gameObject.SetActive(value); }
    }

    virtual protected void Awake() 
    {
        if (autoBind)
            this.BindFieldsWithChildren();
    }
    
}


