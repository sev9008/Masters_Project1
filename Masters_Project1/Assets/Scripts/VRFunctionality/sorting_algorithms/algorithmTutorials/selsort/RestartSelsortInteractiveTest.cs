using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartSelsortInteractiveTest : MonoBehaviour, IPointerDownHandler
{
    public SlesortInteractive1 slesortInteractive1;
    public Text txtTest;
    public Text txtGuide;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slesortInteractive1.IsTestMode == false)
        {
            slesortInteractive1.IsTestMode = true;
        }
        txtTest.text = "Restart";
        txtGuide.text = "Start Guide";
        slesortInteractive1.Begin();
    }
}
