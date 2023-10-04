using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private int BossDame = 20;


    public PlayerHealth playerHealth;
    public Transform player;
    private Vector2 boxSize = new Vector2(1, 1);
    Rigidbody2D rb;
    public LayerMask Player;

    public float distanceToPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
    }
    public void Attack()
    {

        if(distanceToPlayer <= 4f)
        {
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, boxSize, 0f, Vector2.right * this.transform.localScale.x, Player);
            if(hit)
            {
                this.playerHealth.TakeDame(BossDame);
                Animator animator = PlayerCtrl.Instance.GetComponent<Animator>();
                animator.SetTrigger("Hurt");
            }
        }


    }

}

  

