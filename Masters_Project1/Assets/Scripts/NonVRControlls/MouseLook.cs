using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Script is used for a non vr version of the game.  
/// It currently cannot interact with any VR buttons
/// </summary>
public class MouseLook : MonoBehaviour
{
    float xRotation = 0f; 
    float MouseSensitivity = 100f;
    public Transform PlayerBody;
    public GameObject QuitOBj;

    void Start()
    {
        
    }

    void Update()
    {
        float mouseX = 0;
        float mouseY = 0;
        if (!QuitOBj.activeInHierarchy)
        {
            mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        }

        xRotation -= mouseY;

        //Prevents over rotation = looking backward
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate the body and camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
