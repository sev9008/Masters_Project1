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
                //animator.SetTrigger("Close");
                //animator.ResetTrigger("Open");
                animator.SetBool("OpenBool", false);
                animator.SetBool("CloseBool", true);
                //Debug.Log("Close");
            }
        }

        if (animator.GetBool("OpenBool") == true && animator.GetBool("CloseBool") == true)
        {
            animator.SetBool("OpenBool", false);
            animator.SetBool("CloseBool", false);
            //Debug.Log("Reset animation");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor"))
        {
            //animator.SetTrigger("Open");
            //animator.ResetTrigger("Close");
            animator.SetBool("OpenBool", true);
            animator.SetBool("CloseBool", false);
            IsOpen = true;
            //Debug.Log("Opentrigger");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoor"))
        {
            //animator.SetTrigger("Close");
            //animator.ResetTrigger("Open");
            animator.SetBool("OpenBool", false);
            animator.SetBool("CloseBool", true);
            IsOpen = false;
            //Debug.Log("CLoseTrigger");
        }
    }
}
