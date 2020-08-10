using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParent : MonoBehaviour
{
    public GameObject parent;
    public bool gravity;
    public Rigidbody rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        if (!gravity)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
