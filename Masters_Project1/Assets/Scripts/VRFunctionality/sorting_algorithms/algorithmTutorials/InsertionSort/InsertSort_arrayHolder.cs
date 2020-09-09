using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertSort_arrayHolder : MonoBehaviour
{
    //public int size;
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


    public List<int> arr3;

    public int currentstrucstep;
    public int maxstrucstep;

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

    public IEnumerator Insertion()
    {
        //clear the struct
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

        //int curSize = structarr.Count;
        int j, key;
        for (int i = 1; i < arr3.Count; ++i)
        {
            Debug.Log("?");
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
                yield return new WaitForSeconds(speed);
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
                yield return new WaitForSeconds(speed);
            }
            previous = false;
            next = false;
            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count);
            key = arr3[i];
            j = i - 1;

            Step.text = "Save " + arr3[i] + " as a key";
            image1.SetActive(true);
            image2.SetActive(false);
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

            while (j >= 0 && arr3[j] > key)
            {
                Step.text = "if " + arr3[j] + " > " + key + " \nThen set " + arr3[j + 1] + " to " + arr3[j];

                arr3[j + 1] = arr3[j];
                j--;

                image1.SetActive(false);
                image2.SetActive(true);
                image3.SetActive(false);
                Display(arr3, arr3.Count);


                yield return new WaitForSeconds(speed);
                if (previous)
                {
                    Step.text = "Please wait...";
                    previous = true;
                }
                if (next)
                {
                    Step.text = "Please wait...";
                    next = true;
                }

            }

            Step.text = "set " + arr3[j+1] + " to " + key;
            arr3[j + 1] = key;
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
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        Step.text = "Finished";
        Display(arr3, arr3.Count);
        m_selectionAni.ShowGraph(arr3);
        yield return new WaitForSeconds(speed);
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
    /*
    void insertionSort(int arr[], int n)
    {
        int i, key, j;
        for (i = 1; i < n; i++)
        {
            key = arr[i];
            j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }
    */
}
