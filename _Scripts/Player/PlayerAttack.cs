using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerAttack : MonoBehaviour
{


    Animator anim;
    public bool slash;
    public float DistanceToBoss;
    public Transform Boss;
    private Vector2 boxSize = new Vector2(1, 1);
    public LayerMask boss;
    public BossHealth bossHealth;
    public int player_dame;
    public TextValue text;
    private void Awake()

    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Slash();
        DistanceToBoss = Vector2.Distance(transform.position, Boss.position);
        player_dame = Random.Range(40, 80);
    }

    ////private void Slash()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        slash = true;


    //    }
    //    else
    //    {
    //        slash = false;
    //    }
    //    anim.SetBool("slash", slash);
    //}

    public void setSlash(bool slash)
    {
        anim.SetBool("slash", slash);
        
    }


    private void Attack()
    {
        if (DistanceToBoss <= 4f)
        {
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, boxSize, 0f, Vector2.right * this.transform.localScale.x, boss);
            if (hit)
            {
                if(!bossHealth.isDead())
                {
                    this.bossHealth.TakeDame(player_dame);

                    text.UpdateText(this.player_dame);
                    //BossCtrl.Instance.rb.AddForce(Vector2.right * BossCtrl.Instance.transform.localScale.x * 100);
                }
            }
        }
    }
}
