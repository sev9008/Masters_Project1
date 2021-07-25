using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    //public Animation Ani;
    public Animator animator;
    public bool Open;
    public bool Close;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            Debug.Log("IsOpen");
            animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            Debug.Log("CLose");
            animator.SetTrigger("Close");
        }
    }
}
