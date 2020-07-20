using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideNext_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SlideShowController M_SlideShowController;
    public void OnPointerDown(PointerEventData eventData)
    {
        M_SlideShowController.NextSlide();
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}