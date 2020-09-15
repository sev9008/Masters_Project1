using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StartMergeSort : MonoBehaviour, IPointerDownHandler
{
    public ArrayKeeper arrayKeeper;
    public GameObject insertsortcan;
    public List<int> arr2;
    public int size;
    public bool running;

    public MergeSort_arrHolder m_insertSort_ArrayHolder;
    Coroutine co;
    Coroutine sho;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (co != null || sho != null)
        {
            StopCoroutine(co);
            StopCoroutine(sho);
            sho = null;
            co = null;
            running = false;
        }
        size = arrayKeeper.size;
        if (!arr2.Equals(null))
        {
            arr2.Clear();
        }
        for (int i = 0; i < size; i++)
        {
            arr2.Add(arrayKeeper.arr[i]);
        }
        m_insertSort_ArrayHolder.Display(arr2, size, -1, -1, -1, -1, -1);

        if (!running)
        {
            co = StartCoroutine(startani());
            running = true;
        }
    }
    public IEnumerator startani()
    {
        m_insertSort_ArrayHolder.arr3.Clear();
        for (int i = 0; i < arr2.Count; i++)
        {
            m_insertSort_ArrayHolder.arr3.Add(arr2[i]);
        }
        yield return sho = StartCoroutine(m_insertSort_ArrayHolder.IEMerge());

        running = false;
    }
}