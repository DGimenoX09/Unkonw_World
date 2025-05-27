using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    public Transform respawnPoint; // Se actualiza al pisar un nuevo checkpoint

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destruir"))
        {
            Debug.Log("Has muerto");

            // Contar la muerte
            if (DeathManager.Instance != null)
                DeathManager.Instance.AddDeath();

            // Respawnear al jugador
            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                transform.position = respawnPoint.position;
                controller.enabled = true;
            }
            else
            {
                transform.position = respawnPoint.position;
                Debug.Log("In Respawn");
            }
        }
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
}
