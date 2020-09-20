using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdvanceMergeRecursion : MonoBehaviour, IPointerDownHandler
{
    public MergeSortRecursionTutorial m_InsertSort_arrayHolder;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_InsertSort_arrayHolder.next == false && m_InsertSort_arrayHolder.running)
        {
            m_InsertSort_arrayHolder.next = true;
            if (m_InsertSort_arrayHolder.previous)
            {
                m_InsertSort_arrayHolder.previous = false;
            }
        }
    }
}
