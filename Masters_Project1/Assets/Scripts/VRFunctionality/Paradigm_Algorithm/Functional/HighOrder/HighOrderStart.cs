using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighOrderStart : MonoBehaviour, IPointerDownHandler
{
    public HighOrderController abstractController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (abstractController.begin)
        {
            abstractController.begin = false;
            txt.text = "Start";
        }

        else if (!abstractController.begin)
        {
            abstractController.begin = true;
            txt.text = "Stop";
        }
    }
}