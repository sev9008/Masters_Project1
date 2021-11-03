using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMergeSort : MonoBehaviour, IPointerDownHandler
{
    public MergeSort_arrHolder M_quickSort_arrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_quickSort_arrayHolder.paused)
        {
            M_quickSort_arrayHolder.manual = false;
            M_quickSort_arrayHolder.paused = false;
            txt.text = "Pause";
        }
        else if (!M_quickSort_arrayHolder.paused)
        {
            M_quickSort_arrayHolder.manual = true;
            M_quickSort_arrayHolder.paused = true;
            txt.text = "Resume";
        }
    }
}