using UnityEngine;

public class TeleportToSecondLevel : MonoBehaviour
{
    public Transform level2StartPoint; // Asigna aquí el punto de inicio del nivel 2
    public GameObject player; // Asigna el jugador en el inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (player != null && level2StartPoint != null)
            {
                CharacterController controller = player.GetComponent<CharacterController>();

                // Desactiva el CharacterController temporalmente para evitar errores de teletransporte
                if (controller != null)
                    controller.enabled = false;

                player.transform.position = level2StartPoint.position;

                if (controller != null)
                    controller.enabled = true;

                Debug.Log("Teletransportado al inicio del nivel 2");
            }
        }
    }
}
