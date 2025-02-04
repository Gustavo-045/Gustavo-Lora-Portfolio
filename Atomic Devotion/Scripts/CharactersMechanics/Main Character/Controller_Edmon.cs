using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edmon_Controller : MonoBehaviour
{

    [SerializeField] private Rigidbody Edmon_RigidBody;
    [SerializeField] private float Edmon_Speed;
    [SerializeField] float Edmon_SprintSpeed;
    [SerializeField] float Edmon_TurnSpeed;
    [SerializeField] private float HorizontalMove;
    [SerializeField] private float VerticalMove;
    private Animation_Edmon animation_Edmon;

    private bool Interacting;
    // Start is called before the first frame update
    void Start()   
    {
       // Edmon_RigidBody = GetComponent<Rigidbody>();
        Edmon_Speed = 2.0f;
        Edmon_SprintSpeed = 1.0f;
        Edmon_TurnSpeed = 12.0f;
        //animation_Edmon = GetComponent<Animation_Edmon>();
        Interacting = false;
    }

    // Update is called once per frame
    void Update()
    {        
        HorizontalMove = Input.GetAxis("Horizontal");
        VerticalMove = Input.GetAxis("Vertical");
        
        if(!Interacting)
        {
            CheckMovement();
        }
        


        
    }

    private void CheckMovement()
    {
        if (HorizontalMove !=0 || VerticalMove != 0)
        {            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                CharacterRunning();
                animation_Edmon.ChangeCurrentStateAccessMethod("Running");
            }
            else
            {
                CharacterWalking();
                animation_Edmon.ChangeCurrentStateAccessMethod("Walking");
            }
            
        }
        else
        {
            animation_Edmon.ChangeCurrentStateAccessMethod("Idle");
        }
    }

    void FixedUpdate()
    {    
        Movement();

    }

    private void Movement()
{
    if (!Interacting) // Solo se mueve si no está interactuando
    {
        Vector3 Movement = new Vector3(HorizontalMove, 0, VerticalMove);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, Movement, Edmon_TurnSpeed * Time.deltaTime, 0f);

        Quaternion Rotation = Quaternion.LookRotation(desiredForward);

        Edmon_RigidBody.MovePosition(transform.position + Movement * Edmon_Speed * Edmon_SprintSpeed * Time.deltaTime);
        Edmon_RigidBody.MoveRotation(Rotation);
    }
    else
    {
        // No moverse mientras está interactuando
    }
}


    private void CharacterWalking()
    {
        Edmon_SprintSpeed = 1.0f;               
    }

    private void CharacterRunning()
    {
        Edmon_SprintSpeed = 2.0f;
    }


    // ACCESS METHODS

    public void SetInteract(bool interacting)
    {
        Interacting = interacting;
    }

}
