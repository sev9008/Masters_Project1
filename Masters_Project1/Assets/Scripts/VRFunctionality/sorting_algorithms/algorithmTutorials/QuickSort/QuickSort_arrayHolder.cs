using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuickSort_arrayHolder : MonoBehaviour
{
    public GameObject[] b;

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

    //public Material pivotmat;
    //public Material imat;
    //public Material jmat;
    //public Material hmat;
    //public Material lmat;
    //public Material Normalmat;
    public Material Donesort;//blue
    public Material NextSort;//red for swaps
    public Material Unsorted;//white
    public Material Recurs;//yellow for recursion
    public Material Goodmat;//green for pivot

    public Text PivotText;
    public Text iText;
    public Text jText;
    public Text ifText;
    public Text ifText1;
    public Text LText;
    public Text HText;

    public bool manual;

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
        public int pivot;
        public int i;
        public int j;
        public int l;
        public int h;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size, int pivot, int i, int j, int l, int h )//need to edit
    {
        Txt_Text.text = "";
        for (int n = 0; n < size; n++)
        {
            b[n].SetActive(true);
            Debug.Log(pivot);
            if (n == pivot)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = Goodmat;
            }
            else if (n == j)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = NextSort;
            }            
            else if (n == i)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = NextSort;
            }
            else if (n >= l && n <= h)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = Recurs;

            }
            else
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
        }
    }

    public IEnumerator Quick()
    {
        running = true;
        StopAllCoroutines();

        for (int k = 0; k < b.Length; k++)
        {
            b[k].SetActive(false);
        }
        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].oldarr.Clear();
            structarr[n].activeImage = -1;
            structarr[n].steptxt = "";
            structarr[n].arrtxt = "";
            structarr[n].pivot = -1;
            structarr[n].l = -1;
            structarr[n].j = -1;
            structarr[n].i = -1;
            structarr[n].h = -1;
        }

        structarr.Clear();
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;
        m_selectionAni.ShowGraph(arr3);

        yield return StartCoroutine(quickSort(arr3, 0, arr3.Count-1));
        Display(arr3, arr3.Count, -20, -20, -20, -20, -20);
        imageController(-1);
        ArrStep.text = "";
        Step.text = "Finished";
        running = false;
    }
    public IEnumerator quickSort(List<int> arr, int l, int h)
    {
        imageController(-1);
        Display(arr3, arr3.Count, -20, -20, -20, -20, -20);

        if (l < h)
        {
            LText.text = "L = " + l.ToString();
            HText.text = "H = " + h.ToString();
            ifText1.text = l.ToString() + " < " + h.ToString();
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
            structarr[currentstrucstep].pivot = -1;
            structarr[currentstrucstep].j = -1;
            structarr[currentstrucstep].i = -1;
            structarr[currentstrucstep].h = h;
            structarr[currentstrucstep].l = l;
            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count, -20, -20, -20, l, h);

            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (next && currentstrucstep >= maxstrucstep)
                {
                    next = false;
                    paused = false;
                }
                else if (next || previous)
                {
                    yield return StartCoroutine(changeStep());
                }
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
            structarr[currentstrucstep].pivot = -1;
            structarr[currentstrucstep].j = -1;
            structarr[currentstrucstep].i = -1;
            structarr[currentstrucstep].h = h;
            structarr[currentstrucstep].l = l;
            m_selectionAni.ShowGraph(arr3);
            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (next && currentstrucstep >= maxstrucstep)
                {
                    next = false;
                    paused = false;
                }
                else if (next || previous)
                {
                    yield return StartCoroutine(changeStep());
                }
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
            structarr[currentstrucstep].pivot = -1;
            structarr[currentstrucstep].j = -1;
            structarr[currentstrucstep].i = -1;
            structarr[currentstrucstep].h = h;
            structarr[currentstrucstep].l = l;
            m_selectionAni.ShowGraph(arr3);
            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (next && currentstrucstep >= maxstrucstep)
                {
                    next = false;
                    paused = false;
                }
                else if (next || previous)
                {
                    yield return StartCoroutine(changeStep());
                }
                yield return null;
            }
            yield return StartCoroutine(quickSort(arr, pi + 1, h));
        }
    }

    public IEnumerator partition(List<int> arr, int l, int h)
    {
        int pivot = arr[h];
        PivotText.text = "Pivot = " + pivot.ToString();
        int i = (l - 1);
        iText.text = "i = " + i.ToString();

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
        structarr[currentstrucstep].pivot = pivot;
        structarr[currentstrucstep].j = -1;
        structarr[currentstrucstep].i = i;
        structarr[currentstrucstep].h = h;
        structarr[currentstrucstep].l = l;
        m_selectionAni.ShowGraph(arr3);
        Display(arr3, arr3.Count, pivot, -20, -20, l, h);
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
        }
        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentstrucstep >= maxstrucstep)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }
        for (int j = l; j < h; j++)
        {
            ifText.text = arr[j].ToString() + " < " + i.ToString();
            if (arr[j] < pivot)
            {
                i++;
                iText.text = "i = " + i.ToString();
                jText.text = "j = " + j.ToString();
                Step.text = "Swap " + arr[i] + " and " + arr[j];

                b[i].GetComponentInChildren<MeshRenderer>().material = NextSort;
                b[j].GetComponentInChildren<MeshRenderer>().material = NextSort;

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
                structarr[currentstrucstep].pivot = pivot;
                structarr[currentstrucstep].j = j;
                structarr[currentstrucstep].i = i;
                structarr[currentstrucstep].h = h;
                structarr[currentstrucstep].l = l;

                imageController(4);
                Display(arr3, arr3.Count, h, i, j, l, h);
                m_selectionAni.ShowGraph(arr3);
                if (!manual)
                {
                    yield return new WaitForSeconds(speed);
                }
                if (manual)
                {
                    paused = true;
                }
                while (paused && manual)
                {
                    if (next && currentstrucstep >= maxstrucstep)
                    {
                        next = false;
                        paused = false;
                    }
                    else if (next || previous)
                    {
                        yield return StartCoroutine(changeStep());
                    }
                    yield return null;
                }
            }
        }
        Step.text = "Swap " + arr[i + 1] + " and " + arr[h] + " to get the new pivot and return this value";

        b[i+1].GetComponentInChildren<MeshRenderer>().material = NextSort;
        b[h].GetComponentInChildren<MeshRenderer>().material = NextSort;

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
        structarr[currentstrucstep].pivot = pivot;
        structarr[currentstrucstep].j = -1;
        structarr[currentstrucstep].i = i;
        structarr[currentstrucstep].h = h;
        structarr[currentstrucstep].l = l;

        imageController(5);
        Display(arr3, arr3.Count, h, i+1, h, l, h);
        m_selectionAni.ShowGraph(arr3);
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
        }
        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentstrucstep >= maxstrucstep)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
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

        Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count, structarr[currentstrucstep].pivot, structarr[currentstrucstep].i, structarr[currentstrucstep].j, structarr[currentstrucstep].l, structarr[currentstrucstep].h);
        imageController(structarr[currentstrucstep].activeImage);
        Step.text = structarr[currentstrucstep].steptxt;
        ArrStep.text = structarr[currentstrucstep].arrtxt;
        m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);

        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentstrucstep >= maxstrucstep)
            {
                next = false;
                paused = false;
            }
            else if (previous && currentstrucstep > 0)
            {
                goto resume;
            }
            else if (next && currentstrucstep < maxstrucstep)
            {
                goto resume;
            }
            yield return null;
        }
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
            if (currentstrucstep >= maxstrucstep)
            {
                paused = false;
            }
            else if (currentstrucstep >= 0 && currentstrucstep < maxstrucstep)
            {
                goto resume;
            }
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