using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayInsertSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InsertSort_arrayHolder M_InsertSort_arrayHolder;

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
            M_InsertSort_arrayHolder.StartCoroutine(M_InsertSort_arrayHolder.Insertion(M_InsertSort_arrayHolder.arr));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
