using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// This button was used to close a UI panel.  
/// It is no longer in use
/// </summary>
public class ButtonClose_delete : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Canvas;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Canvas.SetActive(false);
        Destroy(Canvas);
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}
