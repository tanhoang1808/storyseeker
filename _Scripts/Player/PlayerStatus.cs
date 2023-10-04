using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public bool isDead = false;
   

    private void Update()
    {
        if (PlayerCtrl.Instance.playerHealth.isDead() && isDead == false)
        {
            Dead();
            isDead = true;
        }
    }

    public void Dead()
    {
        PlayerCtrl.Instance.animator.SetTrigger("Dead");
       
    }



}
