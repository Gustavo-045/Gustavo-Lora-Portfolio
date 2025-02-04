using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Interacting : MonoBehaviour, I_Animation
{
    public void ChangeState(Animator Animator)
    {
        Animator.SetBool("IsRunning", false);
        Animator.SetBool("IsWalking", false);
        Animator.SetTrigger("IsInteracting");  
    } 
}

