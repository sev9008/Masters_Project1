using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution1600 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Screen.SetResolution(1600, 900, false);
            Debug.Log("1600");
        }
    }
}
