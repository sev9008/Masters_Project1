using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class playselsort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public selsort_arrayholder M_selsort_Arrayholder;

    private bool pressed;
    void Start()
    {
        pressed = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressed)
        {
            pressed = false;

        }
        else if (!pressed)
        {
            pressed = true;
           // M_selsort_Arrayholder.StartCoroutine(M_selsort_Arrayholder.Selection());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
