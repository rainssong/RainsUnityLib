using UnityEngine;
using System.Collections;

public class RainMonoBehaviour:MonoBehaviour
{
    public bool autoBind;
    public bool Visible
    {
        //return true;
        get { return this.isActiveAndEnabled; }
        set { this.gameObject.SetActive(value); }
    }

    private void Awake() 
    {
        if (autoBind)
            this.BindFieldsWithChildren();
    }
    
}


