using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class SelectionAni : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;

    public List<int> arr;
    public int size;

    public float speed;

    public Slider slider;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        size = 30;
        for (int i = 0; i < size; i++)
        {
            arr.Add(Random.Range(0, 100));
        }
        ShowGraph();

        //StartCoroutine(SelectSort(arr));
    }

    public void Update()
    {
        speed = slider.value;
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
    }

    private void ShowGraph()
    {
        foreach (Transform child in graphContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = 100f;
        float xsize = 10f;
        for (int i = 0; i < arr.Count; i++)
        {
            float xPosition = xsize + i * xsize;
            float yPosition = (arr[i] / yMaximum) * graphHeight;
            CreateCircle(new Vector2(xPosition, yPosition));
        }
    }

    public IEnumerator SelectSort()
    {
        int i, j;
        int iMin;
        for (i = 0; i < size - 1; i++)
        {
            iMin = i;
            for (j = i + 1; j < size; j++)
            {
                if (arr[j] < arr[iMin])
                {
                    iMin = j;
                }
            }
            if (iMin != i)
            {
                int temp = arr[i];
                arr[i] = arr[iMin];
                arr[iMin] = temp;
                ShowGraph();
                yield return new WaitForSeconds(speed);
            }
        }
        ShowGraph();
    }
}
