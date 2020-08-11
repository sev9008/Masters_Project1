using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParent : MonoBehaviour
{
    public GameObject parent;
    public bool gravity;
    private Rigidbody rb;
    public GameObject txtpos;
    public GameObject PairedPos;
    public Material mat;

    public bool isGrabbed;
    public void Start()
    {
        gravity = true;
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
        txtpos.transform.position = this.transform.position + (Vector3.up * .3f);

        if (isGrabbed && this.GetComponent<BoxCollider>().enabled == true)
        {
            PairedPos.GetComponent<MeshRenderer>().material = mat;
            PairedPos.GetComponent<MeshRenderer>().enabled = true;
        }

    }
}
