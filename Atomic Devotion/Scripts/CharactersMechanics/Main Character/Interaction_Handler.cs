using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Handler : MonoBehaviour
{
    [SerializeField] private bool Interacting; /* El valor de esta variable provendra de la clase FirstPersonController, que gestiona el estado 
    de interacción del jugador y se asigna a través del método SetInteract() en la clase Animation_Edmon. */

    [SerializeField] private bool CanInteract;
    [SerializeField] private FirstPersonController edmon_Controller;
    [SerializeField] private Animation_Edmon animation_Edmon;
    [SerializeField] private InteractableObject interactableObject;

    [SerializeField] private Animator AninPlayerController;
    [SerializeField] private Camera edmonCamera; // Cámara de Edmon
    [SerializeField] private GameObject circulo; // El sprite 2D del circulo

    // Nueva variable para la distancia máxima del raycast
    [SerializeField] private float maxRaycastDistance; // Ajusta esta distancia a tu gusto

    [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] private DocumentManager documentManager;
[SerializeField] private bool flashlightPickedUp = false; // Control para evitar recoger la linterna varias veces


    private void Start()
    {
        // Inicialización de interacciones y controles
        Interacting = false;
        maxRaycastDistance = 1f; 
        edmon_Controller = this.GetComponent<FirstPersonController>();
        animation_Edmon = this.GetComponent<Animation_Edmon>();

        // Iniciamos la corutina para esperar las referencias necesarias
        StartCoroutine(WaitForReferences());
    }

    // Corutina para esperar las referencias necesarias
    private IEnumerator WaitForReferences()
    {
        // Esperamos un momento para asegurarnos de que las referencias se inicialicen correctamente
        yield return new WaitForEndOfFrame(); // Espera hasta el final del cuadro actual

        // Buscamos automáticamente las referencias dentro del entorno de juego
        // Busca la cámara como un hijo dentro del prefab de Edmon
        Transform cameraTransform = transform.Find("Camera"); // Suponiendo que el objeto de la cámara tiene el nombre "Camera"
        if (cameraTransform != null)
        {
            edmonCamera = cameraTransform.GetComponent<Camera>(); // Obtén el componente Camera desde el transform encontrado
        }

        // Busca el circulo de la retícula
        circulo = GameObject.Find("Circle"); // Busca el objeto de la retícula (Circulo)

        if (edmonCamera == null || circulo == null)
        {
            Debug.LogError("Faltan referencias necesarias (Cámara o Circulo)");
            yield break; // Si alguna referencia es nula, terminamos la corutina
        }

         inventoryManager = Object.FindFirstObjectByType<InventoryManager>();
        documentManager = Object.FindFirstObjectByType<DocumentManager>();

        Debug.Log("Referencias encontradas y listas.");
    }

    private void Update()
    {
        if (edmonCamera == null || circulo == null)
        {
            return; // Si las referencias no están listas, no hacemos nada
        }

        Interacting = edmon_Controller.GetInteract(); // Actualiza el estado de Interacting desde el controlador.
        HandleInteraction();
    }

private void HandleInteraction()
{
    if (CanInteract && inventoryManager != null && !inventoryManager.isInventoryOpen && documentManager != null && !documentManager.isPaused)
    {
        if (!Interacting && Input.GetKeyDown(KeyCode.F))
        {
            if (interactableObject != null && CanInteractWithObject(interactableObject))
            {
                // Verifica si es la linterna y si ya fue recogida
                if (interactableObject.CompareTag("Interactable") && interactableObject.name == "Linterna" && flashlightPickedUp)
                {
                    Debug.Log("La linterna ya está en el inventario, no se puede recoger de nuevo.");
                    return; // Evita recoger la linterna otra vez
                }

                Debug.Log("Empezamos interaccion");
                interactableObject.Interact();
                AninPlayerController.SetTrigger("interactuar");

                // Si es la linterna, marca que ya ha sido recogida
                if (interactableObject.name == "Linterna")
                {
                    flashlightPickedUp = true;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2); 
            Ray ray = edmonCamera.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out hit, maxRaycastDistance)) 
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    interactableObject = hit.collider.GetComponent<InteractableObject>();
                    if (interactableObject != null && CanInteractWithObject(interactableObject))
                    {
                        // Verifica si es la linterna y si ya fue recogida
                        if (interactableObject.name == "Linterna(Clone)" && flashlightPickedUp)
                        {
                            Debug.Log("La linterna ya está en el inventario, no se puede recoger de nuevo.");
                            return; // Evita recoger la linterna otra vez
                        }

                        Debug.Log("Interacción con clic");
                        interactableObject.Interact();
                        AninPlayerController.SetTrigger("interactuar");

                        // Si es la linterna, marca que ya ha sido recogida
                        if (interactableObject.name == "Linterna")
                        {
                            flashlightPickedUp = true;
                        }
                    }
                }
            }
        }
    }
}


    private bool CanInteractWithObject(InteractableObject obj)
    {
        // Verifica si el collider isTrigger de la zona interactuable está activo
        if (obj != null)
        {
            Collider[] colliders = obj.GetComponents<Collider>();
            foreach (Collider collider in colliders)
            {
                // Si algún collider isTrigger está desactivado, no se puede interactuar
                if (collider.isTrigger && !collider.enabled)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactableObject = other.GetComponent<InteractableObject>();
            if (CanInteractWithObject(interactableObject))
            {
                CanInteract = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            CanInteract = false;
            interactableObject = null; // Limpiamos el objeto interactuable al salir del área
        }
    }
}
