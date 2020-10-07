using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseInsertSort : MonoBehaviour, IPointerDownHandler
{
    public InsertSort_arrayHolder M_InsertSort_arrayHolder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_InsertSort_arrayHolder.manual)
        {
            M_InsertSort_arrayHolder.manual = false;
            txt.text = "Auto";
        }

        else if (!M_InsertSort_arrayHolder.manual)
        {
            M_InsertSort_arrayHolder.manual = true;
            txt.text = "Resume";
        }
    }
}