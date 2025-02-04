using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Controller_Mutant1 : MonoBehaviour
{
    private NavMeshAgent agent;
    
    [SerializeField] private float Speed_Mutant1 = 2.0f;  // Velocidad de patrulla
    [SerializeField] private float ChaseSpeed_Mutant1 = 4.0f;  // Velocidad de persecución

    [SerializeField] private GameObject[] PatrolPoints;
    [SerializeField] private int CurrentTarget;
    [SerializeField] private GameObject Edmon_Player;

    private EnemyStates EnemyState = EnemyStates.Patroling;
    private Animation_Mutant1 animation_Mutant1;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cinematicDuration = 1.5f; // Duración de la cinemática

    private GameManager gameManager; // Reference to the GameManager

    private enum EnemyStates
    {
        Patroling,
        Chasing
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtenemos el componente NavMeshAgent
        Edmon_Player = GameObject.FindWithTag("Player");
        
        animation_Mutant1 = GetComponent<Animation_Mutant1>();
        gameManager = FindFirstObjectByType<GameManager>(); // Automatically assign GameManager


        // Configuración inicial de velocidad en NavMeshAgent
        agent.speed = Speed_Mutant1;
    }




    void FixedUpdate()
    {
        PatrolMutant();
    }

    void Update()
    {
        Edmon_Player = GameObject.FindWithTag("Player");
                        playerCamera = Edmon_Player.GetComponentInChildren<Camera>(); // Obtenemos la cámara como hijo del jugador

    }

    private void PatrolMutant()
    {
        switch (EnemyState)
        {
            case EnemyStates.Patroling:
                // Cambiamos el destino del agente al siguiente punto de patrulla
                if (CurrentTarget >= PatrolPoints.Length)
                {
                    CurrentTarget = 0;
                }

                agent.speed = Speed_Mutant1;  // Velocidad de patrulla
                agent.SetDestination(PatrolPoints[CurrentTarget].transform.position); // Asignamos el destino
                animation_Mutant1.ChangeCurrentStateAccessMethod("Walking");

                // Cambiamos al siguiente objetivo si estamos lo suficientemente cerca
                if (Vector3.Distance(transform.position, PatrolPoints[CurrentTarget].transform.position) < 3.0f)
                {
                    CurrentTarget++;
                }
                break;

            case EnemyStates.Chasing:
                agent.speed = ChaseSpeed_Mutant1; // Velocidad de persecución
                agent.SetDestination(Edmon_Player.transform.position); // Seguimos al jugador
                animation_Mutant1.ChangeCurrentStateAccessMethod("Running");
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificamos si la colisión es con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Llamamos a la función que activa la cinemática
            StartCoroutine(PlayCinematic());
        }
    }

    private IEnumerator PlayCinematic()
    {
        Vector3 originalPosition = playerCamera.transform.position;
        Quaternion originalRotation = playerCamera.transform.rotation;

        // Calcula la dirección hacia el enemigo
        Vector3 directionToEnemy = (transform.position - playerCamera.transform.position).normalized;

        // Posición de destino para el acercamiento (ajusta esta distancia según el efecto deseado)
        Vector3 targetPosition = transform.position - directionToEnemy * 0.5f;

        // Interpolación de posición y rotación
        float elapsedTime = 0f;
        while (elapsedTime < cinematicDuration)
        {
            // Rotación hacia el enemigo
            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            playerCamera.transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, elapsedTime / cinematicDuration);

            // Movimiento hacia el enemigo
            playerCamera.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / cinematicDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restablecemos la posición y rotación de la cámara
        playerCamera.transform.position = originalPosition;
        playerCamera.transform.rotation = originalRotation;

        // Llamamos a RespawnPlayer() después de la cinemática
        RespawnPlayer();
    }

    // Métodos de acceso
    public int GetCurrentTarget()
    {
        return CurrentTarget;
    }

    public string GetCurrentState()
    {
        return EnemyState.ToString();
    }

    public void ChangeCurrentStateAccessMethod(string State)
    {
        if (System.Enum.TryParse(State, out EnemyStates newState))
        {
            EnemyState = newState;
        }
    }

    // Método que será llamado al finalizar la cinemática
    private void RespawnPlayer()
    {
        
         EnemyState = EnemyStates.Patroling;
        CurrentTarget = 0;
        gameManager.RespawnPlayer();    
        }
}
