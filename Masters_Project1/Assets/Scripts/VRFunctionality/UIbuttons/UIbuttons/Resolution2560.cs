using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution2560 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Screen.SetResolution(2560, 1440, true);
            Debug.Log("2560");
        }
    }
}
