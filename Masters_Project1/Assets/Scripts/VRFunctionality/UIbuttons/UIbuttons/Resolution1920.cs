using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution1920 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Screen.SetResolution(1920, 1080, true);
            Debug.Log("1920");
        }
    }
}
