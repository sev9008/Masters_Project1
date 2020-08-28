using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartBubbleGraphAni : MonoBehaviour, IPointerDownHandler
{
    public BubbleSortAni thirdDselectionani;
    public List<int> arr1;
    public int size;
    public ArrayKeeper arrayKeeper;
    Coroutine co;
    Coroutine sho;

    public bool running;
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
        arr1.Clear();
        for (int i = 0; i < size; i++)
        {
            arr1.Add(arrayKeeper.arr[i]);
        }

        thirdDselectionani.ShowGraph(arr1);
        if (!running)
        {
            running = true;
            co = StartCoroutine(startani());
        }
    }
    public IEnumerator startani()
    {
        yield return sho = StartCoroutine(thirdDselectionani.Bubble(arr1));
        running = false;
    }
}