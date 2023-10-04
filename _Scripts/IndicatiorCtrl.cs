using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatiorCtrl : MonoBehaviour
{
    private  static IndicatiorCtrl instance;
    public static IndicatiorCtrl Indicatior { get => instance; }
    
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Exeed IndicationCtrl");
        }
        instance = this;
        gameObject.SetActive(false);
       
    }

    

   
}
