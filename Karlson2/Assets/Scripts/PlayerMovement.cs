using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Refrences
    private CharacterController controller;
    private Animator anim;

    //Variables
    [SerializeField] private float speed;
    [SerializeField] private float rSpeed;
    [SerializeField] private float wSpeed;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private float jumpHeight;
    private bool hasjumped = false;

    private Vector3 moveDirection;
    private Vector3 velocity;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if (moveDirection == Vector3.zero)
        {
            Idle();
        }

        moveDirection *= speed;

        if (isGrounded)
        {
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            anim.SetBool("Grounded", true);
            anim.SetFloat("Jump", 0f);

        }
        else
        {
            anim.SetBool("Grounded", false);
            anim.SetFloat("Jump", 0.5f, 0.1f, Time.deltaTime);
            
            
        }

        if(hasjumped && isGrounded)
        {
            anim.SetFloat("Jump", 1f);
            //anim.SetBool("hasJumped", false);

        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        speed = wSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        speed = rSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        //anim.SetBool("hasJumped", true);
        anim.SetTrigger("boing");
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        
            

        
    }
}
