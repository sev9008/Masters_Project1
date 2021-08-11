﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartQuickInteractive1 : MonoBehaviour, IPointerDownHandler
{
    public QuickSortInteractive1 slesortInteractive1;
    public Text txtTest;
    public Text txtGuide;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slesortInteractive1.IsTestMode == true)
        {
            slesortInteractive1.IsTestMode = false;
        }
        txtGuide.text = "Restart";
        txtTest.text = "Start Test";
        slesortInteractive1.Begin();
    }


}
