using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    CharacterController charController;
    float Gravity = -9.81f;
    bool Grounded;
    Vector3 Velocity;
    float GroundDistance = 0.4f;

    public float MouseSensitivity = 100f;
    public float speed = 10f;
    public Transform PlayerBody;
    //public Transform weapon;
    float xRotation = 0f;
    float yRotation = 0f;

    Quaternion rotation;


    public LayerMask GroundMask;
    public Transform GroundCheck;
    public float MoveSpeed = 5;


    void Start()
    {
        cam = Camera.main;
        charController = GetComponent<CharacterController>();
        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        Grounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore);
        if (!Grounded)
        {
            Grounded = charController.isGrounded;
        }
        Movement();
        //ThirdPersonCameraLookDirection();

        //Time.deltaTime prevents framerate based speed
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        yRotation -= mouseX;
        //Prevents over rotation = looking backward
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Rotate the body and camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, -yRotation, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {
        if (!Grounded)
        {
            Velocity += Vector3.up * Gravity  * Time.deltaTime; // increases fall gravity for better feel
        }
        else
        {
            Velocity = Vector3.up * 0;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //transform.right and transform.forward uses local coords instead of world coords
        Vector3 move = transform.right * x + transform.forward * z;

        //Debug.Log(Velocity);
        charController.Move(Velocity * Time.deltaTime);
        charController.Move(move * MoveSpeed * Time.deltaTime);
    }
}
