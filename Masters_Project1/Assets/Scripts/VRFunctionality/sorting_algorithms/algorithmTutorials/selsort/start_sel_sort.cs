using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class start_sel_sort : MonoBehaviour, IPointerDownHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject selsortcan;
    public List<int> arr;
    public int size;

    public bool running;

    public selsort_arrayholder m_selsort_Arrayholder;

    public void Update()
    {
        Debug.Log(running);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        arr = arrayKeeper.arr;
        size = arrayKeeper.size;
        m_selsort_Arrayholder.Display(arr, size);

        if (!running)
        {
            StartCoroutine(startani());
            running = true;
        }
    }
    public IEnumerator startani()
    {
        yield return StartCoroutine(m_selsort_Arrayholder.Selection(arr, size));

        running = false;
    }
}
