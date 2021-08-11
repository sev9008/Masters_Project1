using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// old graph function.  nnot used anymore
/// </summary>
public class BubbleSortAni : MonoBehaviour
{
    public GameObject box;
    private RectTransform graphContainer;

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

    public IEnumerator Bubble(List<int> arr3)
    {
        int i, j;
        for (i = 0; i < arr3.Count - 1; i++)
        {
            for (j = 0; j < arr3.Count - i - 1; j++)
            {
                if (arr3[j] > arr3[j + 1])
                {
                    int temp = arr3[j];
                    arr3[j] = arr3[j + 1];
                    arr3[j + 1] = temp;
                    ShowGraph(arr3);
                    yield return new WaitForSeconds(speed);
                }
            }
        }
        ShowGraph(arr3);
        yield return new WaitForSeconds(speed);
    }
}
