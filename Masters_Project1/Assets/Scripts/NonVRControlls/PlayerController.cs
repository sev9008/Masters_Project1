using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script provides movement to a non vr  player.
/// It currently cannot interact with any VR buttons
/// </summary>
public class PlayerController : MonoBehaviour
{
    Camera cam;
    CharacterController charController;
    float Gravity = -9.81f;
    bool Grounded;
    Vector3 Velocity;
    float GroundDistance = 0.4f;

    public float speed = 10f;

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
        Cursor.lockState = CursorLockMode.Locked;


        Grounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore);
        if (!Grounded)
        {
            Grounded = charController.isGrounded;
        }
        Movement();
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
