using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private static PlayerCtrl instance;
    public static PlayerCtrl Instance { get => instance;}
    public PlayerHealth playerHealth;
    public Rigidbody2D rb;
    public PlayerMove playerMove;
    public Animator animator;
    public PlayerStatus playerStatus;
    public PlayerRedFlask playerRedFlask;
    private void Awake()
    {
        if(instance !=null)
        {
            Debug.LogError("Exeed PlayerCtrl");
        }
        instance = this;

        
        
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        playerStatus = GetComponent<PlayerStatus>();
    }
}
