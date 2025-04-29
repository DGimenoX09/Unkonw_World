using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDestroyer : MonoBehaviour
{
    // Este metodo se llama cuando el collider del jugador entra en contacto con otro collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona tiene la etiqueta "Destruir"
        if (other.CompareTag("Destruir"))
        {
            Debug.Log("Has muerto"); // Mensaje de depuracion
            SceneManager.LoadScene("MenuDeMuerte"); // Asegurate de que el nombre de la escena sea correcto
        }
    }
} 




     