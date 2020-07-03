using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_NewMenu1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject CanvasObject;
    public GameObject spawnpos;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 newpos = spawnpos.transform.position;
        newpos.y -= .8f;
        Instantiate(CanvasObject, newpos, spawnpos.transform.rotation);
    }

    public void OnPointerUp(PointerEventData eventData)
    {    }



}
