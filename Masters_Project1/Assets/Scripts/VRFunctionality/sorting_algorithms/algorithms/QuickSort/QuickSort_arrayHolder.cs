using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuickSort_arrayHolder : MonoBehaviour
{
    public List<int> arr;
    public Text Txt_Text;
    public int size;
    public float waittime;
    public Text Step;
    public Text ArrStep;
    public float speed;

    public Slider slider;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public bool paused;
    private int pi;
    private void Start()
    {
        paused = false;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display()
    {
        Txt_Text.text = "";
        int Tmpsize = size - 1;
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                Txt_Text.text += "a[" + Tmpsize + "] = " + arr[i].ToString();
            }
            else if (i > 0)
            {
                Txt_Text.text += ", " + arr[i].ToString();
            }
        }
    }
    public IEnumerator Quick(List<int> arr)
    {
        yield return StartCoroutine(quickSort(arr, 0, size - 1));
        Display();
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
        ArrStep.text = "";
        Step.text = "Finished";
    }
    public IEnumerator quickSort(List<int> arr, int l, int h)
    {
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
        if (l < h)
        {
            Step.text = "Find the pivot and create partitions";
            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
            yield return StartCoroutine(partition(arr, l, h));

            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(false);
            arrdup(arr, l, pi - 1);
            Step.text = "Perform quicksort on the array elements to the left of our pivot";
            yield return new WaitForSeconds(speed);
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(quickSort(arr, l, pi - 1));

            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
            arrdup(arr, pi + 1, h);
            Step.text = "Perform quicksort on the array elements to the right of our pivot";
            yield return new WaitForSeconds(speed);
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(quickSort(arr, pi + 1, h));
        }
    }

    public IEnumerator partition(List<int> arr, int l, int h)
    {
        int pivot = arr[h];
        int i = (l - 1);

        image4.SetActive(true);
        image5.SetActive(false);
        image6.SetActive(false);
        Step.text = "start with new pivot at " + pivot;
        while (paused)
        {
            yield return null;
        }
        yield return new WaitForSeconds(speed);
        for (int j = l; j < h; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                Step.text = "Swap " + arr[i] + " and " + arr[j];

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;

                image4.SetActive(false);
                image5.SetActive(true);
                image6.SetActive(false);
                Display();
                while (paused)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(speed);
            }
        }
        Step.text = "Swap " + arr[i + 1] + " and " + arr[h] + " to get the new pivot and return this value";

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[h];
        arr[h] = temp1;
        pi =  i + 1;

        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(true);
        Display();
        while (paused)
        {
            yield return null;
        }
        yield return new WaitForSeconds(speed);
    }

    public void arrdup(List<int> arr, int i, int k)
    {
        ArrStep.text = "";
        int Tmpsize = size - 1;
        for (int j = i; j < k; j++)
        {
            if (j == i)
            {
                ArrStep.text += "pivot of a[j] to a[k] = " + arr[j].ToString();
            }
            else if (j > i)
            {
                ArrStep.text += ", " + arr[j].ToString();
            }
        }
    }
}