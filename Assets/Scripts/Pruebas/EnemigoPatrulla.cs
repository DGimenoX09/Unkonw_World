using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{
    public Transform puntoIzquierda; 
    public Transform puntoDerecha;   
    public float velocidad = 2f;     

    private Vector3 objetivo;         
    private bool moviendoDerecha = true; 
    private CharacterController characterController;
    private bool atacando = false; 

    public float distance;
    private float tiempoSinContacto = 0f; 
    public float tiempoParaReanudar = 1.5f; 

    [Header("Ajustes de Ataque")]
    public float rangoAtaque = 1.5f; // Distancia de ataque
    private Transform jugador; 

    void Start()
    {
        objetivo = puntoDerecha.position;
        characterController = GetComponent<CharacterController>();

        // Encuentra al jugador por la etiqueta
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
        }
    }

    void Update()
    {
        if (jugador != null)
        {
            float distanciaAlJugador = Vector3.Distance(transform.position, jugador.position);

            if (distanciaAlJugador <= rangoAtaque)
            {
                atacando = true;
                tiempoSinContacto = 0f; 
                Debug.Log("Atacando al jugador dentro del rango.");
            }
            else
            {
                if (atacando)
                {
                    tiempoSinContacto += Time.deltaTime;
                    if (tiempoSinContacto >= tiempoParaReanudar)
                    {
                        atacando = false; // Reanuda el movimiento
                        Debug.Log("Reanudando patrulla despues de esperar.");
                    }
                }
            }
        }

        if (!atacando)
        {
            // Movimiento normal
            Vector3 direccion = (objetivo - transform.position).normalized;
            direccion.y = 0; 
            characterController.Move(direccion * velocidad * Time.deltaTime);

            distance = Vector3.Distance(transform.position, objetivo);

            if (distance <= 0.7f)
            {
                objetivo = moviendoDerecha ? puntoIzquierda.position : puntoDerecha.position;
                moviendoDerecha = !moviendoDerecha;
            }
        }
    }

    public void Stun()
{
    Debug.Log("¡Enemigo aturdido!");
    // Aqui puedes agregar logica para desactivar el movimiento temporalmente
}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
        {
            atacando = true; 
            tiempoSinContacto = 0f; 
            Debug.Log("Atacando al jugador (colision detectada).");
        }
    }

    // Dibuja un circulo en la escena para visualizar el rango de ataque
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Color del circulo
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}