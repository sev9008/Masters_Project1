using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Play_BubbleSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BubbleSort_arrayHolder M_bubbleSort_ArrayHolder;

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
            //M_bubbleSort_ArrayHolder.StartCoroutine(M_bubbleSort_ArrayHolder.Bubble(M_bubbleSort_ArrayHolder.arr));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}