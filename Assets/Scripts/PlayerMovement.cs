using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]public float moveSpeed = 5f; 
    [SerializeField]public float currentMoveSpeed; 
    public float jumpHeight = 2f; 
    public float gravity = -9f; 
    public Transform groundCheck; 
    public float groundDistance = 0.1f; 
    public LayerMask groundMask; 
    public float changeTime;
    public float changeTimer=0.5f;

    private Animator _animator; 


    private CharacterController characterController;
    [SerializeField]private Vector3 velocity; 
    private bool isGrounded; 
    
    public float ReducirVelocidad = 2.5f; 


    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Awake()
    {
        _animator = GetComponent<Animator>(); 
    }


    void Update()
    {
        

        MovePlayer();
        Gravity(); 

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

      // Metodo para aplicar el rebote
    public void Bounce(float bounceForce)
    {
        velocity.y = bounceForce; // Establece la velocidad vertical al valor de rebote
        
    }

    

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
            _animator.SetBool("IsJumping", false); 

        }

        if(!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }



    void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
            _animator.SetBool("IsJumping", true); 
        }
    }



    void MovePlayer()
    {
    
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(0, 0, horizontalInput);

        float currentMoveSpeed = isGrounded ? moveSpeed : ReducirVelocidad;

        // if(!isGrounded) currentMoveSpeed=ReducirVelocidad;
        // else currentMoveSpeed=moveSpeed;

        characterController.Move((move * currentMoveSpeed + velocity) * Time.deltaTime);


          // Rotacion del personaje
        if (horizontalInput < 0) // Si se mueve hacia atras
        {
        transform.rotation = Quaternion.Euler(0, 180, 0); // Rotar 180 en el eje Y
        _animator.SetBool("IsRunning", true); 
        }
        
        if (horizontalInput > 0) // Si se mueve hacia adelante
        {
        transform.rotation = Quaternion.Euler(0, 0, 0); // Mantener la rotacion original
        _animator.SetBool("IsRunning", true); 
        }

        else if (horizontalInput == 0)
        {
        _animator.SetBool("IsRunning", false); 
        }

        }



      
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawCube(groundCheck.position, new Vector3(groundDistance * 2, groundDistance * 2, groundDistance * 2));
        }
    }




}




