using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayMergeSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public MergeSort_arrHolder M_mergeSort_ArrHolder;

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
            M_mergeSort_ArrHolder.StartCoroutine(M_mergeSort_ArrHolder.IEMerge());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
