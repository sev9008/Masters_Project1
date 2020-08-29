using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortAni : MonoBehaviour
{
    public GameObject box;
    private RectTransform graphContainer;

    public float speed;

    public Slider slider;
    private int pi;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
    }

    public void Update()
    {
        speed = slider.value;
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = Instantiate(box);
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(10, 10);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    public void ShowGraph(List<int> arr)
    {
        foreach (Transform child in graphContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xsize = 17f;
        for (int i = 0; i < arr.Count; i++)
        {
            float xPosition = xsize + i * xsize;
            float yPosition = (arr[i] / yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition));
        }
    }


    public IEnumerator Quick(List<int> arr3)
    {
        yield return StartCoroutine(quickSort(arr3, 0, arr3.Count - 1));
    }
    public IEnumerator quickSort(List<int> arr, int l, int h)
    {
        if (l < h)
        {
            yield return StartCoroutine(partition(arr, l, h));
            yield return StartCoroutine(quickSort(arr, l, pi - 1));
            yield return StartCoroutine(quickSort(arr, pi + 1, h));
        }
    }

    public IEnumerator partition(List<int> arr, int l, int h)
    {
        int pivot = arr[h];
        int i = (l - 1);
        for (int j = l; j <= h-1; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                ShowGraph(arr);
                yield return new WaitForSeconds(speed);
            }
        }
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[h];
        arr[h] = temp1;
        pi = i + 1;
        ShowGraph(arr);
        yield return new WaitForSeconds(speed);
    }
}