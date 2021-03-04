using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This button was used to spawn new UI panels at the palyer's controller position
/// It is no longer in use
/// </summary>
public class Button_NewMenu1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject PanelObject;
    public GameObject spawnpos;

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 newpos = spawnpos.transform.position;
        PanelObject.transform.position = newpos;
        spawnpos.SetActive(false);
        PanelObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {    }
}
