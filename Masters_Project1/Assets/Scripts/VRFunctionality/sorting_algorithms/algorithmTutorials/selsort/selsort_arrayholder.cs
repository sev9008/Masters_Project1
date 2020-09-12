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

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
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

    public bool manual;




    private void Start()
    {
        paused = false;
        structarr = new List<MyStruct>();
    }
    [Serializable] public class MyStruct
    {
        public List<int> oldarr;
        public int i_value;
    }
    //[SerializeField] private MyStruct struc;

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size, int i, int j, int iMin)
    {
        iholder.SetActive(false);
        jholder.SetActive(false);
        iMinholder.SetActive(false);
        Txt_Text.text = "";
        int Tmpsize = size - 1;
        for (int n = 0; n < size; n++)
        {
            b[n].SetActive(true);
            if (n == 0)
            {
                if (n == i)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = imat;
                    iholder.SetActive(true);
                    iholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y+12);
                }
                else if (n == iMin)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
                    iMinholder.SetActive(true);
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
                    iholder.SetActive(true);
                    iholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y +12);
                }
                else if (n == iMin)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = iMinmat;
                    iMinholder.SetActive(true);
                    iMinholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
                }
                else if (n == j)
                {
                    b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                    b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
                    jholder.SetActive(true);
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
        if (manual)
        {
            paused = true;
        }
        for (int k = 0; k < b.Length; k++)
        {
            b[k].SetActive(false);
        }
        //clear struct
        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].oldarr.Clear();
            structarr[n].i_value = -1;
        }
        
        structarr.Clear();
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;
        running = true;
        m_selectionAni.ShowGraph(arr3);
        int i, j;
        int iMin;
        for (i = 0; i < arr3.Count - 1; i++)
        {
            i_Text.text = "i = " + i.ToString();
            Display(arr3, arr3.Count, i, -1, -1);
            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (currentstrucstep == maxstrucstep)
            {
                currentstrucstep++;
                maxstrucstep++;
                Debug.Log("addnew");
                var c = new MyStruct();
                structarr.Add(c);
                structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
                }
                structarr[currentstrucstep].i_value = i;
            }
            
            resume:
            
            if (previous && next)
            {
                previous = false;
                next = false;
            }
            if (previous && currentstrucstep > 0)
            {
                currentstrucstep--;
                PressedPrevious();
                previous = false;
                i = structarr[currentstrucstep].i_value;
            }
            else if (previous && currentstrucstep <= 0)
            {
                Step.text = "Cant go back any further";
                if (!manual)
                {
                    yield return new WaitForSeconds(speed);
                }
            }

            if (next && currentstrucstep < maxstrucstep)
            {
                currentstrucstep++;
                PressedNext();
                next = false;
                i = structarr[currentstrucstep].i_value;
            }
            else if (next && currentstrucstep >= maxstrucstep)
            {
                Step.text = "Cant go forward any further";
                if (!manual)
                {
                    yield return new WaitForSeconds(speed);
                }
            }
            previous = false;
            next = false;
            
            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count, i, -1, -1);
            iMin = i;
            iMin_Text.text = "iMin = " + iMin.ToString();

            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            Step.text = "increment i = " + i + ", reset iMin";
            Display(arr3, arr3.Count, i, -1, iMin);
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
                if (previous && currentstrucstep > 0)
                {
                    goto resume;
                }
                if (next && currentstrucstep < maxstrucstep)
                {
                    goto resume;
                }
                else if (next && currentstrucstep >= maxstrucstep)
                {
                    next = false;
                    paused = false;
                }
                yield return null;
            }

            for (j = i + 1; j < arr3.Count; j++)
            {
                j_Text.text = "j = " + j.ToString();
                Display(arr3, arr3.Count, i, j, iMin);
                Step.text = "Increment j = " + j;
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
                    if (previous && currentstrucstep > 0)
                    {
                        goto resume;
                    }
                    if (next && currentstrucstep < maxstrucstep)
                    {
                        goto resume;
                    }
                    else if (next && currentstrucstep >= maxstrucstep)
                    {
                        next = false;
                        paused = false;
                    }
                    yield return null;
                }

                if (arr3[j] < arr3[iMin])
                {
                    iMin = j;
                    iMin_Text.text = "iMin = " + iMin.ToString();
                    Display(arr3, arr3.Count, i, j, iMin);
                    Step.text = "iMin = " + j;
                    if (!manual)
                    {
                        yield return new WaitForSeconds(speed);
                    }
                }

                if (manual)
                {
                    paused = true;
                }
                while (paused && manual)
                {
                    if (previous && currentstrucstep > 0)
                    {
                        goto resume;
                    }
                    if (next && currentstrucstep < maxstrucstep)
                    {
                        goto resume;
                    }
                    else if (next && currentstrucstep >= maxstrucstep)
                    {
                        next = false;
                        paused = false;
                    }
                    yield return null;
                }
            }
            if (iMin != i)
            {
                int temp = arr3[i];
                arr3[i] = arr3[iMin];
                arr3[iMin] = temp;

                Step.text = "Swap i = " + arr3[i] + " and iMin = " + arr3[iMin];
                m_selectionAni.ShowGraph(arr3);
                Display(arr3, arr3.Count, iMin, j, i);
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
                    if (previous && currentstrucstep > 0)
                    {
                        goto resume;
                    }
                    if (next && currentstrucstep < maxstrucstep)
                    {
                        goto resume;
                    }
                    else if (next && currentstrucstep >= maxstrucstep)
                    {
                        next = false;
                        paused = false;
                    }
                    yield return null;
                }
            }
        }
        Step.text = "Finished";
        Display(arr3, arr3.Count, -1, -1, -1);
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        m_selectionAni.ShowGraph(arr3);
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
        }
        running = false;
    }

    public void PressedPrevious()
    {
        arr3.Clear();
        for (int i = 0; i < structarr[currentstrucstep].oldarr.Count; i++)
        {
            arr3.Add(structarr[currentstrucstep].oldarr[i]);
        }
    }    
    
    public void PressedNext()
    {
        arr3.Clear();
        for (int i = 0; i < structarr[currentstrucstep].oldarr.Count; i++)
        {
            arr3.Add(structarr[currentstrucstep].oldarr[i]);
        }
    }

}
