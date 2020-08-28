using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortAni : MonoBehaviour
{
    public GameObject box;
    private RectTransform graphContainer;

    //public List<int> arr;
    //public int size;

    public float speed;

    public Slider slider;

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

    public IEnumerator IEMerge(List<int> arr3)
    {
        yield return StartCoroutine(MergeSort(arr3, 0, arr3.Count - 1));
    }

    public IEnumerator MergeSort(List<int> arr, int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;

            yield return StartCoroutine(MergeSort(arr, l, m));

            yield return StartCoroutine(MergeSort(arr, m + 1, r));

            yield return StartCoroutine(merge(arr, l, m, r));
        }
    }

    public IEnumerator merge(List<int> arr, int l, int m, int r)
    {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        int[] L = new int[n1];
        int[] R = new int[n2];

        for (i = 0; i < n1; i++)
        {
            L[i] = arr[l + i];
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = arr[m + 1 + j];
        }
        i = 0;
        j = 0;
        k = l;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
            k++;

            ShowGraph(arr);
            yield return new WaitForSeconds(speed);
        }
        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;

            ShowGraph(arr);
            yield return new WaitForSeconds(speed);
        }
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;

            ShowGraph(arr);
            yield return new WaitForSeconds(speed);
        }
    }
}
