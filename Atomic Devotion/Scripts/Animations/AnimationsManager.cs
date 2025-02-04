using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    private Animation_Running animation_Running;
    private Animation_Walking animation_Walking;
    private Animation_Idle animation_Idle;
    private Animation_Interacting animation_Interacting;


    [SerializeField] private string currentState;
   
    // Start is called before the first frame update
    void Start()
    {
        animation_Running = this.GetComponent<Animation_Running>();
        animation_Walking = this.GetComponent<Animation_Walking>();
        animation_Idle = this.GetComponent<Animation_Idle>();
        animation_Interacting = this.GetComponent<Animation_Interacting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(string State, Animator animator)
    {
        switch(State)
        {
            case "Walking":
            animation_Walking.ChangeState(animator);
            break;

            case "Running":
            animation_Running.ChangeState(animator);
            break;

            case "Idle":
            animation_Idle.ChangeState(animator);
            break;

            case "Interacting":
            animation_Interacting.ChangeState(animator);
            break;
        }
    }
    
        
} 

