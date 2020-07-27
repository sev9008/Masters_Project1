using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_pause : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public selsort_arrayholder M_selsort_Arrayholder;
    public Text txt;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (M_selsort_Arrayholder.paused)
        {
            M_selsort_Arrayholder.paused = false;
            txt.text = "Pause";
        }

        else if (!M_selsort_Arrayholder.paused)
        {
            M_selsort_Arrayholder.paused = true;
            txt.text = "Resume";
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    { 
    
    }
}