using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMergeRcursion : MonoBehaviour, IPointerDownHandler
{
    public MergeSortRecursionTutorial slesortInteractive1;
    public void OnPointerDown(PointerEventData eventData)
    {
        slesortInteractive1.Begin();
    }
}

