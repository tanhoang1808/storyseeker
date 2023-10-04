using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 2000f;
    public float currentHealth;
    public float TriggerHealth ;
   
    private void Start()
    {
        currentHealth = maxHealth;

    }

    private void Update()
    {
        isDead();
    }

    public void TakeDame(int Damage)
    {
        currentHealth -= Damage;
       if(isDead()) BossCtrl.Instance.BossRedFlask.UpdateFlask(0,maxHealth);
        BossCtrl.Instance.BossRedFlask.UpdateFlask(currentHealth, maxHealth);

        TriggerHealth += Damage;
    }

    public bool isDead()
    {
        return this.currentHealth <= 0;
    }

   

}
