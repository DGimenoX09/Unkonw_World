using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX; // Necesario para trabajar con efectos visuales

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 respawnPoint;
    [SerializeField] VisualEffect visualEffect; // Referencia al VisualEffect

    void Start()
    {
        // Guardamos la posición inicial como punto de respawn inicial
        respawnPoint = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            // Si toca un checkpoint, actualizamos el punto de respawn
            respawnPoint = other.transform.position;

            // Activamos el efecto visual cuando el jugador toca un checkpoint
            if (visualEffect != null)
            {
                visualEffect.Play(); // Reproducimos el efecto visual
            }
        }
        else if (other.CompareTag("Destruir"))
        {
            // Marcar como no vivo antes de respawnear
            CambioCarril cambioCarril = player.GetComponent<CambioCarril>();
            if (cambioCarril != null)
            {
                cambioCarril.isAlive = false;
            }

            // Respawn en el último checkpoint
            RespawnPlayer();
        }
    }

    public void RespawnPlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false; // Desactivamos el CharacterController para mover al jugador
            player.transform.position = respawnPoint + Vector3.up * 0.5f; // Respawn ligeramente arriba
            cc.enabled = true; // Reactivamos el CharacterController
        }

        // Resetear carril y volver a activar
        CambioCarril cambioCarril = player.GetComponent<CambioCarril>();
        if (cambioCarril != null)
        {
            cambioCarril.ResetToMainLane(); // Mover al carril principal y reiniciar target
            cambioCarril.isAlive = true;    // Reactivar el control
        }
    }
}
