using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// gonna be honest, no clue what this is.  Its not used so i wont emss with it.  deleting it might not ahve an effect???
/// </summary>
public class Start_BubbleSort : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject bubblesortcan;

    public void OnPointerDown(PointerEventData eventData)
    {
        //bubblesortcan.GetComponentInChildren<BubbleSort_arrayHolder>().arr = arrayKeeper.arr;
        //bubblesortcan.GetComponentInChildren<BubbleSort_arrayHolder>().size = arrayKeeper.size;
        //bubblesortcan.GetComponentInChildren<BubbleSort_arrayHolder>().Display();
    }
    public void OnPointerUp(PointerEventData eventData)
    { }
}
