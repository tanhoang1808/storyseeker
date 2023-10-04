using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_run : StateMachineBehaviour
{
    public Transform Player;
    protected float speed = 3f;
    protected float AttackRange = 4f;
    protected float DelayAttack = 1.5f;
    public BossHealth bossHealth;
     private bool WaitingAttack = false;

    public Rigidbody2D  rb;
     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        bossHealth = animator.GetComponent<BossHealth>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 Target = new Vector2(Player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(this.rb.position, Target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(Target, rb.position) <= AttackRange)
        {
            WaitingAttack = true;
            CoroutineHelper.Instance.StartCoroutine(WaitForAttack(animator,DelayAttack));
        
        }
        else if (Vector2.Distance(Target, rb.position) >= AttackRange)
        {
            WaitingAttack = false;
            CoroutineHelper.Instance.StopCoroutine(WaitForAttack(animator,0));
            
        }
        //Change State to Rage
        if (bossHealth.currentHealth <= 1000)
        {
            CoroutineHelper.Instance.StopAllCoroutines();
            animator.SetTrigger("Rage");
        }
    }

    protected virtual IEnumerator WaitForAttack(Animator anim,float delay)
    {
     
        yield return new WaitForSeconds(delay);
        if(WaitingAttack == true)
        {
            anim.SetTrigger("Attack");
        }
   
    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    
}
