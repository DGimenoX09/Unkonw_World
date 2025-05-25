using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // <- Importar TMPro

public class PlayerFlashlight : MonoBehaviour
{
    public float stunRange = 5f;
    public LayerMask enemyLayer;
    public Transform rayOrigin;
    public float verticalOffset = 0f;

    [SerializeField] private GameObject _linterna;
    [SerializeField] private AudioSource flashSound;
    [SerializeField] private ParticleSystem flashParticles;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private Image cooldownUI;
    [SerializeField] private TextMeshProUGUI cooldownText; // <- NUEVO: texto del cooldown

    private Animator _animator;
    private CharacterController _characterController;

    private bool canFlash = true;
    private float cooldownTimer = 0f;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canFlash && Input.GetButtonDown("Flash") && _characterController.isGrounded)
        {
            ActivarLinterna();
        }

        if (!canFlash)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownUI != null)
                cooldownUI.fillAmount = 1 - (cooldownTimer / cooldown);

            if (cooldownText != null)
                cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            if (cooldownTimer <= 0f)
            {
                canFlash = true;
                if (cooldownUI != null)
                    cooldownUI.fillAmount = 0;
                if (cooldownText != null)
                    cooldownText.text = ""; // Vaciar cuando está listo
            }
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
            flashParticles.Play();

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

        canFlash = false;
        cooldownTimer = cooldown;
        if (cooldownUI != null)
            cooldownUI.fillAmount = 1;
        if (cooldownText != null)
            cooldownText.text = cooldown.ToString("F0"); // Mostrar valor inicial
    }

    IEnumerator DesactivarLinternaConRetraso(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        _linterna.SetActive(false);
    }
}
