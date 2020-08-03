using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class selsort_arrayholder : MonoBehaviour
{
    public Text Txt_Text;
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
    public int CurrentStep;

    public int structarrLength;

    public List<int> arr3;

    public int tempcase;

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
    [SerializeField] private MyStruct struc;

    public void Update()
    {
        speed = slider.value;
        structarrLength = structarr.Count;
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
    /*
    public IEnumerator Selection()
    {
        structarr.Clear();
        CurrentStep = 0;
        running = true;
        m_selectionAni.ShowGraph(arr3);
        int i, j;
        int iMin;
        for (i = 0; i < arr3.Count - 1; i++)
        {
            //add our current step to a struct array
            var c = new MyStruct();
            structarr.Add(c);
            structarr[CurrentStep].oldarr = new List<int> (arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[CurrentStep].oldarr.Add(arr3[tempnum]);
            }
            structarr[CurrentStep].i_value = i;
            CurrentStep++;

            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count);

            iMin = i;

            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            Step.text = "increment i = " + i + ", reset iMin to i = " + iMin;
            yield return new WaitForSeconds(speed);
            while (paused)
            {
                yield return null;
            }

            for (j = i + 1; j < arr3.Count; j++)
            {
                if (arr3[j] < arr3[iMin])
                {
                    iMin = j;
                }

                Step.text = "i = " + arr3[i] + ", j = " + arr3[j] + ", min = " + iMin;
                image1.SetActive(false);
                image2.SetActive(true);
                image3.SetActive(false);
                while (paused)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(speed);
            }
            if (iMin != i)
            {
                int temp = arr3[i];
                arr3[i] = arr3[iMin];
                arr3[iMin] = temp;

                Step.text = "Swap " + arr3[i] + " and " + arr3[iMin];
                m_selectionAni.ShowGraph(arr3);
                Display(arr3, arr3.Count);
                image1.SetActive(false);
                image2.SetActive(false);
                image3.SetActive(true);
                while (paused)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(speed);
            }
        }
        Step.text = "Finished";
        Display(arr3, arr3.Count);
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        m_selectionAni.ShowGraph(arr3);
        yield return new WaitForSeconds(speed);
        running = false;
    }
    */
    public IEnumerator Selection()
    {
        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].oldarr.Clear();
            structarr[n].i_value = -1;
        }
            structarr.Clear();
        //structarr = new List<MyStruct>();
        CurrentStep = 0;
        running = true;
        m_selectionAni.ShowGraph(arr3);
        int i, j;
        int iMin;
        for (i = 0; i < arr3.Count - 1; i++)
        {
            resume:
            if (previous && CurrentStep > 1)
            {
                PressedPrevious();
                previous = false;
                i = structarr[CurrentStep].i_value;
                //CurrentStep--;
            }
            else if (next && CurrentStep <= structarr.Count - 1)
            {
                PressedNext();
                next = false;
                i = structarr[CurrentStep].i_value;
                //CurrentStep--;
            }
            else if (CurrentStep == structarr.Count)
            {
                Debug.Log("addnew");
                var c = new MyStruct();
                structarr.Add(c);
                structarr[CurrentStep].oldarr = new List<int>(arr3.Count);
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[CurrentStep].oldarr.Add(arr3[tempnum]);
                }
                structarr[CurrentStep].i_value = i;
                CurrentStep++;
            }
            else if (CurrentStep > structarr.Count || CurrentStep < 0)
            {
                Step.text = "ERROR";
                break;
            }
            else
            {
                CurrentStep++;
            }

            if(CurrentStep < structarr.Count)
            {
                Debug.Log("Replace");
                structarr[CurrentStep].oldarr.Clear();
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[CurrentStep].oldarr.Add(arr3[tempnum]);
                }
                structarr[CurrentStep].i_value = i;
            }
            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count);
            iMin = i;

            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            Step.text = "increment i = " + i + ", reset iMin to i = " + iMin;

            if (previous)
            {
                goto resume;
            }
            if (next)
            {
                goto resume;
            }
            yield return new WaitForSeconds(speed);
            if (previous)
            {
                goto resume;
            }
            if (next)
            {
                goto resume;
            }
            while (paused)
            {
                yield return null;
            }
            if (previous)
            {
                goto resume;
            }
            if (next)
            {
                goto resume;
            }

            for (j = i + 1; j < arr3.Count; j++)
            {
                if (arr3[j] < arr3[iMin])
                {
                    iMin = j;
                }

                Step.text = "i = " + arr3[i] + ", j = " + arr3[j] + ", min = " + iMin;
                image1.SetActive(false);
                image2.SetActive(true);
                image3.SetActive(false);

                if (previous)
                {
                    goto resume;
                }
                if (next)
                {
                    goto resume;
                }
                yield return new WaitForSeconds(speed);
                if (previous)
                {
                    goto resume;
                }
                if (next)
                {
                    goto resume;
                }
                while (paused)
                {
                    yield return null;
                }
                if (previous)
                {
                    goto resume;
                }
                if (next)
                {
                    goto resume;
                }

            }
            if (iMin != i)
            {
                int temp = arr3[i];
                arr3[i] = arr3[iMin];
                arr3[iMin] = temp;

                Step.text = "Swap " + arr3[i] + " and " + arr3[iMin];
                m_selectionAni.ShowGraph(arr3);
                Display(arr3, arr3.Count);
                image1.SetActive(false);
                image2.SetActive(false);
                image3.SetActive(true);

                if (previous)
                {
                    goto resume;
                }
                if (next)
                {
                    goto resume;
                }
                yield return new WaitForSeconds(speed);
                if (previous)
                {
                    goto resume;
                }
                if (next)
                {
                    goto resume;
                }
                while (paused)
                {
                    yield return null;
                }
                if (previous)
                {
                    goto resume;
                }
                if (next)
                {
                    goto resume;
                }
            }
        }
        Step.text = "Finished";
        Display(arr3, arr3.Count); 
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        m_selectionAni.ShowGraph(arr3);
        yield return new WaitForSeconds(speed);
        running = false;
    }

    public void PressedPrevious()
    {
        CurrentStep--;
        arr3.Clear();
        for (int i = 0; i < structarr[CurrentStep].oldarr.Count; i++)
        {
            arr3.Add(structarr[CurrentStep].oldarr[i]);
        }
    }    
    
    public void PressedNext()
    {
        CurrentStep++;
        arr3.Clear();
        for (int i = 0; i < structarr[CurrentStep].oldarr.Count; i++)
        {
            arr3.Add(structarr[CurrentStep].oldarr[i]);
        }
    }

}
