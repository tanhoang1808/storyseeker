using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameSender : MonoBehaviour
{
    public PlayerHealth player;

    [SerializeField] private int laserDame = 20;
  

   
    private void Send(PlayerHealth playerHealth)
    {
        
        playerHealth.TakeDame(laserDame);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            player = collision.gameObject.GetComponentInParent<PlayerHealth>();
            Send(player);
        }
    }

}
