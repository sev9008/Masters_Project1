using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortAniCompare : MonoBehaviour
{
    public List<GameObject> selectionSortArr;
    public List<GameObject> insertionSortArr;
    public List<GameObject> bubbleSortArr;
    public List<GameObject> quickSortArr;
    public List<GameObject> mergeSortArr;

    public GameObject selectFinished;
    public GameObject insertFinished;
    public GameObject bubbleFinished;
    public GameObject quickFinished;
    public GameObject mergeFinished;

    public float speed;
    public Slider slider;

    int pi;

    void Start()
    {
        selectFinished.SetActive(false);
        insertFinished.SetActive(false);
        bubbleFinished.SetActive(false);
        quickFinished.SetActive(false);
        mergeFinished.SetActive(false);

        speed = 1f;
        Begin();
    }

    void Update()
    {
        //speed = slider.value;
        for (int x = 0; x < selectionSortArr.Count; x++)
        {
            float.TryParse(selectionSortArr[x].GetComponentInChildren<Text>().text, out float temp1);
            selectionSortArr[x].transform.localScale = new Vector3(selectionSortArr[x].transform.localScale.x, temp1/2, selectionSortArr[x].transform.localScale.z);
            
            float.TryParse(insertionSortArr[x].GetComponentInChildren<Text>().text, out float temp2);
            insertionSortArr[x].transform.localScale = new Vector3(insertionSortArr[x].transform.localScale.x, temp2/2, insertionSortArr[x].transform.localScale.z);

            float.TryParse(bubbleSortArr[x].GetComponentInChildren<Text>().text, out float temp3);
            bubbleSortArr[x].transform.localScale = new Vector3(bubbleSortArr[x].transform.localScale.x, temp3/2, bubbleSortArr[x].transform.localScale.z);

            float.TryParse(quickSortArr[x].GetComponentInChildren<Text>().text, out float temp4);
            quickSortArr[x].transform.localScale = new Vector3(quickSortArr[x].transform.localScale.x, temp4/2, quickSortArr[x].transform.localScale.z);

            float.TryParse(mergeSortArr[x].GetComponentInChildren<Text>().text, out float temp5);
            mergeSortArr[x].transform.localScale = new Vector3(mergeSortArr[x].transform.localScale.x, temp5/2, mergeSortArr[x].transform.localScale.z);
        }
    }

    public void Begin()
    {

        try
        {
            StopAllCoroutines();
        }
        catch { }

        selectFinished.SetActive(false);
        insertFinished.SetActive(false);
        bubbleFinished.SetActive(false);
        quickFinished.SetActive(false);
        mergeFinished.SetActive(false);

        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text c = selectionSortArr[i].GetComponentInChildren<Text>();
            Text b = insertionSortArr[i].GetComponentInChildren<Text>();
            Text a = bubbleSortArr[i].GetComponentInChildren<Text>();
            Text d = quickSortArr[i].GetComponentInChildren<Text>();
            Text e = mergeSortArr[i].GetComponentInChildren<Text>();

            a.text = n.ToString();
            b.text = n.ToString();
            c.text = n.ToString();
            d.text = n.ToString();
            e.text = n.ToString();
        }

        StartCoroutine(Bubble());
        StartCoroutine(Insertion());
        StartCoroutine(Selection());
        StartCoroutine(merge());
        StartCoroutine(quick());
    }

    public IEnumerator Selection()
    {
        int i, j, iMin;

        for (i = 0; i < selectionSortArr.Count - 1; i++)
        {
            iMin = i;
            for (j = i + 1; j < selectionSortArr.Count; j++)
            {
                float.TryParse(selectionSortArr[j].GetComponentInChildren<Text>().text, out float temp1);
                float.TryParse(selectionSortArr[iMin].GetComponentInChildren<Text>().text, out float temp2);

                if (temp1 < temp2)
                {
                    iMin = j;
                }
                yield return new WaitForSeconds(speed);
            }
            if (iMin != i)
            {
                float.TryParse(selectionSortArr[i].GetComponentInChildren<Text>().text, out float temp3);
                selectionSortArr[i].GetComponentInChildren<Text>().text = selectionSortArr[iMin].GetComponentInChildren<Text>().text;
                selectionSortArr[iMin].GetComponentInChildren<Text>().text = temp3.ToString();
            }
        }
        selectFinished.SetActive(true);
    }    
    public IEnumerator Bubble()
    {
        int i, j;
        for (i = 0; i < bubbleSortArr.Count - 1; i++)
        {
            for (j = 0; j < bubbleSortArr.Count - i - 1; j++)
            {
                float.TryParse(bubbleSortArr[j].GetComponentInChildren<Text>().text, out float temp1);
                float.TryParse(bubbleSortArr[j + 1].GetComponentInChildren<Text>().text, out float temp2);

                if (temp1 > temp2)
                {
                    float.TryParse(bubbleSortArr[j].GetComponentInChildren<Text>().text, out float temp3);
                    bubbleSortArr[j].GetComponentInChildren<Text>().text = bubbleSortArr[j + 1].GetComponentInChildren<Text>().text;
                    bubbleSortArr[j + 1].GetComponentInChildren<Text>().text = temp3.ToString();
                }
                yield return new WaitForSeconds(speed);
            }
        }
        bubbleFinished.SetActive(true);
    }

    public IEnumerator Insertion()
    {
        int i, key, j;
        for (i = 1; i < insertionSortArr.Count; i++)
        {
            key = int.Parse(insertionSortArr[i].GetComponentInChildren<Text>().text);
            j = i - 1;

            int tempval = int.Parse(insertionSortArr[j].GetComponentInChildren<Text>().text);
            while (j >= 0 && tempval > key)
            {
                insertionSortArr[j + 1].GetComponentInChildren<Text>().text = insertionSortArr[j].GetComponentInChildren<Text>().text;
                j = j - 1;

                if (j >= 0)
                {
                    tempval = int.Parse(insertionSortArr[j].GetComponentInChildren<Text>().text);
                }
                yield return new WaitForSeconds(speed);
            }
            insertionSortArr[j + 1].GetComponentInChildren<Text>().text = key.ToString();
        }
        insertFinished.SetActive(true);
    }

    public IEnumerator merge()
    {
        yield return StartCoroutine(mergeSort(0, mergeSortArr.Count - 1));
        mergeFinished.SetActive(true);
    }
    public IEnumerator mergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            yield return mergeSort(l, m);
            yield return mergeSort(m + 1, r);
            yield return mergePartition(l, m, r);
        }
    }

    public IEnumerator mergePartition(int l, int m, int r)
    {
        int n1 = m - l + 1;
        int n2 = r - m;

        float[] L = new float[n1];
        float[] R = new float[n2];

        for (int a = 0; a < n1; a++) 
        {
            float.TryParse(mergeSortArr[l + a].GetComponentInChildren<Text>().text, out float temp);
            L[a] = temp;
        }
        for (int b = 0; b < n2; b++)
        {
            float.TryParse(mergeSortArr[m + 1 + b].GetComponentInChildren<Text>().text, out float temp2);
            R[b] = temp2;
        }

        int i = 0;
        int j = 0;
        int k = l;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                mergeSortArr[k].GetComponentInChildren<Text>().text = L[i].ToString();
                i++;
            }
            else
            {
                mergeSortArr[k].GetComponentInChildren<Text>().text = R[j].ToString();
                j++;
            }
            k++;
            yield return new WaitForSeconds(speed);
        }

        while (i < n1)
        {
            mergeSortArr[k].GetComponentInChildren<Text>().text = L[i].ToString();
            i++;
            k++;
            yield return new WaitForSeconds(speed);
        }

        while (j < n2)
        {
            mergeSortArr[k].GetComponentInChildren<Text>().text = R[j].ToString();
            j++;
            k++;
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator quick()
    {
        yield return StartCoroutine(quickSort(0, quickSortArr.Count - 1));
        quickFinished.SetActive(true);
    }
    public IEnumerator quickSort(int l, int h)
    {
        if (l < h)
        {
            pi = h;
            yield return StartCoroutine(quickPartition(l, h));
            yield return StartCoroutine(quickSort(l, pi - 1));
            yield return StartCoroutine(quickSort(pi + 1, h));
        }
    }

    public IEnumerator quickPartition(int l, int h)
    {
        float.TryParse(quickSortArr[h].GetComponentInChildren<Text>().text, out float pivot);
        int i = (l - 1);
        for (int j = l; j <= h - 1; j++)
        {
            float.TryParse(quickSortArr[j].GetComponentInChildren<Text>().text, out float temp);
            if (temp < pivot)
            {
                i++;
                float.TryParse(quickSortArr[i].GetComponentInChildren<Text>().text, out float temp2);
                quickSortArr[i].GetComponentInChildren<Text>().text = quickSortArr[j].GetComponentInChildren<Text>().text;
                quickSortArr[j].GetComponentInChildren<Text>().text = temp2.ToString();
            }
            yield return new WaitForSeconds(speed);
        }

        float.TryParse(quickSortArr[i + 1].GetComponentInChildren<Text>().text, out float temp1);
        quickSortArr[i + 1].GetComponentInChildren<Text>().text = quickSortArr[h].GetComponentInChildren<Text>().text;
        quickSortArr[h].GetComponentInChildren<Text>().text = temp1.ToString();

        pi = i + 1;
    }
}
