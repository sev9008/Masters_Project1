using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//switches interactive 1 between test mode and guindance mode
public class BubbleInteractiveTestSwitch : MonoBehaviour, IPointerDownHandler
{
    public BubbleInteractive1 slesortInteractive1;
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
