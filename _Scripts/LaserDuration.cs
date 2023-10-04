using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDuration : MonoBehaviour
{
    public float laserCounter;
    [SerializeField] public float laserDelay = 2f;

    private void Update()
    {
       
    }


    public void Duration()
    {
        
            laserCounter += Time.deltaTime;
            LaserCtrl.Instance.ActiveLaser();
            if (laserCounter > laserDelay)
            {
                LaserCtrl.Instance.DisableLaser();
            }
        
    }




}
