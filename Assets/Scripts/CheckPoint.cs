using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] Vector3 respawnPoint;

    void Start()
    {
        // Guardamos la posicion inicial como punto de respawn inicial
        respawnPoint = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
    {
        respawnPoint = other.transform.position;
    }
    else if (other.CompareTag("Destruir"))
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.transform.position = respawnPoint + Vector3.up * 0.5f;
            cc.enabled = true;
        }
    }
    }
}
