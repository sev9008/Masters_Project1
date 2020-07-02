using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Close : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Canvas;

    public void OnPointerDown(PointerEventData eventData)
    {
        Canvas.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
