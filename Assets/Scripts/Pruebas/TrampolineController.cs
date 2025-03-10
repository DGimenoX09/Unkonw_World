using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // Fuerza del rebote

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                // Llama al metodo Bounce del script de movimiento del jugador
                playerMovement.Bounce(bounceForce);
            }
        }
    }
}