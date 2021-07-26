using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution1280 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Screen.SetResolution(1280, 720, false);
            Debug.Log("1280");
        }
    }
}
