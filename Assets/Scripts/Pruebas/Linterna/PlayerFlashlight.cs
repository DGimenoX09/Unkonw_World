using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public float stunRange = 5f;
    public LayerMask enemyLayer;
    public Transform rayOrigin;

    private Animator _animator; 

    void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            // _animator.SetBool("IsFlashing", true);
            _animator.SetTrigger("IsFlashing");
            _linterna.SetActive() //TODO

            Vector3 direction = transform.forward; // El rayo apunta en la dirección Z (hacia adelante y hacia atrás del jugador)
            
            if (Physics.Raycast(rayOrigin.position, direction, out hit, stunRange, enemyLayer))
            {
                EnemigoPatrulla enemy = hit.collider.GetComponent<EnemigoPatrulla>();
                if (enemy != null)
                {
                    enemy.Stun(3f); // Aturdir por 3 segundos
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rayOrigin == null) 
            rayOrigin = transform; // Aseguramos que el rayo salga desde el objeto si no se asignó uno específico.

        Gizmos.color = Color.yellow; // Establecemos el color del Gizmo

        // Dirección del rayo (en el eje Z)
        Vector3 direction = transform.forward; // El rayo apunta en la dirección Z

        // Dibujamos una línea que representa la dirección del rayo de la linterna
        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + direction * stunRange);

        // Si deseas agregar más detalles, puedes dibujar una esfera en el extremo del rayo
        Gizmos.DrawSphere(rayOrigin.position + direction * stunRange, 0.2f);
    }
}
