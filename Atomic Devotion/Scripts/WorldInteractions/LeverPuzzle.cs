using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField] private Lever[] levers;   
    [SerializeField]private int[] Position = new int[4];

    [SerializeField]private int[] CorrectPosition = new int[4];

    [SerializeField]private GameObject LeverObject;

    private bool Win;
    // Start is called before the first frame update
    void Start()
    {
        Win = false;
        LeverObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    private void WinPuzzle()
    {
        
        //LeverObject.SetActive(true);
        Debug.Log("waza");
        DisablePuzzle();
    }

    private void DisablePuzzle()
    {
        int j = 0;
        
        for(int i = 0; i < levers.Length; i++)
        {
            BoxCollider LeverBoxCollider;
            LeverBoxCollider = levers[i].GetComponent<BoxCollider>();
            LeverBoxCollider.enabled = !LeverBoxCollider.enabled;
        }

    }

    private void CheckPosition()
    {   
        int j = 0;
        
        for(int i = 0; i < levers.Length; i++)
        {
            
            if(Position[i] == CorrectPosition[i])
            {
                j++;   
            }

            if (j == 4)
            {
                WinPuzzle();
            }
        }

    }

    private void ToggleLever(int index)
    {
         levers[index].SetState(levers[index].GetCurrentPosition() == 0 ? 1 : 0);  
         Position[index] = levers[index].GetCurrentPosition();
        CheckPosition();


    }
    public void ChangeLever(int leverIndex)
    {
        switch (leverIndex)
        {
            //Lever A
            case 0:
            {
                ToggleLever(0);
                ToggleLever(1);
                ToggleLever(2);


                break;
            }

            //Lever B
            case 1:
            {
                 ToggleLever(1);
                ToggleLever(2);
                ToggleLever(3);

                break;
            }

            //Lever C
            case 2:
            {
                 ToggleLever(2);
                ToggleLever(3);

                break;
            }

            //Lever D
            case 3:
            {
               ToggleLever(3);

                break;
            }     
        
        }
            
    }
}
