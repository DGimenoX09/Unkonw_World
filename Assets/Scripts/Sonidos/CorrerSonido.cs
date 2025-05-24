using UnityEngine;

public class CorrerSonido : MonoBehaviour
{
    public AudioSource footstepsAudio;
    public LayerMask groundLayer;
    public float speedThreshold = 0.1f;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool isMoving = Mathf.Abs(horizontal) > speedThreshold;
        bool isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (isMoving && isGrounded)
        {
            if (!footstepsAudio.isPlaying)
            {
                footstepsAudio.Play();
                Debug.Log("Reproduciendo sonido");
            }
        }
        else
        {
            if (footstepsAudio.isPlaying)
                footstepsAudio.Stop();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
