using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MergeSort_arrHolder : MonoBehaviour
{
    public GameObject[] b;

    public List<int> arr3;
    public Text Txt_Text;
    public float waittime;
    public Text Step;
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
    public bool manual;

    public Material imat;
    public Material jmat;
    public Material kmat;
    public Material iMinmat;
    public Material Normalmat;
    public Material checkmat;

    public Text ltextstep1;
    public Text rtextstep1;
    public Text mtextstep1;

    public Text n1textstep1;
    public Text n2textstep1;
    public Text itextstep1;
    public Text jtextstep1;
    public Text n1textstep2;
    public Text n2textstep2;
    
    public Text whiletextstep1;
    public Text ktextstep1;
    public Text ifstep1;
    public Text whiletextstep2;
    public Text ktextstep2;    
    public Text whiletextstep3;
    public Text ktextstep3;
    public Text Larr;
    public Text Rarr;


    private void Start()
    {
        paused = false;
        manual = false;
        structarr = new List<MyStruct>();
    }
    [Serializable] public class MyStruct
    {
        public List<int> oldarr;
        public int activeImage;
        public string steptxt;
        public string Larr;
        public string Rarr;
        public int l;
        public int r;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size, int l, int r, int i, int j, int k)
    {
        //size = size - 1;
        Txt_Text.text = "";
        //int Tmpsize = size - 1;
        for (int n = 0; n < size; n++)
        {
            b[n].SetActive(true);
            //Debug.Log(l + " < " + n + " < "+ r);
            if (l <= n && n <= r)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = checkmat;
            }
            else if (n == 0)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = Normalmat;
            }
            else
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = Normalmat;
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
            structarr[n].Larr = "";
            structarr[n].Rarr = "";
            structarr[n].l = -1;
            structarr[n].r = -1;
        }
        for (int k = 0; k < b.Length; k++)
        {
            b[k].SetActive(false);
        }

        structarr.Clear();
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;

        yield return StartCoroutine(MergeSort(arr3, 0, arr3.Count - 1));
        Display(arr3, arr3.Count, -1,-1,-1,-1,-1);
        imageController(-1);
        Step.text = "Finished";
        running = false;
    }


    //public Text ltextstep1;
    //public Text rtextstep1;
    //public Text mtextstep1;

    public IEnumerator MergeSort(List<int> arr, int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            ltextstep1.text = "l = " + l;
            rtextstep1.text = "r = " + r;
            mtextstep1.text = "m = " + m;
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
            structarr[currentstrucstep].Larr = Larr.text;
            structarr[currentstrucstep].Rarr = Rarr.text;
            structarr[currentstrucstep].l = l;
            structarr[currentstrucstep].r = r;
            m_selectionAni.ShowGraph(arr3);
            Display(arr, arr.Count, l, r, -1, -1, -1);
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
            yield return StartCoroutine(MergeSort(arr, l, m));

            ltextstep1.text = "l = " + l;
            rtextstep1.text = "r = " + r;
            mtextstep1.text = "m = " + m;
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
            structarr[currentstrucstep].Larr = Larr.text;
            structarr[currentstrucstep].Rarr = Rarr.text;
            structarr[currentstrucstep].l = l;
            structarr[currentstrucstep].r = r;
            m_selectionAni.ShowGraph(arr3);
            Display(arr, arr.Count, l, r, -1, -1, -1);
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
            yield return StartCoroutine(MergeSort(arr, m + 1, r));

            ltextstep1.text = "l = " + l;
            rtextstep1.text = "r = " + r;
            mtextstep1.text = "m = " + m;
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
            structarr[currentstrucstep].Larr = Larr.text;
            structarr[currentstrucstep].Rarr = Rarr.text;
            structarr[currentstrucstep].l = l;
            structarr[currentstrucstep].r = r;
            m_selectionAni.ShowGraph(arr3);
            Display(arr, arr.Count, l, r, -1, -1, -1);
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
            yield return StartCoroutine(merge(arr, l, m, r));
        }
        Display(arr, arr.Count, -1, -1, -1, -1, -1);
    }

    public IEnumerator merge(List<int> arr, int l, int m, int r)
    {
        itextstep1.text = "i = 0";
        jtextstep1.text = "j = 0";
        n1textstep1.text = "n1 = 0";
        n2textstep1.text = "n2 = 0";
        itextstep1.text = "i = 0";
        jtextstep1.text = "j = 0";
        n1textstep2.text = "n1 = 0";
        n2textstep2.text = "n2 = 0";
        whiletextstep1.text = "i < n1 && j < n2";
        ktextstep1.text = "k = 0";
        whiletextstep2.text = "i < n1";
        ktextstep2.text = "k = 0";
        whiletextstep3.text = "j < n2";
        ktextstep3.text = "k = 0";
        Larr.text = "L[] = 0";
        Rarr.text = "R[] = 0";
        ifstep1.text = "0 <= 0";


        int i, j, k;
        int n1 = m - l + 1;
        n1textstep1.text = "n1 = " + n1;
        n1textstep2.text = "n1 = " + n1;
        int n2 = r - m;
        n2textstep1.text = "n2 = " + n2;
        n2textstep2.text = "n2 = " + n2;
        int[] L = new int[n1];
        int[] R = new int[n2];

        Larr.text = "L[] = ";
        for (i = 0; i < n1; i++)
        {
            L[i] = arr[l + i];
            Larr.text += L[i] + ", ";
            itextstep1.text = "i = " + i;
        }

        Rarr.text = "R[] = ";
        for (j = 0; j < n2; j++)
        {
            R[j] = arr[m + 1 + j];
            Rarr.text += R[j] + ", ";
            itextstep1.text = "j = " + j;
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
        structarr[currentstrucstep].Larr = Larr.text;
        structarr[currentstrucstep].Rarr = Rarr.text;
        structarr[currentstrucstep].l = l;
        structarr[currentstrucstep].r = r;
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


        Step.text = "swap values between main array and left and right arrays";
        imageController(5);
        i = 0;
        j = 0;
        k = l;

        whiletextstep1.text = i + "<" + n1 + " && " + j + "<" + n2;
        ktextstep1.text = "k = " + k;
        while (i < n1 && j < n2)
        {
            ifstep1.text = L[i] + " <= " + R[j];
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
            Display(arr, arr.Count, l, r, i, j, k);
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
            structarr[currentstrucstep].Larr = Larr.text;
            structarr[currentstrucstep].Rarr = Rarr.text;
            structarr[currentstrucstep].l = l;
            structarr[currentstrucstep].r = r;
            m_selectionAni.ShowGraph(arr3);
            whiletextstep1.text = i + "<" + n1 + " && " + j + "<" + n2;
            ktextstep1.text = "k = " + k;
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

        Step.text = "reorder left side";
        Display(arr, arr.Count, l, r, i, j, k);
        imageController(6);
        whiletextstep2.text = i + "<" + n1;
        ktextstep2.text = "k = " + k;
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
            structarr[currentstrucstep].Larr = Larr.text;
            structarr[currentstrucstep].Rarr = Rarr.text;
            structarr[currentstrucstep].l = l;
            structarr[currentstrucstep].r = r;
            m_selectionAni.ShowGraph(arr3);
            Display(arr, arr.Count, l, r, i, j, k);
            whiletextstep2.text = i + "<" + n1;
            ktextstep2.text = "k = " + k;
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

        Step.text = "reorder right side";
        Display(arr, arr.Count, l, r, i, j, k);
        imageController(7);
        whiletextstep3.text = i + "<" + n1;
        ktextstep3.text = "k = " + k;
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
            structarr[currentstrucstep].Larr = Larr.text;
            structarr[currentstrucstep].Rarr = Rarr.text;
            structarr[currentstrucstep].l = l;
            structarr[currentstrucstep].r = r;
            m_selectionAni.ShowGraph(arr3);
            Display(arr, arr.Count, l, r, i, j, k);
            whiletextstep3.text = i + "<" + n1;
            ktextstep3.text = "k = " + k;
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

        Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count, structarr[currentstrucstep].l, structarr[currentstrucstep].r, -1, -1, -1);
        imageController(structarr[currentstrucstep].activeImage);
        Step.text = structarr[currentstrucstep].steptxt;
        Larr.text = structarr[currentstrucstep].Larr;
        Rarr.text = structarr[currentstrucstep].Rarr;

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