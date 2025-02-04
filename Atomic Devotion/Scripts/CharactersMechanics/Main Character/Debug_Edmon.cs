using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Debug_Edmon : MonoBehaviour
{
    [SerializeField] private TMP_Text DebugText;
    [SerializeField] private Animation_Edmon animation_Edmon;
    [SerializeField] private Radiation_Edmon radiation_Edmon;
    private Health_Edmon health_Edmon;

    
    // Start is called before the first frame update
    void Start()
    {
        animation_Edmon = GetComponent<Animation_Edmon>();
        radiation_Edmon = GetComponent<Radiation_Edmon>();
        health_Edmon = GetComponent<Health_Edmon>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //DebugText.text = $"Current_AnimState:{animation_Edmon.GetCurrentState()}\n RadiationExposure:{radiation_Edmon.GetCoroutineState()}\n RadiationLevel:{radiation_Edmon.GetRadiationLevel()}\n Health: {health_Edmon.GetHealth()}";

    }
}
