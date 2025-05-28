using UnityEngine;

public class TeleportToSecondLevel : MonoBehaviour
{
    public Transform level2StartPoint;  // Punto inicio nivel 2 (tecla U)
    public Transform level3StartPoint;  // Punto inicio nivel 3 (tecla I)
    public Transform partefinal;   // Otro punto personalizado (tecla O)
    public GameObject player;           // El jugador

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TeleportTo(level2StartPoint, "nivel 2");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            TeleportTo(level3StartPoint, "nivel 3");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            TeleportTo(partefinal, "otro punto");
        }
    }

    void TeleportTo(Transform destination, string name)
    {
        if (player != null && destination != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();

            if (controller != null)
                controller.enabled = false;

            player.transform.position = destination.position;

            if (controller != null)
                controller.enabled = true;

            Debug.Log("Teletransportado al " + name);
        }
        else
        {
            Debug.LogWarning("Falta asignar el jugador o el punto de inicio de " + name);
        }
    }
}
