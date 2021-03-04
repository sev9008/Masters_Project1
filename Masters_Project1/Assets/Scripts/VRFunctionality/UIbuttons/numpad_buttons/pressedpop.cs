using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pressedpop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ArrayKeeper arrayKeeper;

    public void OnPointerDown(PointerEventData eventData)
    {
        arrayKeeper.pop();
    }

    public void OnPointerUp(PointerEventData eventData)
    { }
}