using UnityEngine;

public class PlayerDestroyer : MonoBehaviour
{
    public Transform respawnPoint; // Se actualizara al pisar un nuevo checkpoint

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destruir"))
        {
            Debug.Log("Has muerto");

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
            }
        }
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
}
