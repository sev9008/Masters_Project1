using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MergeSort_arrHolder : MonoBehaviour
{
    public List<int> arr3;
    public Text Txt_Text;
    public float waittime;
    public Text Step;
    public Text ArrStep;
    public float speed;

    public Slider slider;

    public GameObject[] image;
    public bool paused;
    private int pi;
    public List<MyStruct> structarr;

    public int currentstrucstep;
    public int maxstrucstep;

    public bool previous;
    public bool next;
    public bool running;
    public SelectionAni m_selectionAni;
    private void Start()
    {
        paused = false;
        structarr = new List<MyStruct>();
    }
    [Serializable] public class MyStruct
    {
        public List<int> oldarr;
        public int activeImage;
        public string steptxt;
        public string arrtxt;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size)
    {
        Txt_Text.text = "";
        int Tmpsize = size - 1;
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                Txt_Text.text += "a[" + Tmpsize + "] = " + arr2[i].ToString();
            }
            else if (i > 0)
            {
                Txt_Text.text += ", " + arr2[i].ToString();
            }
        }
    }

    public IEnumerator IEMerge()
    {
        running = true;
        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].oldarr.Clear();
            structarr[n].activeImage = -1;
            structarr[n].steptxt = "";
            structarr[n].arrtxt = "";
        }

        structarr.Clear();
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;

        yield return StartCoroutine(MergeSort(arr3, 0, arr3.Count - 1));
        Display(arr3, arr3.Count);
        imageController(-1);
        ArrStep.text = "";
        Step.text = "Finished";
        running = false;
    }

    public IEnumerator MergeSort(List<int> arr, int l, int r)
    {
        while (paused)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);

        if (l < r)
        {
            int m = l + (r - l) / 2;
            Step.text = "Iterate through mergeSort to divide and conquer" + "\n" + "m: " + m + " l: " + l + " r: " + r;

            imageController(1);

            currentstrucstep++;
            maxstrucstep++;
            var a = new MyStruct();
            structarr.Add(a);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 1;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;
            m_selectionAni.ShowGraph(arr3);
            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(MergeSort(arr, l, m));

            imageController(2);
            currentstrucstep++;
            maxstrucstep++;
            var b = new MyStruct();
            structarr.Add(b);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 1;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;
            m_selectionAni.ShowGraph(arr3);
            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(MergeSort(arr, m + 1, r));

            imageController(3);
            currentstrucstep++;
            maxstrucstep++;
            var c = new MyStruct();
            structarr.Add(c);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 1;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;
            m_selectionAni.ShowGraph(arr3);
            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(merge(arr, l, m, r));
        }
        Display(arr, arr.Count);
    }

    public IEnumerator merge(List<int> arr, int l, int m, int r)
    {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        int[] L = new int[n1];
        int[] R = new int[n2];
        ArrStep.text = "Left array copy: ";

        for (i = 0; i < n1; i++)
        {
            L[i] = arr[l + i];
            ArrStep.text += L[i] + ", ";
        }
        ArrStep.text += "\n" + "Right array copy: ";
        for (j = 0; j < n2; j++)
        {
            R[j] = arr[m + 1 + j];
            ArrStep.text += R[j] + ", ";
        }

        imageController(4);
        currentstrucstep++;
        maxstrucstep++;
        var a = new MyStruct();
        structarr.Add(a);
        structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
        for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
        {
            structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
        }
        structarr[currentstrucstep].activeImage = 1;
        structarr[currentstrucstep].steptxt = Step.text;
        structarr[currentstrucstep].arrtxt = ArrStep.text;
        m_selectionAni.ShowGraph(arr3);
        yield return new WaitForSeconds(speed);
        if (next || previous)
        {
            yield return StartCoroutine(changeStep());
        }
        while (paused)
        {
            yield return null;
        }

        Step.text = "swap values between main array and left and right arrays";
        imageController(5);
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
            Display(arr, arr.Count);
            currentstrucstep++;
            maxstrucstep++;
            var b = new MyStruct();
            structarr.Add(b);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 1;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;
            m_selectionAni.ShowGraph(arr3);
            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
        }

        Step.text = "reorder left side";
        Display(arr, arr.Count);
        imageController(6);
        while (i < n1)
        {
            arr[k] = L[i];
            arr[k] = L[i];
            i++;
            k++;
            currentstrucstep++;
            maxstrucstep++;
            var c = new MyStruct();
            structarr.Add(c);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 1;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;
            m_selectionAni.ShowGraph(arr3);
            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
        }

        Step.text = "reorder right side";
        Display(arr, arr.Count);
        imageController(7);
        while (j < n2)
        {
            arr[k] = R[j];
            j++;
            k++;
            currentstrucstep++;
            maxstrucstep++;
            var d = new MyStruct();
            structarr.Add(d);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 1;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;
            m_selectionAni.ShowGraph(arr3);
            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
        }
    }
    public IEnumerator changeStep()
    {
        resume:
        if (previous && currentstrucstep > 0)
        {
            currentstrucstep--;
            previous = false;
        }
        else if (next && currentstrucstep != maxstrucstep || currentstrucstep != maxstrucstep)
        {
            currentstrucstep++;
            next = false;
        }

        Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count);
        imageController(structarr[currentstrucstep].activeImage);
        Step.text = structarr[currentstrucstep].steptxt;
        ArrStep.text = structarr[currentstrucstep].arrtxt;
        m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);
        yield return new WaitForSeconds(speed);
        if (currentstrucstep < maxstrucstep)
        {
            goto resume;
        }
    }

    public void imageController(int k)
    {
        for (int i = 0; i < image.Length; i++)
        {
            if (i == k)
            {
                image[i].SetActive(true);
            }
            else
            {
                image[i].SetActive(false);
            }
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
