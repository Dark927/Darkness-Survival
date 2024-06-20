using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : StateMachineBehaviour
{
    // Check attack status
    bool isAttacking = false;

    // OnStateEnter is called when attack state is started
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = true; // Check attack is started 
    }

    // OnStateExit is called when attack state is finished
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = false; // Check attack is finished
    }

    // Taking isAttacking from other classes 
    public bool IsAttacking
    {
        get { return isAttacking; }
        private set { }
    }
}
