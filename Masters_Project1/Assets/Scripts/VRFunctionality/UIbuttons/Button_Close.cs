using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This script will disable a canvas when the button is pressed
/// it is not in use
/// </summary>
public class Button_Close : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Canvas;

    public void OnPointerDown(PointerEventData eventData)
    {
        Canvas.SetActive(false);
        //Destroy(Canvas);
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
