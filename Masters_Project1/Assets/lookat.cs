using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.rotation = Quaternion.LookRotation(transform.position - Player.transform.position);
    }
}
