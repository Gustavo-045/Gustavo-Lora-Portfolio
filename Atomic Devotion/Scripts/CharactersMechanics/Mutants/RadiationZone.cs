using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    [SerializeField] protected float radiationLevel;

    [SerializeField] protected bool collision;

   [SerializeField]  protected Radiation_Edmon radiationEdmon;

   [SerializeField] private GameObject PlayerRef;



    void Start()
    {
        WaitForReference();
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            collision = true;
            PlayerRef = other.gameObject; 
            radiationEdmon = PlayerRef.GetComponent<Radiation_Edmon>();
            radiationEdmon.AddRadiation(radiationLevel);
            //radiationEdmon.AddRadiation(radiationLevel);  
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRef = other.gameObject; 
            radiationEdmon = PlayerRef.GetComponent<Radiation_Edmon>();
            radiationEdmon.RemoveRadiation(radiationLevel);
            //radiationEdmon.RemoveRadiation(radiationLevel);
            collision = false;
        }
    }
        public bool GetCollision()
    {
        return collision;
    }
}