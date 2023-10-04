using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public BossHealth bossHealth;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        Flip();

    }



    private void Flip()
    {

        if (bossHealth.isDead()) return;
        StartCoroutine(WaitForFlip());


    }

    IEnumerator WaitForFlip()
    {
        Vector2 Pos = new Vector2(this.rb.position.x, this.rb.position.y);
        Vector2 Target = new Vector2(player.position.x, this.rb.position.y);
        if(Pos.x > Target.x)
        {
            yield return new WaitForSeconds(0.5f);
            this.transform.localScale = new Vector2(1, 1);
           
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            this.transform.localScale = new Vector2(-1, 1);
           
        }

        
    }


    


}
