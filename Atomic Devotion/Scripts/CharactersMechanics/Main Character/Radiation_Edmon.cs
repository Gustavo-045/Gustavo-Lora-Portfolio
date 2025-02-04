using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Radiation_Edmon : MonoBehaviour
{
    [SerializeField] private float Edmon_RadiationLevel;
    [SerializeField] private float radiationLevel;

    private Health_Edmon health_Edmon;
    [SerializeField] private bool isRadiationCoroutineActive;
    [SerializeField] private bool isRadiationDissapearCoroutineActive;

    private float MaxRadiation = 100;

    [SerializeField] private Coroutine radiationExposureCoroutine;
    [SerializeField] private Coroutine radiationDissapearCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        isRadiationDissapearCoroutineActive = false;
        isRadiationCoroutineActive = false;
        Edmon_RadiationLevel = 0.0f;

        // Get the Health_Edmon component attached to this GameObject
        health_Edmon = GetComponent<Health_Edmon>();
    }

    // Update is called once per frame
    void Update()
    {
        EvaluateRadiationEffects();
    }

    // Check the radiation level and manage health accordingly
    private void EvaluateRadiationEffects()
    {
        if (Edmon_RadiationLevel >= MaxRadiation)
        {
            health_Edmon.StartHealth();  // Start health decrease if radiation is at max
        }
        else 
        {
            health_Edmon.StopHealth();   // Stop health decrease if radiation is below max
        }
    }

    // Coroutine for increasing radiation level over time
    private IEnumerator IncreaseRadiationLevel()
    {
        while (true)
        {
            Edmon_RadiationLevel = Mathf.Clamp(Edmon_RadiationLevel + radiationLevel, 0, MaxRadiation);
            yield return new WaitForSeconds(5f); // Wait for 5 seconds before the next exposure
        }
    }

    // Coroutine for decreasing radiation level over time
    private IEnumerator DecreaseRadiationLevel()
    {
        while (true)
        {
            Edmon_RadiationLevel = Mathf.Clamp(Edmon_RadiationLevel - 1, 0, MaxRadiation);
            yield return new WaitForSeconds(5f); // Wait for 5 seconds before the next decrease
        }
    }

    // Start the radiation increase coroutine
    public void StartRadiationIncrease()
    {
        if (!isRadiationCoroutineActive)
        {
            radiationExposureCoroutine = StartCoroutine(IncreaseRadiationLevel());
            isRadiationCoroutineActive = true;
        }
    }

    // Stop the radiation increase coroutine
    public void StopRadiationIncrease()
    {
        if (isRadiationCoroutineActive)
        {
            StopCoroutine(radiationExposureCoroutine);
            isRadiationCoroutineActive = false;
        }
    }

    // Start the radiation decrease coroutine
    private void StartRadiationDecrease()
    {
        if (!isRadiationDissapearCoroutineActive)
        {
            radiationDissapearCoroutine = StartCoroutine(DecreaseRadiationLevel());
            isRadiationDissapearCoroutineActive = true;
        }
    }

    // Stop the radiation decrease coroutine
    private void StopRadiationDecrease()
    {
        if (isRadiationDissapearCoroutineActive)
        {
            StopCoroutine(radiationDissapearCoroutine);
            isRadiationDissapearCoroutineActive = false;
        }
    }

    // Add radiation and manage coroutines
    public void AddRadiation(float radiation)
    {
        radiationLevel += radiation;

        if (radiationLevel > 0)
        {
            StartRadiationIncrease();
            if (isRadiationDissapearCoroutineActive)
            {
                StopRadiationDecrease();
            }
        }
    }

    // Remove radiation and manage coroutines
    public void RemoveRadiation(float radiation)
    {
        radiationLevel -= radiation;

        if (radiationLevel <= 0)
        {
            StopRadiationIncrease();
            if (!isRadiationDissapearCoroutineActive)
            {
                StartRadiationDecrease();
            }
        }
    }

    // Get the state of the radiation increase coroutine
    public bool IsRadiationCoroutineActive()
    {
        return isRadiationCoroutineActive;
    }

    // Access the current radiation level
    public float GetCurrentRadiationLevel()
    {
        return Edmon_RadiationLevel;
    }

    public void stationReduce(float value)
    {
        if(Edmon_RadiationLevel> 0)
        {
            Edmon_RadiationLevel -=  value;
        }
    }

}
