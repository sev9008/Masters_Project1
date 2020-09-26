using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdvanceMerge : MonoBehaviour, IPointerDownHandler
{
    public MergeSortRecursionTutorial m_InsertSort_arrayHolder;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("hit");
        if (m_InsertSort_arrayHolder.next == false && m_InsertSort_arrayHolder.running)
        {
            Debug.Log("hit2");
            m_InsertSort_arrayHolder.next = true;
            if (m_InsertSort_arrayHolder.previous)
            {
                m_InsertSort_arrayHolder.previous = false;
            }
        }
    }
}
