using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// this script is used to advance the Bubble sort Main animation
/// while it is paused this script will tell the animation to advance to the next step.
/// </summary>
public class AdvanceBubble : MonoBehaviour, IPointerDownHandler
{
    public BubbleSort_arrayHolder m_InsertSort_arrayHolder;

    //if the button is pressed tell arrayholder to proceed to the next step
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
