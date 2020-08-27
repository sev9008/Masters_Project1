using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuickSort_arrayHolder : MonoBehaviour
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
    public IEnumerator Quick()
    {
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
        //running = true;
        //m_selectionAni.ShowGraph(arr3);

        yield return StartCoroutine(quickSort(arr3, 0, arr3.Count-1));
        Display(arr3, arr3.Count);
        imageController(-1);
        ArrStep.text = "";
        Step.text = "Finished";
    }
    public IEnumerator quickSort(List<int> arr, int l, int h)
    {
        Debug.Log("work");
        imageController(-1);
        if (l < h)
        {
            Step.text = "Find the pivot and create partitions";
            imageController(0);

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

            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(partition(arr, l, h));

            imageController(1);
            arrdup(arr, l, pi - 1);
            Step.text = "Perform quicksort on the array elements to the left of our pivot";

            currentstrucstep++;
            maxstrucstep++;
            var c = new MyStruct();
            structarr.Add(c);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 2;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;

            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            while (paused)
            {
                yield return null;
            }
            yield return StartCoroutine(quickSort(arr, l, pi - 1));

            imageController(2);
            arrdup(arr, pi + 1, h);
            Step.text = "Perform quicksort on the array elements to the right of our pivot";

            currentstrucstep++;
            maxstrucstep++;
            var b = new MyStruct();
            structarr.Add(b);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 3;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].arrtxt = ArrStep.text;

            yield return new WaitForSeconds(speed);
            if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
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

        imageController(3);
        Step.text = "start with new pivot at " + pivot;

        currentstrucstep++;
        maxstrucstep++;
        var c = new MyStruct();
        structarr.Add(c);
        structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
        for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
        {
            structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
        }
        structarr[currentstrucstep].activeImage = 4;
        structarr[currentstrucstep].steptxt = Step.text;
        structarr[currentstrucstep].arrtxt = ArrStep.text;
        yield return new WaitForSeconds(speed);
        if (next || previous)
        {
            yield return StartCoroutine(changeStep());
        }
        while (paused)
        {
            yield return null;
        }
        for (int j = l; j < h; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                Step.text = "Swap " + arr[i] + " and " + arr[j];

                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;

                currentstrucstep++;
                maxstrucstep++;
                var a = new MyStruct();
                structarr.Add(a);
                structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
                }
                structarr[currentstrucstep].activeImage = 5;
                structarr[currentstrucstep].steptxt = Step.text;
                structarr[currentstrucstep].arrtxt = ArrStep.text;

                imageController(4);
                Display(arr, arr.Count);
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
        Step.text = "Swap " + arr[i + 1] + " and " + arr[h] + " to get the new pivot and return this value";

        int temp1 = arr[i + 1];
        arr[i + 1] = arr[h];
        arr[h] = temp1;
        pi =  i + 1;

        currentstrucstep++;
        maxstrucstep++;
        var m = new MyStruct();
        structarr.Add(m);
        structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
        for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
        {
            structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
        }
        structarr[currentstrucstep].activeImage = 6;
        structarr[currentstrucstep].steptxt = Step.text;
        structarr[currentstrucstep].arrtxt = ArrStep.text;

        imageController(5);
        Display(arr, arr.Count);
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

    public void arrdup(List<int> arr, int i, int k)
    {
        ArrStep.text = "";
        int Tmpsize = arr3.Count;
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