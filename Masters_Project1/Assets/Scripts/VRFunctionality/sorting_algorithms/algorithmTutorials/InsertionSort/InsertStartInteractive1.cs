using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InsertStartInteractive1 : MonoBehaviour, IPointerDownHandler
{
    public InsertSortInteractive1 slesortInteractive1;
    public void OnPointerDown(PointerEventData eventData)
    {
        slesortInteractive1.Begin();
    }
}
