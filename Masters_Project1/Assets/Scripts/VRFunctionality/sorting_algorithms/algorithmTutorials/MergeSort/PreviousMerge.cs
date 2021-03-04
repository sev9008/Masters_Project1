using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PreviousMerge : MonoBehaviour, IPointerDownHandler
{
    public MergeSort_arrHolder m_InsertSort_arrayHolder;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_InsertSort_arrayHolder.previous == false && m_InsertSort_arrayHolder.running)
        {
            m_InsertSort_arrayHolder.previous = true;
            if (m_InsertSort_arrayHolder.next)
            {
                m_InsertSort_arrayHolder.next = false;
            }
        }
    }
}