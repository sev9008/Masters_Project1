using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class mergesortpause : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MergeSort_arrHolder M_MergeSort_arrHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_MergeSort_arrHolder.paused)
        {
            M_MergeSort_arrHolder.paused = false;
            txt.text = "Pause";
        }

        else if (!M_MergeSort_arrHolder.paused)
        {
            M_MergeSort_arrHolder.paused = true;
            txt.text = "Resume";
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
