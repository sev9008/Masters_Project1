using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InsertNext : MonoBehaviour, IPointerDownHandler
{
    public InsertSort_arrayHolder m_InsertSort_arrayHolder;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_InsertSort_arrayHolder.next == false && m_InsertSort_arrayHolder.running)
        {
            m_InsertSort_arrayHolder.next = true;
            if (m_InsertSort_arrayHolder.previous)
            {
                m_InsertSort_arrayHolder.previous = false;
            }
            m_InsertSort_arrayHolder.Step.text = "Processing next Step";
        }
    }
}
