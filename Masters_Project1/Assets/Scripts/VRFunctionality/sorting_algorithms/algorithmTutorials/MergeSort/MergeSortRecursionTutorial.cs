using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortRecursionTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] bstep;
    public float speed;
    public int currentIndex;
    public Text tmptxt;
    public Material Lmat;
    public Material Rmat;
    public Material Normalmat;
    public bool manual;
    public bool next;
    public bool previous;
    public bool paused;
    public int currentstrucstep;
    public int maxstrucstep;
    public List<MyStruct> structarr;
    public Slider slider;
    public bool running;
    private bool firstiter;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    Coroutine SHO;



    public void Start()
    {
        firstiter = false;
        paused = false;
        manual = false;
        //Begin();
        //structarr = new List<MyStruct>();
    }

    [Serializable] public class MyStruct
    {
        public int oldarr;
        public string steptxt;
        public GameObject Activeimage;
    }
    public void Update()
    {
        speed = slider.value;
        //speed = 1;
    }

    public void Begin()
    {
        StopAllCoroutines();
        currentIndex = 0;
        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].steptxt = "";
            structarr[n].oldarr = -1;
            structarr[n].Activeimage = null;
        }
        for (int k = 0; k < bstep.Length; k++)
        {
            bstep[k].SetActive(false);
        }
        structarr.Clear();
        structarr = new List<MyStruct>();

        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        tmptxt.text += "Start \n";

        StartCoroutine(Mergechecksort());
    }

    public IEnumerator Mergechecksort()
    {
        running = true;
        firstiter = false;
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;

        Debug.Log(currentstrucstep + " - " + currentIndex + " - " + structarr.Count);
        yield return StartCoroutine(mergeSort(0, b.Length - 1));
        tmptxt.text = "Merge Sort is finished.";

        running = false;
    }

    public IEnumerator mergeSort(int l, int r)
    {
        int tempsize = 0;
        if (l < r)
        {
            int m = l + (r - l) / 2;


            tempsize = m - l;
            for (int i = l; i < l+tempsize+1; i++)
            {
                bstep[currentIndex].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
                bstep[currentIndex].GetComponentInChildren<MeshRenderer>().material = Lmat;
                bstep[currentIndex].SetActive(true);
                currentIndex++;
            }
            tmptxt.text = "Perform MergeSort on the left side of the array.\n" + "Left index = " + l + "\nRight index = " + m;
            currentstrucstep++;
            maxstrucstep++;
            var a = new MyStruct();
            a.oldarr = currentIndex;
            a.steptxt = tmptxt.text;
            a.Activeimage = image1;
            structarr.Add(a);
            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
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
            yield return mergeSort(l, m);


            tempsize = r - m;
            for (int i = m+1; i < m+1+tempsize; i++)
            {
                bstep[currentIndex].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
                bstep[currentIndex].GetComponentInChildren<MeshRenderer>().material = Rmat;
                bstep[currentIndex].SetActive(true);
                currentIndex++;
            }
            tmptxt.text = "Perform MergeSort on the right side of the array.\n" + "Left index = " + m+1 + "\nRight index = " + r;
            currentstrucstep++;
            maxstrucstep++;
            var c = new MyStruct();
            c.oldarr = currentIndex;
            c.steptxt = tmptxt.text;
            c.Activeimage = image2;
            structarr.Add(c);
            //Debug.Log(currentstrucstep + " - " + currentIndex + " - " + structarr.Count);
            image1.SetActive(false);
            image2.SetActive(true);
            image3.SetActive(false);
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
            yield return mergeSort(m + 1, r);

            tmptxt.text = "Perform Merge on ";
            for (int i = l; i < r+1; i++)
            {
                tmptxt.text += b[i].GetComponentInChildren<Text>().text + " ";
            }
            tmptxt.text += "\nLeft index = " + l + "\nMiddle index = " + m + "\nRight index = " + r;
            yield return merge(l, m, r);
            currentstrucstep++;
            maxstrucstep++;
            var d = new MyStruct();
            d.oldarr = currentIndex;
            d.steptxt = tmptxt.text;
            d.Activeimage = image3;
            structarr.Add(d);
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
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

    public IEnumerator merge(int l, int m, int r)
    {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        GameObject[] L = new GameObject[n1];
        GameObject[] R = new GameObject[n2];

        for (i = 0; i < n1; i++)
        {
            L[i] = b[l + i];
        }

        for (j = 0; j < n2; j++)
        {
            R[j] = b[m + 1 + j];
        }

        i = 0;
        j = 0;
        k = l;

        while (i < n1 && j < n2)
        {
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out float temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out float temp2);

            if (temp <= temp2)
            {
                b[k] = L[i];
                i++;
            }
            else
            {
                b[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            b[k] = L[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            b[k] = R[j];
            j++;
            k++;

        }
        for (int n = l; n < k; n++)
        {
            bstep[currentIndex].GetComponentInChildren<Text>().text = b[n].GetComponentInChildren<Text>().text;
            bstep[currentIndex].GetComponentInChildren<MeshRenderer>().material = Normalmat;
            bstep[currentIndex].SetActive(true);
            currentIndex++;
        }
        yield return null;
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
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        structarr[currentstrucstep].Activeimage.SetActive(true);

        tmptxt.text = structarr[currentstrucstep].steptxt;
        
        for (int i = 0; i < bstep.Length; i++)
        {
            if (i < structarr[currentstrucstep].oldarr)
            {
                bstep[i].SetActive(true);
            }
            else
            {
                bstep[i].SetActive(false);
            }
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
            else if (currentstrucstep > 0 && currentstrucstep < maxstrucstep)
            {
                goto resume;
            }
        }
    }
}
