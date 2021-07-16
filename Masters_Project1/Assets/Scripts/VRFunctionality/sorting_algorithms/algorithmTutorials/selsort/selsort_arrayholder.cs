using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class selsort_arrayholder : MonoBehaviour
{
    public GameObject[] b;

    public Text Txt_Text;
    public Text i_Text;
    public Text j_Text;
    public Text iMin_Text;
    public float waittime;
    public Text Step;
    public float speed;

    public Slider slider;

    public bool paused;
    public bool running;

    public bool previous;
    public bool next;

    public SelectionAni m_selectionAni;

    public List<MyStruct> structarr;

    public List<int> arr3;

    public int currentstrucstep;
    public int maxstrucstep;

    public Material imat;
    public Material jmat;
    public Material iMinmat;
    public Material Normalmat;

    public GameObject iholder;
    public GameObject jholder;
    public GameObject iMinholder;

    public GameObject[] image;

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
        public int jval;
        public int ival;
        public int iMinval;
    }
    //[SerializeField] private MyStruct struc;

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size, int i, int j, int iMin)
    {
        iholder.SetActive(true);
        jholder.SetActive(true);
        iMinholder.SetActive(true);
        Txt_Text.text = "";
        for (int n = 0; n < size; n++)
        {
            b[n].SetActive(true);
            if (n == 0)
            {
                if (n == i)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = imat;
                    iholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y+12);
                }
                else if (n == iMin)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
                    iMinholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
                }
                else 
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = Normalmat;
                }
            }
            else if (n > 0)
            {
                if (n == i)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = imat;
                    iholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y +12);
                }
                else if (n == iMin)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = iMinmat;
                    iMinholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
                }
                else if (n == j)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
                    jholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
                }
                else 
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = Normalmat;
                }
            }
        }
    }

    public IEnumerator Selection()
    {
        running = true;

        for (int n = 0; n < structarr.Count; n++)//reset all the data in the struct
        {
            structarr[n].oldarr.Clear();
            structarr[n].activeImage = -1;
            structarr[n].steptxt = "";
            structarr[n].jval = -1;
            structarr[n].ival = -1;
            structarr[n].iMinval = -1;
        }
        for (int k = 0; k < b.Length; k++)//set all blocks to false
        {
            b[k].SetActive(false);
        }
        structarr.Clear();//seems redundant.  dont actually know if the above is needed
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;
        m_selectionAni.ShowGraph(arr3);//display the current array on our graph
        Display(arr3, arr3.Count, 0, -1, -1);
        Step.text = "We will begin by initializing j, i, and iMin";
        if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
        {
            yield return new WaitForSeconds(speed);
        }
        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
            {
                next = false;
                paused = false;
            }
            else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }
        yield return new WaitForSeconds(speed);

        int i, j;
        int iMin;
        for (i = 0; i < arr3.Count - 1; i++)
        {
            iMin = i;

            imageController(0);
            Step.text = "increment i and set iMin = " + i;
            j_Text.text = " ";
            i_Text.text = "i = " + i.ToString();
            iMin_Text.text = "iMin = " + iMin.ToString();
            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count, i, -1, iMin);

            currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
            maxstrucstep++;
            var b = new MyStruct();
            structarr.Add(b);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].activeImage = 0;
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].jval = -1;
            structarr[currentstrucstep].ival = i;
            structarr[currentstrucstep].iMinval = iMin;
            m_selectionAni.ShowGraph(arr3);
            if (!manual)//descsibed above
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

            for (j = i + 1; j < arr3.Count; j++)
            {
                imageController(1);
                Step.text = "Increment j = " + j + " and swap if arr[j] < arr[iMin]";
                j_Text.text = "j = " + j.ToString();
                i_Text.text = "i = " + i.ToString();
                iMin_Text.text = "iMin = " + iMin.ToString();
                m_selectionAni.ShowGraph(arr3);
                Display(arr3, arr3.Count, i, j, iMin);

                currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
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
                structarr[currentstrucstep].jval = j;
                structarr[currentstrucstep].ival = i;
                structarr[currentstrucstep].iMinval = iMin;
                m_selectionAni.ShowGraph(arr3);
                if (!manual)//descsibed above
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

                if (arr3[j] < arr3[iMin])
                {
                    iMin = j;

                    imageController(1);
                    Step.text = "arr[f] < arr[iMin] so set iMin to j Set iMin to " + j;
                    j_Text.text = "j = " + j.ToString();
                    i_Text.text = "i = " + i.ToString();
                    iMin_Text.text = "iMin = " + iMin.ToString();
                    m_selectionAni.ShowGraph(arr3);
                    Display(arr3, arr3.Count, i, j, iMin);

                    currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
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
                    structarr[currentstrucstep].jval = j;
                    structarr[currentstrucstep].ival = i;
                    structarr[currentstrucstep].iMinval = iMin;
                    m_selectionAni.ShowGraph(arr3);
                    if (!manual)//descsibed above
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

            if (iMin != i)
            {
                int temp = arr3[i];
                arr3[i] = arr3[iMin];
                arr3[iMin] = temp;

                imageController(2);
                Step.text = "Swap arr[i] and arr[iMin].  arr[i] = " + arr3[iMin] + ". arr[iMin] = " + arr3[i];
                j_Text.text = "j = " + j.ToString();
                i_Text.text = "i = " + i.ToString();
                iMin_Text.text = "iMin = " + iMin.ToString();
                m_selectionAni.ShowGraph(arr3);
                Display(arr3, arr3.Count, iMin, j, i);

                currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
                maxstrucstep++;
                var e = new MyStruct();
                structarr.Add(e);
                structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
                }
                structarr[currentstrucstep].activeImage = 2;
                structarr[currentstrucstep].steptxt = Step.text;
                structarr[currentstrucstep].jval = j;
                structarr[currentstrucstep].ival = i;
                structarr[currentstrucstep].iMinval = iMin;
                m_selectionAni.ShowGraph(arr3);
                if (!manual)//descsibed above
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
        running = false;

        imageController(-1);
        Step.text = "The array is now Sorted";
        j_Text.text = "";
        i_Text.text = "";
        iMin_Text.text = "";
        m_selectionAni.ShowGraph(arr3);
        Display(arr3, arr3.Count, -1, -1, -1);
        currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
        maxstrucstep++;
        var a = new MyStruct();
        structarr.Add(a);
        structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
        for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
        {
            structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
        }
        structarr[currentstrucstep].activeImage = -1;
        structarr[currentstrucstep].steptxt = Step.text;
        structarr[currentstrucstep].jval = -1;
        structarr[currentstrucstep].ival = -1;
        structarr[currentstrucstep].iMinval = -1;
        m_selectionAni.ShowGraph(arr3);
        if (!manual)//descsibed above
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

        for (int n = 0; n < arr3.Count; n++)
        {
            b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
        }
    }

    public IEnumerator changeStep()//if next or previous is pressed then this algorithm takes over.  it will display the current step and attempt to continue the animation 
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

        imageController(structarr[currentstrucstep].activeImage);
        Step.text = structarr[currentstrucstep].steptxt;
        j_Text.text = structarr[currentstrucstep].jval.ToString();
        i_Text.text = structarr[currentstrucstep].ival.ToString();
        iMin_Text.text = structarr[currentstrucstep].iMinval.ToString();
        m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);
        Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count, structarr[currentstrucstep].ival, structarr[currentstrucstep].jval, structarr[currentstrucstep].iMinval);

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

    public void imageController(int k)//control the active image
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
