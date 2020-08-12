using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    bool correctvalue;

    public void OnPointerDown(PointerEventData eventData)
    {
        obj.transform.parent = Dot.transform;
        down = true;
        pointer.defaultLength = 0f;
        obj.transform.position = pointer.transform.position;
        obj.transform.position = obj.transform.position + (pointer.transform.forward * .1f);//new Vector3(pointer.transform.position.x - .1f, pointer.transform.position.y, pointer.transform.position.z + .1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        obj.transform.parent = parent.transform;
        down = false;
        pointer.defaultLength = 7f;
        slesortInteractive.updatePos();
        slesortInteractive.arrow.SetActive(false);
    }
    public void Start()
    {
        Dot = GameObject.FindWithTag("DotCanR");
    }

    private void Update()
    {
        if (down)
        {
            slesortInteractive.arrow.SetActive(true);
            oldpos = obj.transform.position;
            obj.transform.rotation = new Quaternion(0,0,0,0);
        }

        if (!down){ }

        if (correctvalue && !down)
        {
            //slesortInteractive.Step.text = "Incorrect.  The block you attempted to swap was not the smallest value in the unsorted array.";
            //correctvalue = false;
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


            float.TryParse(this.GetComponentInChildren<Text>().text, out float thisvalue);
            if (thisvalue == slesortInteractive.currentSmallest)
            {
                correctvalue = true;
                try
                {
                    slesortInteractive.SwapValues(tempnum2 - 1, tempnum - 1);
                }
                catch { }
            }
            else 
            {
                slesortInteractive.Step.text = "Incorrect.  The block you attempted to swap was not the smallest value in the unsorted array.";
                slesortInteractive.incorretAnswers += 1;
                slesortInteractive.incorretAnswersText.GetComponent<Text>().text = slesortInteractive.incorretAnswers.ToString();

            }
            slesortInteractive.arrow.SetActive(false);
        }
    }
}
