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
    public Material Transparent;

    public bool OnTable;

    public bool isGrabbed;
    public float timer;
    public void Start()
    {
        OnTable = true;
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
            //PairedPos.GetComponent<MeshRenderer>().material = mat;
            PairedPos.GetComponent<MeshRenderer>().enabled = true;
        }
        if (!OnTable)
        {
            timer += Time.deltaTime;
            if (timer > 5f)
            {
                //PairedPos.GetComponent<MeshRenderer>().material = Transparent;
                PairedPos.GetComponent<MeshRenderer>().enabled = true;
                transform.position = PairedPos.transform.position;
            }
        }
        if (OnTable)
        {
            timer = 0;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Table" && collision.gameObject.tag != "Moveable")
        {
            Debug.Log("notble");
            OnTable= false;
        }        
        if (collision.gameObject.tag == "Table" || collision.gameObject.tag == "Moveable")
        {
            Debug.Log("notble");
            OnTable = true;
        }
    }
}
