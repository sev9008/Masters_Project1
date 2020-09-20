using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMergeRecusion : MonoBehaviour, IPointerDownHandler
{
    public MergeSortRecursionTutorial M_quickSort_arrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_quickSort_arrayHolder.manual)
        {
            M_quickSort_arrayHolder.manual = false;
            M_quickSort_arrayHolder.paused = false;
            txt.text = "Auto";
        }
        else if (!M_quickSort_arrayHolder.manual)
        {
            M_quickSort_arrayHolder.manual = true;
            M_quickSort_arrayHolder.paused = true;
            txt.text = "Manual";
        }
    }
}