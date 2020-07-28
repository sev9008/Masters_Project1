using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class thirdDselectionani : MonoBehaviour
{
    public GameObject box;
    private RectTransform graphContainer;
    public SelectionAni m_selectionAni;

    public List<int> arr;
    public int size;

    public float speed;

    public Slider slider;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        size = 30;

        //StartCoroutine(SelectSort(arr));
    }

    public void Update()
    {
        speed = slider.value;
        if (arr.Count == 0)
        {
            arr = m_selectionAni.arr;
            ShowGraph();
            Debug.Log("hit");
        }
    }

    private void CreateCircle(Vector2 anchoredPosition)
    {
        //GameObject gameObject = new GameObject("circle", typeof(GameObject));
        GameObject gameObject = Instantiate(box);
        gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(10, 10);
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
        float xsize = 17f;
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
