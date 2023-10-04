using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_dead : StateMachineBehaviour
{
    public PlayerHealth playerHealth;
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerHealth = animator.GetComponent<PlayerHealth>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playerHealth.isDead())
        {
            CoroutineHelper.Instance.StartCoroutine(WaitForDead(animator));
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    protected virtual IEnumerator WaitForDead(Animator animator)
    {
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("isDead");
    }

}
