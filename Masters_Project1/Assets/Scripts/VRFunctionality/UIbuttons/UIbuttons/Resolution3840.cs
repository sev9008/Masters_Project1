using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution3840 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Screen.SetResolution(3840, 2160, true);
            Debug.Log("3840");
        }
    }
}
