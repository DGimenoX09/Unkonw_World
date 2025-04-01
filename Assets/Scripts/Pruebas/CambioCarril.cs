using UnityEngine;

public class CambioCarril : MonoBehaviour
{
    public CharacterController controller;
    public float laneDistance = 2f; // La distancia entre los carriles
    public float transitionSpeed = 15f; // La velocidad de transicion
    private float currentLane = -0.006881595f; // Carril inicial
    private Vector3 targetPosition;
    private bool isMoving = false; // Controla si el personaje esta cambiando de carril
    private bool canChangeLane = false; // Habilidad activada solo en ciertas plataformas
    private bool wasInZone = false; // Permite cambiar de carril en el aire si estuvo en la zona
    private bool hasChangedInAir = false; // Evita multiples cambios en el aire

    void Start()
    {
        // Inicializamos la posicion de destino con el carril inicial
        targetPosition = transform.position;
        targetPosition.x = currentLane;
    }

    void Update()
    {
        // Detecta si el personaje esta sobre una plataforma valida
        CheckLaneZone();
        
        // Movimiento suave hacia el nuevo carril
        Vector3 moveDirection = new Vector3(targetPosition.x - transform.position.x, 0, 0);
        if (moveDirection.magnitude > 0.05f) // Si aun no ha llegado al destino
        {
            controller.Move(moveDirection.normalized * transitionSpeed * Time.deltaTime);
        }
        else if (isMoving)
        {
            // Cuando llega al destino, desbloqueamos el cambio de carril
            isMoving = false;
            transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z); // Asegurar la posicion exacta
        }

        // Solo permite el cambio de carril si no esta en movimiento, la habilidad esta activada y no ha cambiado en el aire
        if (Input.GetKeyDown(KeyCode.C) && !isMoving && (canChangeLane || (wasInZone && !hasChangedInAir)))
        {
            ChangeLane();
            if (!controller.isGrounded) hasChangedInAir = true; // Marca que ya cambio en el aire
        }

        // Si el personaje toca el suelo, resetea las variables
        if (controller.isGrounded)
        {
            wasInZone = false;
            hasChangedInAir = false;
        }
    }

    void ChangeLane()
    {
        isMoving = true; // Bloquea el cambio hasta que termine de moverse

        // Cambia entre las dos posiciones especificas
        currentLane = (currentLane == -0.006881595f) ? -4f : -0.006881595f;

        // Actualiza la posicion de destino segun el carril seleccionado
        targetPosition.x = currentLane;
    }

    void CheckLaneZone()
    {
        // Lanza un pequeno chequeo debajo del personaje para detectar si esta sobre una zona valida
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.down * 0.5f, 0.2f, Physics.AllLayers, QueryTriggerInteraction.Collide);
        canChangeLane = false;
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("CambioCarrilZone"))
            {
                canChangeLane = true;
                wasInZone = true; // Si el personaje entra en la zona, recuerda que puede cambiar de carril en el aire
                break;
            }
        }
    }
}
