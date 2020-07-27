using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSelectiongraphani : MonoBehaviour, IPointerDownHandler
{
    public SelectionAni m_selectionAni;
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(m_selectionAni.SelectSort(m_selectionAni.arr));
    }
}
