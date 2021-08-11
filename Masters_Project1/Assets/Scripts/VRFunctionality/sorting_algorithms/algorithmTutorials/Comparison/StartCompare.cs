using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartCompare : MonoBehaviour, IPointerDownHandler
{
    public SortAniCompareV2 selsortInteractive2;
    public void OnPointerDown(PointerEventData eventData)
    {
        selsortInteractive2.Begin();
        this.GetComponentInChildren<Text>().text = "Restart";
    }
}
