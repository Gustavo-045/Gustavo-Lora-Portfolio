using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HStation_Object : InteractableObject
{
    
    [SerializeField] private Radiation_Edmon radiationEdmon;
    [SerializeField] private Transform currentCheckpoint; // The current respawn point
    private GameManager gameManager; // Reference to the GameManager

void Start()
    {
                gameManager = FindFirstObjectByType<GameManager>(); // Automatically assign GameManager

    }

    void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
         radiationEdmon = playerObject.GetComponent<Radiation_Edmon>();
         currentCheckpoint = GetComponent<Transform>();
    }

    
    public override void Interact()
    {
        Debug.Log("Se salvo el juego");
            gameManager.SetRespawnPosition(currentCheckpoint.position); // Set respawn in GameManager
        // Asegúrate de que radiationEdmon esté asignado
        if (radiationEdmon != null)
        {
            // Reduce la radiación en 5
            radiationEdmon.stationReduce(5f);

            // Imprime en consola que se ha salvado el juego
            Debug.Log("Se salvó el juego. Radiación reducida en 5.");
        }
        else
        {
            Debug.LogWarning("Radiation_Edmon no está asignado en el HStation_Object.");
        }
    }
}

