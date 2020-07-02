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
    }


    private void Update()
    {
        if (down)
        {
            oldpos = transform.position;
        }
        if (!down)
        {
            Canvas.transform.parent = null;
            Canvas.transform.position = oldpos;
            Debug.Log(oldpos);
        }
    }
}
