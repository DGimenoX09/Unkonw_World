using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 3f;
    private Rigidbody rb;
    private Collider col;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        if (col != null)
        {
            col.isTrigger = true; // Al inicio es trigger para detectar la entrada del jugador
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detectado con: " + other.name);

        if (!isFalling && other.CompareTag("Player"))
        {
            Debug.Log("El jugador pisó la plataforma (trigger)");
            StartCoroutine(FallAfterDelay());
        }
    }

    IEnumerator FallAfterDelay()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelay);

        Debug.Log("¡La plataforma ahora debería caer!");

        if (col != null)
        {
            col.isTrigger = false; // Desactivamos trigger para que actúe como collider físico
        }

        if (rb != null)
        {
            rb.isKinematic = false; // Activamos la física para que caiga
        }
    }
}
