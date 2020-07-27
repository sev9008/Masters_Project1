using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSelectiongraphani : MonoBehaviour, IPointerDownHandler
{
    public SelectionAni m_selectionAni;
    public thirdDselectionani m_thirdDselectionani;
    public bool running;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!running)
        {
            StartCoroutine(m_selectionAni.SelectSort());
            StartCoroutine(m_thirdDselectionani.SelectSort());
            running = true;
        }
    }
}
