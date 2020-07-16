using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause_BubbleSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BubbleSort_arrayHolder M_bubbleSort_ArrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_bubbleSort_ArrayHolder.paused)
        {
            M_bubbleSort_ArrayHolder.paused = false;
            txt.text = "Pause";
        }

        else if (!M_bubbleSort_ArrayHolder.paused)
        {
            M_bubbleSort_ArrayHolder.paused = true;
            txt.text = "Resume";
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
