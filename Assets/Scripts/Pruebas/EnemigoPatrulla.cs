using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{
    public Transform puntoIzquierda; // Asigna el punto izquierdo en el inspector
    public Transform puntoDerecha;   // Asigna el punto derecho en el inspector
    public float velocidad = 2f;     // Velocidad de patrullaje

    private Vector3 objetivo;         // El objetivo actual al que se mueve el enemigo
    private bool moviendoDerecha = true; // Estado de movimiento
    private CharacterController characterController;

    void Start()
    {
        // Inicializa el objetivo al punto de la derecha
        objetivo = puntoDerecha.position;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calcula la direccion hacia el objetivo
        Vector3 direccion = (objetivo - transform.position).normalized;

        // Mueve el enemigo hacia el objetivo
        characterController.Move(direccion * velocidad * Time.deltaTime);

        // Verifica si el enemigo ha llegado al objetivo
        if (Vector3.Distance(transform.position, objetivo) < 0.1f)
        {
            // Cambia el objetivo al punto opuesto
            if (moviendoDerecha)
            {
                objetivo = puntoIzquierda.position;
            }
            else
            {
                objetivo = puntoDerecha.position;
            }
            moviendoDerecha = !moviendoDerecha; // Cambia el estado de movimiento
            Debug.Log("Cambiando objetivo a: " + objetivo);
        }
    }
}