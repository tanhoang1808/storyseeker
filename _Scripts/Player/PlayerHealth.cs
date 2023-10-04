using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float MaxHealth = 600f;
    public float CurrentHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }


    public void TakeDame(int Damage)
    {
        this.CurrentHealth -= Damage;
        PlayerCtrl.Instance.playerRedFlask.UpdateFlask(CurrentHealth, MaxHealth);

    }


    public bool isDead()
    {
        return CurrentHealth <= 0;
    }
}
