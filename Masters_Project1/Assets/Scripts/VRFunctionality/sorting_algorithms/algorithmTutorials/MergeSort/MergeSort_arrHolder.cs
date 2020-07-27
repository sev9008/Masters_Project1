using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MergeSort_arrHolder : MonoBehaviour
{
    public List<int> arr;
    public Text Txt_Text;
    public int size;
    public float waittime;
    public Text Step;
    public Text ArrayStep;
    public float speed;

    public Slider slider;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;
    public bool paused;

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

    public IEnumerator IEMerge()
    {
        yield return StartCoroutine(MergeSort(arr, 0, size-1));
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
        image7.SetActive(false);

        ArrayStep.text = "Finished";
        Step.text = "Finished";
    }

    public IEnumerator MergeSort(List<int> arr, int l, int r)
    {
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
        image7.SetActive(false);
        while (paused)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);

        if (l < r)
        {
            int m = l + (r - l) / 2;
            Step.text = "Iterate through mergeSort to divide and conquer" + "\n" + "m: " + m + " l: " + l + " r: " + r;

            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
            yield return StartCoroutine(MergeSort(arr, l, m));

            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(false);
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
            yield return StartCoroutine(MergeSort(arr, m + 1, r));

            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
            yield return StartCoroutine(merge(arr, l, m, r));
        }
        Display();
    }

    public IEnumerator merge(List<int> arr, int l, int m, int r)
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        int[] L = new int[n1];
        int[] R = new int[n2];
        ArrayStep.text = "Left array copy: ";

        for (i = 0; i < n1; i++)
        {
            L[i] = arr[l + i];
            ArrayStep.text += L[i] + ", ";
        }
        ArrayStep.text += "\n" + "Right array copy: ";
        for (j = 0; j < n2; j++)
        {
            R[j] = arr[m + 1 + j];
            ArrayStep.text += R[j] + ", ";
        }

        image4.SetActive(true);
        image5.SetActive(false);
        image6.SetActive(false);
        image7.SetActive(false);
        while (paused)
        {
            yield return null;
        }
        yield return new WaitForSeconds(speed);

        Step.text = "swap values between main array and left and right arrays";
        image4.SetActive(false);
        image5.SetActive(true);
        image6.SetActive(false);
        image7.SetActive(false);
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
            Display();
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
        }

        Step.text = "reorder left side";
        Display();
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(true);
        image7.SetActive(false);
        while (i < n1)
        {
            arr[k] = L[i];
            i++;
            k++;
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
        }

        Step.text = "reorder right side";
        Display();
        image4.SetActive(false);
        image5.SetActive(false);
        image6.SetActive(false);
        image7.SetActive(true);
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
        }
    }
}

/*
 * 
 void merge(int arr[], int l, int m, int r) 
{ 
    int i, j, k; 
    int n1 = m - l + 1; 
    int n2 = r - m; 
    int L[n1], R[n2]; 
    for (i = 0; i<n1; i++)
    {
        L[i] = arr[l + i]; 
    }
    for (j = 0; j<n2; j++) 
    {   
        R[j] = arr[m + 1 + j]; 
    }
    i = 0; 
    j = 0; 
    k = l; 
    while (i<n1 && j<n2) 
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
    } 
    while (i<n1) { 
        arr[k] = L[i]; 
        i++; 
        k++; 
    } 
    while (j<n2) 
    { 
        arr[k] = R[j]; 
        j++; 
        k++; 
    } 
} 
  
void mergeSort(int arr[], int l, int r)
{
    if (l < r)
    {
        int m = l + (r - l) / 2;
        mergeSort(arr, l, m);
        mergeSort(arr, m + 1, r);
        merge(arr, l, m, r);
    }
}
 */
