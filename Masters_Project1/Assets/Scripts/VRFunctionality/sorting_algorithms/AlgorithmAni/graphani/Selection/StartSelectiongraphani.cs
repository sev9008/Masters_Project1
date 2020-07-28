using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartSelectiongraphani : MonoBehaviour, IPointerDownHandler
{
    public thirdDselectionani thirdDselectionani;
    public List<int> arr;
    public ArrayKeeper arrayKeeper;

    public bool running;
    public void OnPointerDown(PointerEventData eventData)
    {
        List<int> arr = arrayKeeper.arr;
        thirdDselectionani.ShowGraph(arr);
        if (!running)
        {
            running = true;
        }
    }
    public IEnumerator startani()
    {
        yield return StartCoroutine(thirdDselectionani.SelectSort(arr));
        running = false;
    }

}
