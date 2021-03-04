using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// WHen pressed this script will decrement the slide show controller
/// </summary>
public class SlideBack_Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SlideShowController M_SlideShowController;

    public void OnPointerDown(PointerEventData eventData)
    {
        M_SlideShowController.PrevSlide();
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}