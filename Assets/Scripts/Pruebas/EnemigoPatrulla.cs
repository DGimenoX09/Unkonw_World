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

    private Animator _animator; 

    private bool aturdido = false; // Estado interno
    private float tiempoAturdido = 0f;


    public float distance;
    private float tiempoSinContacto = 0f; 
    public float tiempoParaReanudar = 0.5f; 

    [Header("Ajustes de Ataque")]
    public Transform puntoAtaque; // Empty para definir el centro del ataque
    public Vector3 rangoAtaqueXYZ = new Vector3(1.5f, 1.5f, 1.5f); // Rango en cada eje
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

    void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (aturdido)
        {
            _animator.SetBool("IsCegado",true);
            tiempoAturdido -= Time.deltaTime;
            if (tiempoAturdido <= 0f)
            {
                aturdido = false;
                _animator.SetBool("IsCegado",false);
                Debug.Log("Enemigo ya no está aturdido");
            }
            return; // Salir del Update si está aturdido
        }
        
        if (jugador != null)
        {
            Vector3 distanciaRelativa = jugador.position - puntoAtaque.position;
            bool dentroDelRango = Mathf.Abs(distanciaRelativa.x) <= rangoAtaqueXYZ.x &&
                                  Mathf.Abs(distanciaRelativa.y) <= rangoAtaqueXYZ.y &&
                                  Mathf.Abs(distanciaRelativa.z) <= rangoAtaqueXYZ.z;

            if (dentroDelRango)
            {
                atacando = true;
                tiempoSinContacto = 0f; 
                Debug.Log("Atacando al jugador dentro del rango.");
                _animator.SetTrigger("IsAttacking"); 
                
                  // Aqui llamamos a la funcion que hace dano
                HacerDanioAlJugador();
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
            _animator.SetBool("IsWalking", true); 
            characterController.Move(direccion * velocidad * Time.deltaTime);

            distance = Vector3.Distance(transform.position, objetivo);

            if (distance <= 0.7f)
            {
                objetivo = moviendoDerecha ? puntoIzquierda.position : puntoDerecha.position;
                moviendoDerecha = !moviendoDerecha;

                // Rotar el enemigo dependiendo de la direccion
                transform.rotation = moviendoDerecha ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
            }
        }
        
        
    }

    public void Stun(float duration)
    {
    if (!aturdido)
    {
        aturdido = true;
        tiempoAturdido = duration;
        atacando = false; // Detener ataque si estaba atacando
        _animator.SetBool("IsWalking", false);
        _animator.SetTrigger("IsStunned"); // Si tienes una animación de aturdimiento
        Debug.Log("Enemigo aturdido por " + duration + " segundos");
    }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Player"))
        {
            atacando = true; 
            tiempoSinContacto = 0f; 
            Debug.Log("Atacando al jugador (colisión detectada).");
        }
    }

    private void HacerDanioAlJugador()
    {
    if (jugador != null)
    {
        PlayerHealth playerHealth = jugador.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1); // Le quitamos 1 punto de vida
        }
    }
    }


    // Dibuja un área de ataque en la escena para visualizar el rango en X, Y y Z
    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque != null)
        {
            Gizmos.color = Color.red; // Color del área de ataque
            Gizmos.DrawWireCube(puntoAtaque.position, rangoAtaqueXYZ * 2);
        }
    }
}