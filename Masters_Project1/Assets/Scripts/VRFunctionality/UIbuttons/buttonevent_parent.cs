using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Not currently in use
/// 
/// Cant remember what this was used for.
/// I believe it was used originally to parent game objects to the vr controller
/// </summary>
public class buttonevent_parent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject parent; 
    public GameObject obj; 
    public GameObject Dot;
    private bool down;
    private Vector3 oldpos;
    private bool downtest;

    public void OnPointerDown(PointerEventData eventData)
    {
        obj.transform.parent = Dot.transform;
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
            oldpos = obj.transform.position;
            downtest = true;
        }
        if (!down && downtest)
        {
            obj.transform.SetParent(parent.transform, true);
            //Debug.Log(oldpos);
            downtest = false;
        }
    }
}
