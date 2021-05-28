using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortAniCompareV2 : MonoBehaviour
{
    //the arrays that hold the blocks and their values
    public List<GameObject> selectionSortArr;
    public List<GameObject> insertionSortArr;
    public List<GameObject> bubbleSortArr;
    public List<GameObject> quickSortArr;
    public List<GameObject> mergeSortArr;

    //the paretns of the blocks
    public GameObject SelectHolder;
    public GameObject InsertHolder;
    public GameObject BubbleHolder;
    public GameObject QuickHolder;
    public GameObject MergeHolder;

    //these are gamobject that will be displayed when the algorithm has finished running
    public GameObject selectFinished;
    public GameObject insertFinished;
    public GameObject bubbleFinished;
    public GameObject quickFinished;
    public GameObject mergeFinished;

    //controls the speed of the animation, and the number of numbers in the array
    public float speed;
    public int NumberOfNums;
    public Slider sliderSpeed;
    public Slider sliderNum;

    //these bools are for our toggle buttons.  they control the starting state of the array
    public Toggle RandomToggle;
    public Toggle DescendingToggle;
    public Toggle AscendingToggle;

    public GameObject SortingAniCubeHolder;

    int pi;

    void Start()
    {
        selectFinished.SetActive(false);
        insertFinished.SetActive(false);
        bubbleFinished.SetActive(false);
        quickFinished.SetActive(false);
        mergeFinished.SetActive(false);

        speed = sliderSpeed.value;
        NumberOfNums = (int)sliderNum.value;
    }

    private void Update()
    {
        speed = sliderSpeed.value;

        //set the position and scale of the obj
        for (int x = 0; x < selectionSortArr.Count; x++)
        {
            float.TryParse(selectionSortArr[x].GetComponentInChildren<Text>().text, out float temp1);
            selectionSortArr[x].transform.localScale = new Vector3(.2f, temp1 / 2, selectionSortArr[x].transform.localScale.z);
            selectionSortArr[x].transform.localPosition = new Vector3(selectionSortArr[x].transform.position.x + (x * (450/NumberOfNums)) + 80, selectionSortArr[x].transform.position.y, selectionSortArr[x].transform.position.z - 6);

            float.TryParse(insertionSortArr[x].GetComponentInChildren<Text>().text, out temp1);
            insertionSortArr[x].transform.localScale = new Vector3(.2f, temp1 / 2, insertionSortArr[x].transform.localScale.z);
            insertionSortArr[x].transform.localPosition = new Vector3(insertionSortArr[x].transform.position.x + (x * (450 / NumberOfNums)) + 80, insertionSortArr[x].transform.position.y, insertionSortArr[x].transform.position.z - 6);

            float.TryParse(bubbleSortArr[x].GetComponentInChildren<Text>().text, out temp1);
            bubbleSortArr[x].transform.localScale = new Vector3(.2f, temp1 / 2, bubbleSortArr[x].transform.localScale.z);
            bubbleSortArr[x].transform.localPosition = new Vector3(bubbleSortArr[x].transform.position.x + (x * (450 / NumberOfNums)) + 80, bubbleSortArr[x].transform.position.y, bubbleSortArr[x].transform.position.z - 6);

            float.TryParse(quickSortArr[x].GetComponentInChildren<Text>().text, out temp1);
            quickSortArr[x].transform.localScale = new Vector3(.2f, temp1 / 2, quickSortArr[x].transform.localScale.z);
            quickSortArr[x].transform.localPosition = new Vector3(quickSortArr[x].transform.position.x + (x * (450 / NumberOfNums)) + 80, quickSortArr[x].transform.position.y, quickSortArr[x].transform.position.z - 6);

            float.TryParse(mergeSortArr[x].GetComponentInChildren<Text>().text, out temp1);
            mergeSortArr[x].transform.localScale = new Vector3(.2f, temp1 / 2, mergeSortArr[x].transform.localScale.z);
            mergeSortArr[x].transform.localPosition = new Vector3(mergeSortArr[x].transform.position.x + (x * (450 / NumberOfNums)) + 80, mergeSortArr[x].transform.position.y, mergeSortArr[x].transform.position.z - 6);
        }
    }

    public void Begin()//start all coroutines and begin runnin each algorithm simultaneously
    {
        StopAllCoroutines();

        NumberOfNums = (int)sliderNum.value;

        selectFinished.SetActive(false);
        insertFinished.SetActive(false);
        bubbleFinished.SetActive(false);
        quickFinished.SetActive(false);
        mergeFinished.SetActive(false);

        foreach (Transform child in SelectHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        selectionSortArr.Clear();

        foreach (Transform child in InsertHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        insertionSortArr.Clear();

        foreach (Transform child in BubbleHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        bubbleSortArr.Clear();

        foreach (Transform child in QuickHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        quickSortArr.Clear();

        foreach (Transform child in MergeHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        mergeSortArr.Clear();


        int A = 0;
        int D = NumberOfNums;
        int n = 0;
        for (int i = 0; i < NumberOfNums; i++)
        {
            if (RandomToggle.isOn)
            {
                n = Random.Range(1, NumberOfNums) * (100 / NumberOfNums);
            }
            if (DescendingToggle.isOn)
            {
                n = D * (100 / NumberOfNums);
                D--;
            }
            if (AscendingToggle.isOn)
            {
                n = A * (100 / NumberOfNums);
                A++;
            }

            GameObject go = Instantiate(SortingAniCubeHolder, SelectHolder.transform.position, SelectHolder.transform.rotation, SelectHolder.transform);
            selectionSortArr.Add(go);
            Text a = selectionSortArr[i].GetComponentInChildren<Text>();
            a.text = n.ToString();
            
            go = Instantiate(SortingAniCubeHolder, InsertHolder.transform.position, InsertHolder.transform.rotation, InsertHolder.transform);
            insertionSortArr.Add(go);
            a = insertionSortArr[i].GetComponentInChildren<Text>();
            a.text = n.ToString();

            go = Instantiate(SortingAniCubeHolder, BubbleHolder.transform.position, BubbleHolder.transform.rotation, BubbleHolder.transform);
            bubbleSortArr.Add(go);
            a = bubbleSortArr[i].GetComponentInChildren<Text>();
            a.text = n.ToString();
            
            go = Instantiate(SortingAniCubeHolder, QuickHolder.transform.position, QuickHolder.transform.rotation, QuickHolder.transform);
            quickSortArr.Add(go);
            a = quickSortArr[i].GetComponentInChildren<Text>();
            a.text = n.ToString();

            go = Instantiate(SortingAniCubeHolder, MergeHolder.transform.position, MergeHolder.transform.rotation, MergeHolder.transform);
            mergeSortArr.Add(go);
            a = mergeSortArr[i].GetComponentInChildren<Text>();
            a.text = n.ToString();
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
        Debug.Log(selectionSortArr.Count);
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
                Debug.Log("RUN");

                yield return new WaitForSeconds(speed);
            }
            if (iMin != i)
            {
                float.TryParse(selectionSortArr[i].GetComponentInChildren<Text>().text, out float temp3);
                selectionSortArr[i].GetComponentInChildren<Text>().text = selectionSortArr[iMin].GetComponentInChildren<Text>().text;
                selectionSortArr[iMin].GetComponentInChildren<Text>().text = temp3.ToString();
            }
            yield return new WaitForSeconds(speed);
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
            yield return new WaitForSeconds(speed);
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
            yield return new WaitForSeconds(speed);
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