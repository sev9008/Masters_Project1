using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSortnext : MonoBehaviour, IPointerDownHandler
{
    public selsort_arrayholder m_selsort_arrayholder;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        m_selsort_arrayholder.next = true;
        if (m_selsort_arrayholder.previous)
        {
            m_selsort_arrayholder.previous = false;
        }
        m_selsort_arrayholder.Step.text = "Processing next Step";
    }
}
