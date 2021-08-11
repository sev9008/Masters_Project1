using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// previous button for the array holder script
/// </summary>
public class PreviousBubble : MonoBehaviour, IPointerDownHandler
{
    public BubbleSort_arrayHolder m_InsertSort_arrayHolder;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_InsertSort_arrayHolder.previous == false && m_InsertSort_arrayHolder.running)
        {
            m_InsertSort_arrayHolder.previous = true;
            if (m_InsertSort_arrayHolder.next)
            {
                m_InsertSort_arrayHolder.next = false;
            }
            m_InsertSort_arrayHolder.Step.text = "Processing previous Step";
        }
    }
}