using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    public Transform respawnPoint; // Punto vacío donde reaparecerá el jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destruir"))
        {
            Debug.Log("Has muerto"); // Mensaje de depuración

            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null)
            {
                // Desactivar el CharacterController para cambiar la posición sin conflicto
                controller.enabled = false;
                transform.position = respawnPoint.position;
                controller.enabled = true;
            }
            else
            {
                // Si no se encuentra CharacterController, mover normalmente
                transform.position = respawnPoint.position;
            }
        }
    }
}