using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartQuickSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject quicksortcan;

    public void OnPointerDown(PointerEventData eventData)
    {
        quicksortcan.GetComponentInChildren<QuickSort_arrayHolder>().arr = arrayKeeper.arr;
        quicksortcan.GetComponentInChildren<QuickSort_arrayHolder>().size = arrayKeeper.size;
        quicksortcan.GetComponentInChildren<QuickSort_arrayHolder>().Display();
    }
    public void OnPointerUp(PointerEventData eventData)
    { }
}