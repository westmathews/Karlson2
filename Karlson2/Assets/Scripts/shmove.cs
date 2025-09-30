using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shmove : MonoBehaviour
{
    //Refrences
    private CharacterController controller;

    //Variables
    [SerializeField] private float speed;
    [SerializeField] private float sprint;
    [SerializeField] private float walk;
    [SerializeField] private float ySpeed = 0f;
    [SerializeField] private bool isGrounded;
    private float moveX;
    private float moveZ;
    public float gravity = -9.81f;
    public float jumpVelocity;
    Vector3 move;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            ySpeed = -1f;
        }
        else
        {
            ySpeed += gravity * Time.deltaTime;
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            ySpeed = jumpVelocity; // Calculate jump speed
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprint;
        }
        else
        {
            speed = walk;
        }

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        move = transform.right * moveX + transform.forward * moveZ;

        if (moveX != 0 && moveZ != 0)
        {
            controller.Move(move * (speed * 1.0f) * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);
        }
        controller.Move(move * speed * Time.deltaTime + new Vector3(0, ySpeed, 0) * Time.deltaTime);

    }
}
