using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayQuickSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public QuickSort_arrayHolder M_quickSort_arrayHolder;

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
            M_quickSort_arrayHolder.StartCoroutine(M_quickSort_arrayHolder.Quick());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
