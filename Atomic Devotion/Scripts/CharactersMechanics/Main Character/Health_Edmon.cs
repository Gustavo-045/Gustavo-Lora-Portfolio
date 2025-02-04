using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Edmon : MonoBehaviour
{

    [SerializeField] private float CurrentHealht;
    [SerializeField] private const float MaxtHealht = 100;

    [SerializeField] private Coroutine HealthCoroutine;

    [SerializeField] private bool CoroutineActive;

        private int activationCount;


    // Start is called before the first frame update
    void Start()
    {
        CoroutineActive = false;
        CurrentHealht = MaxtHealht;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void takelife()
    {

    }

    private IEnumerator TakeLife()
    {
        while(true)
        {
            CurrentHealht-=1;
            yield return new WaitForSeconds(5f);
        }


    }

    //Access Methods

    public void StartHealth()
    {
        if (!CoroutineActive)
        {
            HealthCoroutine = StartCoroutine(TakeLife());
            CoroutineActive = true;
            activationCount++;
            Debug.Log("Corutina activada " + activationCount + " veces."); // Log de activaciones
            


        }

    }

    public void StopHealth()
    {
        if (CoroutineActive)
        {
            StopCoroutine(HealthCoroutine);
            CoroutineActive = false;
                        Debug.Log("Corutina detenida."); // Log de detención


        }

    }
    public float GetHealth()
    {
        return CurrentHealht;
    }
}
