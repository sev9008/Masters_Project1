using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_pause : MonoBehaviour, IPointerDownHandler
{
    public selsort_arrayholder M_selsort_Arrayholder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_selsort_Arrayholder.manual)
        {
            M_selsort_Arrayholder.manual = false;
            txt.text = "Auto";
        }

        else if (!M_selsort_Arrayholder.manual)
        {
            M_selsort_Arrayholder.manual = true;
            txt.text = "Manual";
        }
    }
}