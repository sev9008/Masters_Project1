using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;
    bool IsOpen;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (IsOpen)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) > 10)
            {
                animator.SetTrigger("Close");
                animator.ResetTrigger("Open");
                Debug.Log("Close");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
        {
            animator.SetTrigger("Open");
            animator.ResetTrigger("Close");
            IsOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoor"))
        {
            animator.SetTrigger("Close");
            animator.ResetTrigger("Open");
            IsOpen = false;
        }
    }
}
