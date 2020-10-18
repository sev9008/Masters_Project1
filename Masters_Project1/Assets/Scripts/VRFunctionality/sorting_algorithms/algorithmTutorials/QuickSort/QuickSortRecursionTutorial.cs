using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortRecursionTutorial : MonoBehaviour
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
    private int pi;

    Coroutine SHO;

    public void Start()
    {
        firstiter = false;
        paused = false;
        manual = false;
        Begin();
        //structarr = new List<MyStruct>();
    }

    [Serializable]
    public class MyStruct
    {
        public int oldarr;
        public string steptxt;
        public GameObject Activeimage;
    }
    public void Update()
    {
        speed = slider.value;
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
            b[i].SetActive(true);
        }
        tmptxt.text += "Start \n";

        StartCoroutine(Quickchecksort());
    }

    public IEnumerator Quickchecksort()
    {
        running = true;
        firstiter = false;
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;

        Debug.Log(currentstrucstep + " - " + currentIndex + " - " + structarr.Count);
        yield return StartCoroutine(quickSort(0, b.Length - 1));
        tmptxt.text = "Quick Sort is finished.";

        running = false;
    }
    public IEnumerator quickSort(int l, int h)
    {
        //int tempsize = 0;
        if (l < h)
        {
            yield return StartCoroutine(partition(l, h));

            yield return StartCoroutine(quickSort(l, pi - 1));
           
            yield return StartCoroutine(quickSort(pi + 1, h));
        }
    }

    public IEnumerator partition(int l, int h)
    {
        float.TryParse(b[h].GetComponentInChildren<Text>().text, out float pivot);
        int i = (l - 1);

        for (int j = l; j < h; j++)
        {
            float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp2);
            if (temp2 < pivot)
            {
                i++;
                GameObject temp = b[i];
                b[i] = b[j];
                b[j] = temp;
            }
        }
        GameObject temp1 = b[i + 1];
        b[i + 1] = b[h];
        b[h] = temp1;
        pi = i + 1;
        yield return new WaitForSeconds(speed);
    }

    public IEnumerator changeStep()
    {
        resume:
        if (previous && currentstrucstep > 0)
        {
            currentstrucstep--;
            previous = false;
        }
        else if ((next && currentstrucstep != maxstrucstep) || currentstrucstep != maxstrucstep)
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
            else if (currentstrucstep >= 0 && currentstrucstep < maxstrucstep)
            {
                goto resume;
            }
        }
    }
}
