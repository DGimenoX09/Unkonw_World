using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform puntoA; // Asigna el Punto A en el Inspector
    public Transform puntoB; // Asigna el Punto B en el Inspector
    public float velocidad = 2f; // Velocidad de movimiento
    public float distanciaDeCambio = 0.1f; // Distancia para cambiar de punto
    private Vector3 objetivo; // El objetivo actual al que se mueve
    private bool moviendoHaciaA = true; // Indica hacia qué punto se está moviendo
    private CharacterController characterController; // Referencia al CharacterController

    void Start()
    {
        // Inicializa el CharacterController
        characterController = GetComponent<CharacterController>();
        
        // Inicializa el objetivo al Punto A
        objetivo = puntoA.position;
    }

    void Update()
    {
        // Calcula la dirección hacia el objetivo
        Vector3 direccion = (objetivo - transform.position).normalized;

        // Mueve el enemigo hacia el objetivo
        characterController.Move(direccion * velocidad * Time.deltaTime);

        // Verifica si ha llegado al objetivo
        if (Vector3.Distance(transform.position, objetivo) < distanciaDeCambio)
        {
            // Cambia el objetivo
            if (moviendoHaciaA)
            {
                objetivo = puntoB.position;
            }
            else
            {
                objetivo = puntoA.position;
            }
            moviendoHaciaA = !moviendoHaciaA; // Cambia la dirección
        }
    }
}