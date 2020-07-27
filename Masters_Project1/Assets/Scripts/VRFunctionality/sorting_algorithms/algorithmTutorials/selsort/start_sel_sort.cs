using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class start_sel_sort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject selsortcan;

    public void OnPointerDown(PointerEventData eventData)
    {
        selsortcan.GetComponentInChildren<selsort_arrayholder>().arr = arrayKeeper.arr;
        selsortcan.GetComponentInChildren<selsort_arrayholder>().size = arrayKeeper.size;
        selsortcan.GetComponentInChildren<selsort_arrayholder>().Display();
    }
    public void OnPointerUp(PointerEventData eventData)
    { }
}
