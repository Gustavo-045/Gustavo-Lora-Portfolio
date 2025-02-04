using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadiation : RadiationZone
{

    private Controller_Mutant1 controllerMutant1;

    
    [SerializeField] private SphereCollider radiationZoneEnemy;
    // Start is called before the first frame update
    void Start()
    {
        radiationZoneEnemy = GetComponent<SphereCollider>();
        StartCoroutine(WaitForReference());

        controllerMutant1 = GetComponent<Controller_Mutant1>();

    }

    private IEnumerator WaitForReference()
    {
        while (radiationEdmon == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                radiationEdmon = playerObject.GetComponent<Radiation_Edmon>();
            }
            yield return null; // Waits one frame before trying again
        }
        // Continue with the setup once edmonRadiation is assigned
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            radiationEdmon.RemoveRadiation(radiationLevel);
            collision = false;
            controllerMutant1.ChangeCurrentStateAccessMethod("Patroling");
            
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            collision = true;
            radiationEdmon.AddRadiation(radiationLevel);  
            controllerMutant1.ChangeCurrentStateAccessMethod("Chasing");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}