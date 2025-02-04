using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomRadiation : RadiationZone
{

    [SerializeField] private BoxCollider radiationZoneRoom;



    // Start is called before the first frame update
    void Start()
    {

        WaitForReference();
        radiationZoneRoom = GetComponent<BoxCollider>();
    }

        private IEnumerator WaitForReference()
    {
        while (radiationEdmon == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                Debug.Log("Player Found, assigning component");
                radiationEdmon = playerObject.GetComponent<Radiation_Edmon>();
            }
            yield return null; // Waits one frame before trying again
        }
        // Continue with the setup once edmonRadiation is assigned
    }
        
}


