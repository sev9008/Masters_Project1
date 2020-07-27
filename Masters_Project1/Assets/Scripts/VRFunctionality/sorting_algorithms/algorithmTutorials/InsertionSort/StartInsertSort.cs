using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartInsertSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject insertsortcan;

    public void OnPointerDown(PointerEventData eventData)
    {
        insertsortcan.GetComponentInChildren<InsertSort_arrayHolder>().arr = arrayKeeper.arr;
        insertsortcan.GetComponentInChildren<InsertSort_arrayHolder>().size = arrayKeeper.size;
        insertsortcan.GetComponentInChildren<InsertSort_arrayHolder>().Display();
    }
    public void OnPointerUp(PointerEventData eventData)
    { }
}
