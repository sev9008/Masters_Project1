using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseQuickSort : MonoBehaviour, IPointerDownHandler
{
    public QuickSort_arrayHolder M_quickSort_arrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_quickSort_arrayHolder.manual)
        {
            M_quickSort_arrayHolder.manual = false;
            txt.text = "Pause";
        }

        else if (!M_quickSort_arrayHolder.manual)
        {
            M_quickSort_arrayHolder.manual = true;
            txt.text = "Resume";
        }
    }
}