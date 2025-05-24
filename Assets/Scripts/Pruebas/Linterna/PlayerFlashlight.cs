using System.Collections;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public float stunRange = 5f;
    public LayerMask enemyLayer;
    public Transform rayOrigin;

    [SerializeField] private GameObject _linterna;

    private Animator _animator;
    private CharacterController _characterController;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Solo permite usar la linterna si el personaje está en el suelo
        if (Input.GetButtonDown("Flash") && _characterController.isGrounded)
        {
            ActivarLinterna();
        }
    }

    private void ActivarLinterna()
    {
        RaycastHit hit;

        _animator.SetTrigger("IsFlashing");

        _linterna.SetActive(true);
        StartCoroutine(DesactivarLinternaConRetraso(0.7f));

        Vector3 direction = transform.forward;

        if (Physics.Raycast(rayOrigin.position, direction, out hit, stunRange, enemyLayer))
        {
            EnemigoPatrulla enemy = hit.collider.GetComponent<EnemigoPatrulla>();
            if (enemy != null)
            {
                enemy.Stun(3f); // Aturdir por 3 segundos
            }
        }
    }

    IEnumerator DesactivarLinternaConRetraso(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        _linterna.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        if (rayOrigin == null)
            rayOrigin = transform;

        Gizmos.color = Color.yellow;

        Vector3 direction = transform.forward;

        Gizmos.DrawLine(rayOrigin.position, rayOrigin.position + direction * stunRange);
        Gizmos.DrawSphere(rayOrigin.position + direction * stunRange, 0.2f);
    }
}
