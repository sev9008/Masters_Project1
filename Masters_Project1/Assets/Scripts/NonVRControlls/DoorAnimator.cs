using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
        {
            animator.SetTrigger("Open");
            animator.ResetTrigger("Close");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoor"))
        {
            animator.SetTrigger("Close");
            animator.ResetTrigger("Open");
        }
    }
}
