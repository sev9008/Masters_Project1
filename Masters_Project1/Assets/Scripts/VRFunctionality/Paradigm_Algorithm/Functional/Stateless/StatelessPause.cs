﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatelessPause : MonoBehaviour, IPointerDownHandler
{
    public StatelessController abstractController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (abstractController.manual)
        {
            abstractController.manual = false;
            txt.text = "Pause";
        }

        else if (!abstractController.manual)
        {
            abstractController.manual = true;
            txt.text = "Resume";
        }
    }
}