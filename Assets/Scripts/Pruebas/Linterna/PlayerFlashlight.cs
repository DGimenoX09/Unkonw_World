using System.Collections;
using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    public float stunRange = 5f;
    public LayerMask enemyLayer;
    public Transform rayOrigin;
    public float verticalOffset = 0f;

    [SerializeField] private GameObject _linterna;
    [SerializeField] private AudioSource flashSound;
    [SerializeField] private ParticleSystem flashParticles; // <-- NUEVO

    private Animator _animator;
    private CharacterController _characterController;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
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

        if (flashSound != null)
            flashSound.Play();

        if (flashParticles != null)
            flashParticles.Play(); // <-- NUEVO: reproducir sistema de partículas

        Vector3 originPosition = rayOrigin.position + new Vector3(0f, verticalOffset, 0f);
        Vector3 direction = transform.forward;

        if (Physics.Raycast(originPosition, direction, out hit, stunRange, enemyLayer))
        {
            EnemigoPatrulla enemy = hit.collider.GetComponent<EnemigoPatrulla>();
            if (enemy != null)
            {
                enemy.Stun(3f);
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

        Vector3 originPosition = rayOrigin.position + new Vector3(0f, verticalOffset, 0f);
        Vector3 direction = transform.forward;

        Gizmos.DrawLine(originPosition, originPosition + direction * stunRange);
        Gizmos.DrawSphere(originPosition + direction * stunRange, 0.2f);
    }
}
