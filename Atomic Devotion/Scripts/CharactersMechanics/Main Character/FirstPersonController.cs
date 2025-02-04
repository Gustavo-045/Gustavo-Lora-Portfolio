using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 4.0f;
    [SerializeField] private float mouseSensitivity = 2.0f;

    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float verticalRotationLimit = 85.0f;

    private Rigidbody playerRigidbody;
    private Animation_Edmon animationEdmon;
    private float verticalRotation = 0f;
    private bool isInteracting;

    private InventoryManager inventoryManager;
    private DocumentManager documentManager;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animationEdmon = GetComponent<Animation_Edmon>();
        inventoryManager = Object.FindFirstObjectByType<InventoryManager>();
        documentManager = Object.FindFirstObjectByType<DocumentManager>();

        Cursor.lockState = CursorLockMode.Locked;

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        isInteracting = false;
    }

    private void Update()
    {
        LookAround();
        HandleMovement();
        if (inventoryManager != null || documentManager != null)
        {
            inventoryManager = Object.FindFirstObjectByType<InventoryManager>();
            documentManager = Object.FindFirstObjectByType<DocumentManager>();
        } else 
        {
            return;
        }
    }

    private void LookAround()
    {
        if (inventoryManager != null && !inventoryManager.isInventoryOpen && documentManager != null && !documentManager.isPaused)
        {
            //Debug.Log("Mover Camara");
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    private void HandleMovement()
    {
        if (!isInteracting)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveX, 0, moveZ);
            movement = transform.TransformDirection(movement);
            movement.y = 0;

            // Normalize movement to avoid faster diagonal movement
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }

            // Determine speed based on sprinting
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            // Move the player with physics
            Vector3 targetVelocity = movement * speed;
            targetVelocity.y = playerRigidbody.velocity.y; // Keep existing vertical velocity for gravity
            playerRigidbody.velocity = targetVelocity;

            // Check if the movement affects animation state
            CheckMovement(movement);
        }
    }

    private void CheckMovement(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animationEdmon.ChangeCurrentStateAccessMethod("Running");
            }
            else
            {
                animationEdmon.ChangeCurrentStateAccessMethod("Walking");
            }
        }
        else
        {
            animationEdmon.ChangeCurrentStateAccessMethod("Idle");
        }
    }

    // Access Methods
    public void SetInteract(bool interacting)
    {
        isInteracting = interacting;
    }

    public bool GetInteract()
    {
        return isInteracting;
    }
}
