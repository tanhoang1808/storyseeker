using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss_RageRun : Boss_run
{
    protected new float speed = 9f;
    protected float speedboost = 33f;
    protected new float AttackRange = 6f;
    protected new float DelayAttack = 1f;
    private bool WaitingAttack = false;
    public int countHit = 0;
    private Vector2 boxsize = new(1, 1);
    public Transform pointA;
    public Transform pointB;
    public LayerMask playerMask;
    public bool isPower = false;
    public BossMove BossMove;
    public LaserDuration laserDuration;
    public float DelayPower = 1.5f;
    public CapsuleCollider2D collider;
    public Vector2 offsetDefault;
    public float GravityDefault ;
    protected float distanceLimit = 4f;




    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        bossHealth = animator.GetComponent<BossHealth>();
        pointA = GameObject.FindWithTag("pointA").GetComponent<Transform>();
        pointB = GameObject.FindWithTag("pointB").GetComponent<Transform>();
        BossMove = animator.GetComponent<BossMove>();
        laserDuration = animator.GetComponentInChildren<LaserDuration>();
        collider = animator.GetComponent<CapsuleCollider2D>();
        offsetDefault = this.collider.offset;
        GravityDefault = this.rb.gravityScale;
    }

     //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Vector2 Target;
        if(this.rb.position.x > this.Player.position.x)
        {
             Target = new Vector2(Player.position.x + 1.5f, rb.position.y);
        }
        else
        {
            Target = new Vector2(Player.position.x - 1.5f, rb.position.y);
        }


        Vector2 newPos = Vector2.MoveTowards(this.rb.position, Target, speed * Time.fixedDeltaTime);

        
        
        rb.MovePosition(newPos);
        float playerPosX = Player.position.x;

        if (Vector2.Distance(Target, rb.position) <= AttackRange)
        {
            this.WaitingAttack = true;
            CoroutineHelper.Instance.StartCoroutine(WaitForAttack(animator, DelayAttack));
           
        }
        else if(Vector2.Distance(Target, rb.position) >= AttackRange)
        {
            this.WaitingAttack = false;
            CoroutineHelper.Instance.StopCoroutine(WaitForAttack(animator, 0));
        }

        
        this.collider.offset = offsetDefault;
        this.rb.gravityScale = GravityDefault;
        if(bossHealth.TriggerHealth >= 200)
        {
           
            CoroutineHelper.Instance.StopAllCoroutines();
            //Make boss can not collider with player when Power
            
            if (this.rb.transform.position.x >= playerPosX)
            {
                Vector2 castPos = Vector2.MoveTowards(this.rb.position, this.pointB.position, speedboost * Time.fixedDeltaTime);
                rb.MovePosition(castPos);
                this.rb.transform.localScale = new Vector2(-1, 1);
               
               if(this.rb.position.x >= pointB.position.x - 1)
                {
                    RaycastHit2D hit = Physics2D.BoxCast(this.rb.position, boxsize, 0f, Vector2.right * this.rb.transform.localScale.x, 10f);
                    if(hit)
                    {
                        Debug.Log("Hit");
                        CoroutineHelper.Instance.StartCoroutine(WaitForPower(animator));
                        bossHealth.TriggerHealth = 0;
                    }
                }

            }

            else if (this.rb.transform.position.x < playerPosX)
            {
                Vector2 castPos = Vector2.MoveTowards(this.rb.position, this.pointA.position, speedboost * Time.fixedDeltaTime);
                rb.MovePosition(castPos);
                this.rb.transform.localScale = new Vector2(1, 1);
                if (this.rb.position.x <= pointA.position.x + 1)
                {
                    RaycastHit2D hit = Physics2D.BoxCast(this.rb.position, boxsize, 0f, Vector2.right * this.rb.transform.localScale.x, 10f);
                    if (hit)
                    {
                        Debug.Log("Hit");
                        CoroutineHelper.Instance.StartCoroutine(WaitForPower(animator));
                        bossHealth.TriggerHealth = 0;
                    }
                }
            }
           
        }
        if (bossHealth.isDead())
        {
            CoroutineHelper.Instance.StopAllCoroutines();
            bossHealth.TriggerHealth = 0;
            bossHealth.currentHealth = 0;
            Rigidbody2D rigid = BossCtrl.Instance.GetComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Kinematic;
            rigid.gravityScale = 0;
            CapsuleCollider2D collider = BossCtrl.Instance.GetComponent<CapsuleCollider2D>();
            collider.enabled = false;
            
            animator.SetTrigger("Death");
        }




    }

    protected virtual IEnumerator WaitForPower(Animator animator)
    {
        animator.SetTrigger("Power");
        IndicatiorCtrl.Indicatior.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        laserDuration.Duration();
        yield return new WaitForSeconds(0.4f);
        LaserCtrl.Instance.DisableLaser();
        IndicatiorCtrl.Indicatior.gameObject.SetActive(false);
    }


   protected override IEnumerator WaitForAttack(Animator animator,float delay)
    {
        yield return new WaitForSeconds(delay);
        if (WaitingAttack == true)
        {
            animator.SetTrigger("Attack");
        }
    }



    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        
    }

    
}
