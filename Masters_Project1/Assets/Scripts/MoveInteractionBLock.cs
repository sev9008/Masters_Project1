using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveInteractionBLock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject parent;
    public GameObject obj;
    public GameObject Dot;
    public Pointer pointer;
    public SlesortInteractive1 slesortInteractive;
    private bool down;
    private Vector3 oldpos;
    private bool downtest;

    public bool hit;

    public void OnPointerDown(PointerEventData eventData)
    {
        obj.transform.parent = Dot.transform;
        down = true;
        pointer.defaultLength = 0f;
        obj.transform.position = pointer.transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        obj.transform.parent = parent.transform;
        down = false;
        pointer.defaultLength = 7f;
        slesortInteractive.updatePos();
    }
    public void Start()
    {
        Dot = GameObject.FindWithTag("DotCan");
    }

    private void Update()
    {
        if (down)
        {
            oldpos = obj.transform.position;
            //downtest = true;
        }
        if (!down) //&& downtest)
        {
            //obj.transform.parent = parent.transform;

            //obj.transform.SetParent(parent.transform, true);
            //downtest = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int tempnum, tempnum2;
        int.TryParse(other.gameObject.tag, out tempnum);
        int.TryParse(this.gameObject.tag, out tempnum2);

        if (tempnum > 0 && tempnum < 9)
        {
            Debug.Log("hit");

            obj.transform.parent = parent.transform;
            down = false;
            pointer.defaultLength = 7f;
            slesortInteractive.updatePos();
            try
            {
                slesortInteractive.SwapValues(tempnum2 - 1, tempnum - 1);
            }
            catch { }
        }



        //hit = true;
        //if (down && other.gameObject.tag == "1" || other.gameObject.tag == "2" || other.gameObject.tag == "3" || other.gameObject.tag == "4" || other.gameObject.tag == "5" || other.gameObject.tag == "6" || other.gameObject.tag == "7" || other.gameObject.tag == "8" || other.gameObject.tag == "9")
        //{
        //    if (other.transform.parent == parent.transform)
        //    {
        //        Debug.Log("hit");

        //        if (tempnum != tempnum2 && tempnum > 0 && tempnum <= 9 && tempnum2 > 0 && tempnum2 <= 9)
        //        {
        //        }
        //    }
        //}
    }
}
