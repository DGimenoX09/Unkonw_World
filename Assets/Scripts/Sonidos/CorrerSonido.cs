using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrerSonido : MonoBehaviour
{
    public AudioSource footstepsAudio;
    public LayerMask groundLayer;
    public float speedThreshold = 0.1f;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isMoving = Mathf.Abs(controller.velocity.x) > speedThreshold;
        bool isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (isMoving && isGrounded)
        {
            if (!footstepsAudio.isPlaying)
                footstepsAudio.Play();
                Debug.Log("Reproduciendo sonido");
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