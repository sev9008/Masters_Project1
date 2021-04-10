using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Animation Ani;
    private bool Open;
    private bool Close;

    void Update()
    {
        if (Open)
        {
            Ani.Play("Open");
            Open = false;
        }
        if (Close)
        {
            Ani.Play("Close");
            Close = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Open = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Close = true;
        }
    }
}
