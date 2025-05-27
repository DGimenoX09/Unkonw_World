using UnityEngine;

public class CambioCarril : MonoBehaviour
{
    public CharacterController controller;
    public float laneDistance = 2f;
    public float transitionSpeed = 15f;
    private float currentLane = -0.006881595f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool canChangeLane = false;
    private bool wasInZone = false;
    private bool hasChangedInAir = false;

    public bool isAlive = true; // Control de estado de vida

    void Start()
    {
        targetPosition = transform.position;
        targetPosition.x = currentLane;
    }

    void Update()
    {
        if (!isAlive) return;

        CheckLaneZone();

        Vector3 moveDirection = new Vector3(targetPosition.x - transform.position.x, 0, 0);
        if (moveDirection.magnitude > 0.05f)
        {
            controller.Move(moveDirection.normalized * transitionSpeed * Time.deltaTime);
        }
        else if (isMoving)
        {
            isMoving = false;
            transform.position = new Vector3(currentLane, transform.position.y, transform.position.z);
        }

        if (Input.GetButtonDown("CambioCarril") && !isMoving && (canChangeLane || (wasInZone && !hasChangedInAir)))
        {
            ChangeLane();
            if (!controller.isGrounded) hasChangedInAir = true;
        }

        if (controller.isGrounded)
        {
            wasInZone = false;
            hasChangedInAir = false;
        }
    }

    void ChangeLane()
    {
        isMoving = true;
        currentLane = (currentLane == -0.006881595f) ? 8f : -0.006881595f;
        targetPosition.x = currentLane;
        // Debug.Log("CAMBIANDO DE CARRIL");
    }

    void CheckLaneZone()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.down * 0.5f, 0.2f, Physics.AllLayers, QueryTriggerInteraction.Collide);
        canChangeLane = false;
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("CambioCarrilZone"))
            {
                canChangeLane = true;
                wasInZone = true;
                break;
            }
        }
    }

    public void ResetToMainLane()
    {
        currentLane = -0.006881595f;
        isMoving = false;
        Debug.Log("RESETEANDO RESETEANDO");
        // Mover al carril principal directamente
        Vector3 newPosition = transform.position;
        newPosition.x = currentLane;
        transform.position = newPosition;

        // Recalcular target para evitar movimiento no deseado
        targetPosition = transform.position;
    }
}
