using UnityEngine;

public class CambioCarril : MonoBehaviour

{
    public CharacterController controller;
    public float laneDistance = 2f; 
    public float transitionSpeed = 5f; 
    private int currentLane = 1; 
    private Vector3 targetPosition;
    private bool isMoving = false; // Evita que presionen C mientras se mueve

    void Start()
    {
        targetPosition = transform.position;
        targetPosition.x = currentLane * laneDistance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isMoving)
        {
            ChangeLane();
        }

        // Movimiento suave hacia el nuevo carril
        Vector3 moveDirection = new Vector3(targetPosition.x - transform.position.x, 0, 0);
        if (moveDirection.magnitude > 0.05f) // Si aún no llegó al destino
        {
            controller.Move(moveDirection * transitionSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false; // Desbloquea el cambio de carril
        }
    }

    void ChangeLane()
    {
        isMoving = true; // Bloquea el cambio hasta que termine de moverse
        currentLane *= -1; // Alterna entre -1 y 1
        targetPosition.x = currentLane * laneDistance;
    }

}