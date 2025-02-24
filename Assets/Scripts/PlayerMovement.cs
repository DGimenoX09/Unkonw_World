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


    private CharacterController characterController;
    [SerializeField]private Vector3 velocity; 
    private bool isGrounded; 
    

    public GameObject escenario1; 
    public GameObject escenario2; 

    private GameObject escenarioActual;

    public float ReducirVelocidad = 2.5f; 


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        escenarioActual = escenario1;
        // escenario1.SetActive(true);
        // escenario2.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            CambiarEscenario();
            return;
        }
        
        /*if (Input.GetKeyDown(KeyCode.J)&& changeTime<0 )
        {
            CambiarEscenario();
            changeTime=changeTimer;
            return;
        }
        changeTime-=Time.deltaTime;*/

        MovePlayer();
        Gravity(); 

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }



    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
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


    }



      
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawCube(groundCheck.position, new Vector3(groundDistance * 2, groundDistance * 2, groundDistance * 2));
        }
    }



    void CambiarEscenario()
    {   
        float teleportOffset;

        if(escenarioActual == escenario1)
        {
            teleportOffset = 61;
            escenarioActual = escenario2;
        }
        else
        {
            teleportOffset = -59;
            escenarioActual = escenario1;
        }
        
        transform.position = new Vector3(transform.position.x, transform.position.y + teleportOffset, transform.position.z);

            /*if (escenarioActual == escenario1)
            {
                // escenario1.SetActive(false);
                // escenario2.SetActive(true);
                escenarioActual = escenario2;

                transform.position = new Vector3(transform.position.x, transform.position.y + 61f, transform.position.z);
            }
            else
            {
                // escenario2.SetActive(false);
                // escenario1.SetActive(true);
                escenarioActual = escenario1;

                transform.position = new Vector3(transform.position.x, transform.position.y - 59f, transform.position.z);
            }*/
      
    }


}




