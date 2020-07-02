using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonevent_parent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Canvas; //Drag your breaklight here
    public GameObject Dot;
    private bool down;
    private Vector3 oldpos;
    private bool downtest;

    public void OnPointerDown(PointerEventData eventData)
    {
        Canvas.transform.parent = Dot.transform;
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        down = false;
    }
    public void Start()
    {
        oldpos = transform.position;
        downtest = true;
    }


    private void Update()
    {
        if (down)
        {
            oldpos = Canvas.transform.position;
            downtest = true;
        }
        if (!down && downtest)
        {
            Canvas.transform.parent = null;
            Canvas.transform.position = oldpos;
            Debug.Log(oldpos);
            downtest = false;
        }
    }
}
