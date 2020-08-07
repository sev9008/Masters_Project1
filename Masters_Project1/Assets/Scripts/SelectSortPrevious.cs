using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSortPrevious : MonoBehaviour, IPointerDownHandler
{
    public selsort_arrayholder m_selsort_arrayholder;
    // Start is called before the first frame update
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_selsort_arrayholder.previous == false && m_selsort_arrayholder.running)
        {
            m_selsort_arrayholder.previous = true;
            if (m_selsort_arrayholder.next)
            {
                m_selsort_arrayholder.next = false;
            }
            m_selsort_arrayholder.Step.text = "Processing previous Step";
        }
    }
}
