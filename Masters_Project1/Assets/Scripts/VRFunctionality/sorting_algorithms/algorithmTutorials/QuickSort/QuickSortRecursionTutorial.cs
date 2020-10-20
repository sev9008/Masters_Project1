using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortRecursionTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;
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

    public GameObject StepCube;
    public GameObject PivotPointer;
    public GameObject LPointer;
    public GameObject HPointer;
    private int pi;
    //public bool lastrightside;

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
        public List<float> oldarr;
        public string steptxt;
        public string Cubetxt;
        public int pi;
        public int left;
        public int right;
        public GameObject Activeimage;
    }
    public void Update()
    {
        speed = slider.value;
    }

    public void Begin()
    {
        StopAllCoroutines();
        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].steptxt = "";
            structarr[n].pi = -20;
            for (int m = 0; m < structarr[n].oldarr.Count; m++)
            {
                structarr[n].oldarr.Clear();
            }
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
        //int tn = b.Length;
        //for (int i = 0; i < b.Length; i++)
        //{
        //    b[i].GetComponentInChildren<Text>().text = i.ToString();
        //}       
        tmptxt.text += "Start \n";
        currentstrucstep = -1;
        maxstrucstep = -1;
        StartCoroutine(Quickchecksort());
    }

    public IEnumerator Quickchecksort()
    {
        running = true;
        firstiter = false;
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;

        //Debug.Log(currentstrucstep + " - " + currentIndex + " - " + structarr.Count);
        yield return StartCoroutine(quickSort(0, b.Length - 1));
        tmptxt.text = "Quick Sort is finished.";

        running = false;
    }
    public IEnumerator quickSort(int l, int h)
    {
        if (l < h)
        {
            yield return StartCoroutine(partition(l, h));
            tmptxt.text = "Perform Partition";
            StepCube.GetComponentInChildren<Text>().text = "Perform Partition";
            currentstrucstep++;
            maxstrucstep++;
            var a = new MyStruct();
            structarr.Add(a);
            int m = 0;
            structarr[currentstrucstep].oldarr = new List<float>(h-l);
            for (int i = l; i <= h; i++)
            {
                float.TryParse(b[i].GetComponentInChildren<Text>().text, out float temp);
                structarr[currentstrucstep].oldarr.Add(temp);
                m++;
            }
            structarr[currentstrucstep].steptxt = tmptxt.text;
            structarr[currentstrucstep].Cubetxt = StepCube.GetComponentInChildren<Text>().text;
            structarr[currentstrucstep].pi = pi;
            structarr[currentstrucstep].left = l;
            structarr[currentstrucstep].right = h;
            structarr[currentstrucstep].Activeimage = image1;
            Updatedisplay();
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

            tmptxt.text = "Perform quicksort on left side";
            StepCube.GetComponentInChildren<Text>().text = "Perform quicksort on left side";
            currentstrucstep++;
            maxstrucstep++;
            a = new MyStruct();
            structarr.Add(a);
            m = 0;
            structarr[currentstrucstep].oldarr = new List<float>(0);
            for (int i = l; i <= pi-1; i++)
            {
                float.TryParse(b[i].GetComponentInChildren<Text>().text, out float temp);
                structarr[currentstrucstep].oldarr.Add(temp);
                m++;
            }
            structarr[currentstrucstep].steptxt = tmptxt.text;
            structarr[currentstrucstep].Cubetxt = StepCube.GetComponentInChildren<Text>().text;
            structarr[currentstrucstep].pi = pi;
            structarr[currentstrucstep].left = l;
            structarr[currentstrucstep].right = pi-1;
            structarr[currentstrucstep].Activeimage = image1;
            Updatedisplay();
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
            yield return StartCoroutine(quickSort(l, pi - 1));

            tmptxt.text = "Perform quicksort on right side";
            StepCube.GetComponentInChildren<Text>().text = "Perform quicksort on right side";
            currentstrucstep++;
            maxstrucstep++;
            a = new MyStruct();
            structarr.Add(a);
            m = 0;
            structarr[currentstrucstep].oldarr = new List<float>(h - pi+1);
            for (int i = pi+1; i <= h; i++)
            {
                float.TryParse(b[i].GetComponentInChildren<Text>().text, out float temp);
                structarr[currentstrucstep].oldarr.Add(temp);
                m++;
            }
            structarr[currentstrucstep].steptxt = tmptxt.text;
            structarr[currentstrucstep].Cubetxt = StepCube.GetComponentInChildren<Text>().text;
            structarr[currentstrucstep].pi = pi;
            structarr[currentstrucstep].left = pi+1;
            structarr[currentstrucstep].right = h;
            structarr[currentstrucstep].Activeimage = image1;
            Updatedisplay();
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
                updatePos();
            }
        }
        GameObject temp1 = b[i + 1];
        b[i + 1] = b[h];
        b[h] = temp1;
        updatePos();
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

        //for (int i = 0; i < bstep.Length; i++)
        //{
        //    if (i < structarr[currentstrucstep].oldarr)
        //    {
        //        bstep[i].SetActive(true);
        //    }
        //    else
        //    {
        //        bstep[i].SetActive(false);
        //    }
        //}

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

    public void Updatedisplay()
    {
        for (int i = 0; i < bstep.Length; i++)
        {
            bstep[i].SetActive(false);
        }
        int m = 0;

        LPointer.SetActive(false);
        HPointer.SetActive(false);
        PivotPointer.SetActive(false);

        if (structarr[currentstrucstep].left < structarr[currentstrucstep].right)
        {
            if (structarr[currentstrucstep].left == structarr[currentstrucstep].right)
            { }
            else
            {
                LPointer.SetActive(true);
                HPointer.SetActive(true);
                if (structarr[currentstrucstep].pi >= structarr[currentstrucstep].left && structarr[currentstrucstep].pi <= structarr[currentstrucstep].right)
                {
                    PivotPointer.SetActive(true);
                }
                LPointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(bstep[structarr[currentstrucstep].left].GetComponent<RectTransform>().anchoredPosition.x, bstep[structarr[currentstrucstep].left].GetComponent<RectTransform>().anchoredPosition.y + 14);
                HPointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(bstep[structarr[currentstrucstep].right].GetComponent<RectTransform>().anchoredPosition.x, bstep[structarr[currentstrucstep].right].GetComponent<RectTransform>().anchoredPosition.y + 14);
                PivotPointer.GetComponent<RectTransform>().anchoredPosition = new Vector2(bstep[structarr[currentstrucstep].pi].GetComponent<RectTransform>().anchoredPosition.x, bstep[structarr[currentstrucstep].pi].GetComponent<RectTransform>().anchoredPosition.y - 14);
                for (int i = 0; i < bstep.Length; i++)
                {
                    if (i >= structarr[currentstrucstep].left && i <= structarr[currentstrucstep].right)
                    {
                        Debug.Log(m + "   " + i);
                        bstep[i].GetComponentInChildren<Text>().text = structarr[currentstrucstep].oldarr[m].ToString();
                        m++;
                        bstep[i].SetActive(true);
                    }
                }
            }
        }
        else 
        {
            for (int i = 0; i < bstep.Length; i++)
            {
                if (i >= structarr[currentstrucstep].left && i <= structarr[currentstrucstep].right)
                {
                    bstep[i].GetComponentInChildren<Text>().text = structarr[currentstrucstep].oldarr[m].ToString();
                    m++;
                    bstep[i].SetActive(true);
                }
            }
        }
    }

    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].GetComponent<RectTransform>().anchoredPosition = pos[i].GetComponent<RectTransform>().anchoredPosition;
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
        }
    }

}
