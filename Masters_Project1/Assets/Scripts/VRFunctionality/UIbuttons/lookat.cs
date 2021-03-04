using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script will have the UI turn to face the palyer.
/// It is rarely used in this project
/// </summary>
public class lookat : MonoBehaviour
{
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Camera");
    }

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
