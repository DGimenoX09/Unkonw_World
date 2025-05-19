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
            // Si toca la zona de muerte, reaparece en el último checkpoint
            RespawnPlayer();
        }
    }

    // Nueva función para que otros scripts puedan forzar el respawn del jugador
    public void RespawnPlayer()
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false; // Desactivamos el CharacterController para mover al jugador
            player.transform.position = respawnPoint + Vector3.up * 0.5f; // Respawn ligeramente arriba
            cc.enabled = true; // Reactivamos el CharacterController
        }
    }
}
