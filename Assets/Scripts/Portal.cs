using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal destino")]
    public Transform destino;  // El otro portal

    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que sea el jugador el que entra
        if (other.CompareTag("Player"))
        {
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
            {
                // Desactiva temporalmente el CharacterController para evitar glitches
                cc.enabled = false;
                other.transform.position = destino.position;
                cc.enabled = true;
            }
            else
            {
                // Si no usas CharacterController, simplemente cambia la posición
                other.transform.position = destino.position;
            }
        }
    }
}
