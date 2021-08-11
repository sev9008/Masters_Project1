using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public GameObject Player;
    public GameObject spawnPoint;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, spawnPoint.transform.position) > 200)
        {
            Player.transform.position = spawnPoint.transform.position;
        }
    }

    //for some reason the below will randomly teleport the player.  I wonder if the trigger is being hit by the canvas attached to the player.  I can make the canvas smaller, but then you wont be able to see any ui attached to the player.
    //private void Start()
    //{
    //    Player = GameObject.FindGameObjectWithTag("Player");
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("riggerTP?");
    //    if (other.tag == "Player")
    //    {
    //        Player.transform.position = spawnPoint.transform.position;
    //    }

    //}
}
