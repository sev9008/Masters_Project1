using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartQuickRecursion : MonoBehaviour
{
    public QuickSortRecursionTutorial slesortInteractive1;
    public void OnPointerDown(PointerEventData eventData)
    {
        slesortInteractive1.Begin();
    }
}
