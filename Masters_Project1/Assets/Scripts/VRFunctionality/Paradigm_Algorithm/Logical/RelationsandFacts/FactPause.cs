﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FactPause : MonoBehaviour, IPointerDownHandler
{
    public FactController abstractController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (abstractController.manual)
        {
            abstractController.manual = false;
            txt.text = "Resume";
        }

        else if (!abstractController.manual)
        {
            abstractController.manual = true;
            txt.text = "Pause";
        }
    }
}
