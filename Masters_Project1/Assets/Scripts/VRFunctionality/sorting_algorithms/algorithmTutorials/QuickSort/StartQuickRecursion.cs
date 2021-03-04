using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartQuickRecursion : MonoBehaviour, IPointerDownHandler
{
    public QuickSortRecursion2 slesortInteractive1;
    public void OnPointerDown(PointerEventData eventData)
    {
        slesortInteractive1.Begin();
    }
}
