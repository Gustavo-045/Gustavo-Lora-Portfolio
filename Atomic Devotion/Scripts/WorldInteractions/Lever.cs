using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class Lever : InteractableObject
{
    [SerializeField] private int initialPotition;
    [SerializeField] private int correctPosition;
    [SerializeField] private int currentPosition;

    [SerializeField] private LeverPuzzle leverPuzzle;
    [SerializeField] private TMP_Text DebugText;

    [SerializeField] private int leverIndex;
    [SerializeField] private Animator leverAnimator;

    // Nuevo campo para asignar el objeto visual de la palanca
    //[SerializeField] private GameObject leverVisual;

    public static int Lenght { get; internal set; }

    public override void Interact()
    {
        leverPuzzle.ChangeLever(leverIndex);
    }

    public void SetState(int state)
    {
        currentPosition = state;
        Debug.Log(currentPosition);

            if (state == 0)
            {
                leverAnimator.SetBool("LeverUp", true);
                leverAnimator.SetBool("LeverDown",false);

            }
            if (state == 1)
            {
                leverAnimator.SetBool("LeverDown",true);
                leverAnimator.SetBool("LeverUp", false);

            }


        // Aplica la rotación al objeto visual de la palanca
        /*if (leverVisual != null)
        {
            // Rota hacia arriba cuando el estado es 0 y hacia abajo cuando es 1
            //leverVisual.transform.rotation = state == 0 ? Quaternion.Euler(45, 0, 0) : Quaternion.Euler(-45, 0, 0);

        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        leverAnimator = GetComponent<Animator>();
        currentPosition = initialPotition;

        leverPuzzle = GetComponentInParent<LeverPuzzle>();

        // Ajusta la posición inicial de la visualización de acuerdo con el estado inicial
        SetState(currentPosition);
    }

    // Update is called once per frame
    void Update()
    {
        DebugText.text = $"Current State: {currentPosition}";
    }

    public int GetCurrentPosition()
    {
        return currentPosition;
    }
}
