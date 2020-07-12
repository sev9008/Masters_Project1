using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonevent_parent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Canvas; 
    public GameObject Panel; 
    public GameObject Dot;
    private bool down;
    private Vector3 oldpos;
    private bool downtest;

    public void OnPointerDown(PointerEventData eventData)
    {
        Panel.transform.parent = Dot.transform;
        down = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        down = false;
    }
    public void Start()
    {
        Dot = GameObject.FindWithTag("DotCan");
    }

    private void Update()
    {
        if (down)
        {
            oldpos = Panel.transform.position;
            downtest = true;
        }
        if (!down && downtest)
        {
            Panel.transform.SetParent(Canvas.transform, true);
            //Debug.Log(oldpos);
            downtest = false;
        }
    }
}
