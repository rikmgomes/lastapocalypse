using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : StateMachineBehaviour
{
    public float reloadTime = 0.7f;
    bool hasReloaded = false;

    // quando começa a animação
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasReloaded = false;
    }

    // igual ao update normal
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (hasReloaded)
        {
            return;
        }
        if (stateInfo.normalizedTime >= reloadTime)
        {
            animator.GetComponent<Weapon>().Reload();
            hasReloaded = true;
        }
    }

    // para de executar a animação
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasReloaded = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
