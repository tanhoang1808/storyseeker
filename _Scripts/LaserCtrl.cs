using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCtrl : MonoBehaviour
{
    private static LaserCtrl instance;
    public static LaserCtrl Instance { get => instance; }
    public BossHealth bossHealth;
   
    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DisableLaser();
    }

    private void Update()
    {
        
    }

    public void DisableLaser()
    {
        foreach(Transform point in this.transform)
        {
            point.gameObject.SetActive(false);
        }
    }

    public void ActiveLaser()
    {
        foreach (Transform point in this.transform)
        {
            point.gameObject.SetActive(true);
        }
    }

   
}
