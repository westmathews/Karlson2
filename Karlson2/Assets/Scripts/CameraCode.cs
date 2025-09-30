using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode: MonoBehaviour
{
    public Camera maincam;
    [SerializeField] private float mouseSensitivity = 500f;
    public float xRotation = 0f;
    public Transform player;
   
    
    void Start()
    {
        player = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        

        if (!GetComponentInParent<General>().paused)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * mouseX);
        }
        
    }
}
