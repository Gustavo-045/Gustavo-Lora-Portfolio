using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Animation_Mutant1 : MonoBehaviour
{   

    [SerializeField] private Animator Mutant_Animator;
    [SerializeField] private AnimationsManager animationsManager;
    [SerializeField] AnimatorStates animatorStates = AnimatorStates.Idle;
    


    [SerializeField] private enum AnimatorStates
    {
        Walking,
        Running,
        Idle
    };

    // Start is called before the first frame update
    void Start()   
    {
        animationsManager = GameObject.FindWithTag("AnimationsManager").GetComponent<AnimationsManager>();
        Mutant_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        SetAnimationState();
       
    }

    private void SetAnimationState()
    {

        switch(animatorStates)
        {
            case AnimatorStates.Walking:
            {
                animationsManager.ChangeState("Walking", Mutant_Animator);
                break;              
            }
            case AnimatorStates.Running:
            {
                animationsManager.ChangeState("Running", Mutant_Animator);
                break;              
            }

            case AnimatorStates.Idle:
            {
                animationsManager.ChangeState("Idle", Mutant_Animator);
                break;              
            }
        }
        
    }

    public void ChangeCurrentStateAccessMethod(string newState)
    {   
        AnimatorStates State;
        if(Enum.TryParse<AnimatorStates>(newState, out State))
        {
            animatorStates = State;
        }
        
    }

    //ACCESS METHODS
    public string GetCurrentState()
    {
        return animatorStates.ToString();
    }


}
