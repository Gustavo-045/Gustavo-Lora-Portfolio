using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Walking : MonoBehaviour, I_Animation
{
    public void ChangeState(Animator Animator)
    {
        Animator.SetBool("IsRunning", false);
        Animator.SetBool("IsWalking", true); 
        Animator.SetBool("IsInteracting", false);  
    }
}
