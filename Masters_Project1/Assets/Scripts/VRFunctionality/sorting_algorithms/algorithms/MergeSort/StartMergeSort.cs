using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StartMergeSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject selsortcan;

    public void OnPointerDown(PointerEventData eventData)
    {
        selsortcan.GetComponentInChildren<MergeSort_arrHolder>().arr = arrayKeeper.arr;
        selsortcan.GetComponentInChildren<MergeSort_arrHolder>().size = arrayKeeper.size;
        selsortcan.GetComponentInChildren<MergeSort_arrHolder>().Display();
    }
    public void OnPointerUp(PointerEventData eventData)
    { }
}
