using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLink : MonoBehaviour
{
    public Transform TeleportTo;
    public GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("hit");
            Player.transform.position = TeleportTo.position;
        }
    }
}
