﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pressed1 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InputField Text;

    public void OnPointerDown(PointerEventData eventData)
    {
        string txt = Text.text;
        string tmptxt = txt;
        txt = tmptxt + "1";
        Text.text = txt;
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
