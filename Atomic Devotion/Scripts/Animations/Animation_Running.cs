using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Running : MonoBehaviour, I_Animation
{
    

    public void ChangeState(Animator Animator)
    {
        Animator.SetBool("IsRunning", true);
        Animator.SetBool("IsWalking", false);  
        Animator.SetBool("IsInteracting", false); 
        
    } 
}
