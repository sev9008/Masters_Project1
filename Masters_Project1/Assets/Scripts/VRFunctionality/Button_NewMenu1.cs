﻿using System;
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
        Debug.Log(spawnpos.transform.position);
        GameObject newob = Instantiate(CanvasObject, newpos, spawnpos.transform.rotation);
        newob.transform.position = newpos;
        //Destroy(spawnpos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {    }
}
