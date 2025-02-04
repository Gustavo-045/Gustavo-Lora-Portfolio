using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debug_Mutant : MonoBehaviour
{
    [SerializeField] private TMP_Text DebugText;
    [SerializeField] private Controller_Mutant1 controller_Mutant1;
    [SerializeField] private RadiationZone radiation_Zone;
    [SerializeField] private Animation_Mutant1 animation_Mutant1;
    // Start is called before the first frame update
    void Start()
    {
        controller_Mutant1 = GetComponent<Controller_Mutant1>();
        radiation_Zone = GetComponent<RadiationZone>();
        animation_Mutant1 = GetComponent<Animation_Mutant1>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDebugValue();
    }

    void GetDebugValue()
    {
        DebugText.text = $"Current_Target:{controller_Mutant1.GetCurrentTarget()}\n Collision: {radiation_Zone.GetCollision()}\n Current State: {controller_Mutant1.GetCurrentState()}\nCurrent_AnimState:{animation_Mutant1.GetCurrentState()}";
    }
}
