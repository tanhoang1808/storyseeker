using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    private static BossCtrl instance;
    public static BossCtrl Instance { get => instance; }
    public Rigidbody2D rb;
    public BossHealth bossHealth;
    public BossRedFlask BossRedFlask;
    private void Awake()
    {
     if(instance !=null)
        {
            Debug.LogError("Exceed BossCtrl");
        }
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>();
        BossRedFlask = GameObject.FindGameObjectWithTag("BossBar").GetComponentInChildren<BossRedFlask>();
    }
}
