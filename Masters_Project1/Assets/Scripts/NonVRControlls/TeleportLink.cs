using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script can be placed on a gameobject with a trigger collider.
/// When the player enters the collider, the player will be teleported to the TeleportTo position
/// </summary>
public class TeleportLink : MonoBehaviour
{
    public Transform TeleportTo;//references the linked portal the player will teleport to.
    public GameObject Player;//reference the main player in the scene.
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
            Player.transform.rotation = TeleportTo.rotation;
        }
    }
}
