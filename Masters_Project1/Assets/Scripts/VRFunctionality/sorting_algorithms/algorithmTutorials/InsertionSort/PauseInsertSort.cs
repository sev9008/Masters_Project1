using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseInsertSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InsertSort_arrayHolder M_InsertSort_arrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_InsertSort_arrayHolder.paused)
        {
            M_InsertSort_arrayHolder.paused = false;
            txt.text = "Pause";
        }

        else if (!M_InsertSort_arrayHolder.paused)
        {
            M_InsertSort_arrayHolder.paused = true;
            txt.text = "Resume";
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
}