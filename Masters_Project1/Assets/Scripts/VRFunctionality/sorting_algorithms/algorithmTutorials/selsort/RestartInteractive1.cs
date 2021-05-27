using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartInteractive1 : MonoBehaviour, IPointerDownHandler
{
    public SlesortInteractive1 slesortInteractive1;
    public void OnPointerDown(PointerEventData eventData)
    {
        slesortInteractive1.Begin();
    }
}
