using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startgame_behaviour : StateMachineBehaviour
{
   
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CoroutineHelper.Instance.StartCoroutine(Wait(animator));
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    protected virtual IEnumerator Wait(Animator animator)
    {
        yield return new WaitForSeconds(0f);
        animator.SetTrigger("StartGame");
    }

}
