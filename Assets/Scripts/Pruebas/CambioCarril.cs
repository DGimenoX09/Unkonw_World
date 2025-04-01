using UnityEngine;

public class CambioCarril : MonoBehaviour
{
    public CharacterController controller;
    public float laneDistance = 2f; // La distancia entre los carriles (puedes modificarla si es necesario)
    public float transitionSpeed = 5f; // La velocidad de transicion
    private float currentLane = -0.006881595f; // El carril inicial
    private Vector3 targetPosition;
    private bool isMoving = false; // Evita que presionen C mientras se mueve

    void Start()
    {
        // Inicializamos la posicion de destino con el carril inicial
        targetPosition = transform.position;
        targetPosition.x = currentLane;
    }

    void Update()
    {
        // Detecta si se presiona la tecla C para cambiar de carril
        if (Input.GetKeyDown(KeyCode.C) && !isMoving)
        {
            ChangeLane();
        }

        // Movimiento suave hacia el nuevo carril
        Vector3 moveDirection = new Vector3(targetPosition.x - transform.position.x, 0, 0);
        if (moveDirection.magnitude > 0.05f) // Si aun no ha llegado al destino
        {
            controller.Move(moveDirection * transitionSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false; // Desbloquea el cambio de carril cuando se llega a la posicion
        }
    }

    void ChangeLane()
    {
        isMoving = true; // Bloquea el cambio hasta que termine de moverse

        // Cambia entre las dos posiciones especificas
        if (currentLane == -0.006881595f)
        {
            currentLane = -4f; // Cambia a la posicion 1
        }
        else
        {
            currentLane = -0.006881595f; // Cambia a la posicion inicial
        }

        // Actualiza la posicion de destino segun el carril seleccionado
        targetPosition.x = currentLane;
    }
}