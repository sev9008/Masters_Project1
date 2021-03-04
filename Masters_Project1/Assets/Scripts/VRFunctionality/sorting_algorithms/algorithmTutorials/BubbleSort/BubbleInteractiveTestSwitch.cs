using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BubbleInteractiveTestSwitch : MonoBehaviour, IPointerDownHandler
{
    public BubbleInteractive1 slesortInteractive1;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slesortInteractive1.IsTestMode == false)
        {
            slesortInteractive1.IsTestMode = true;
            txt.text = "End Test";
        }
        else if (slesortInteractive1.IsTestMode == true)
        {
            slesortInteractive1.IsTestMode = false;
            txt.text = "Start Test";
        }
        slesortInteractive1.Begin();
    }
}
