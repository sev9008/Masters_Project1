using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pause_BubbleSort : MonoBehaviour, IPointerDownHandler
{
    public BubbleSort_arrayHolder M_quickSort_arrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_quickSort_arrayHolder.paused)
        {
            M_quickSort_arrayHolder.paused = false;
            M_quickSort_arrayHolder.manual = false;
            txt.text = "Pause";
        }

        else if (!M_quickSort_arrayHolder.paused)
        {
            M_quickSort_arrayHolder.paused = true;
            M_quickSort_arrayHolder.manual = true;
            txt.text = "Resume";
        }
    }
}