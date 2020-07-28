﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class selsort_arrayholder : MonoBehaviour
{
    //public List<int> arr;
    public Text Txt_Text;
    //public int size;
    public float waittime;
    public Text Step;
    public float speed;

    public Slider slider;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public bool paused;
    public bool running;

    public SelectionAni m_selectionAni;

    private void Start()
    {
        paused = false;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr, int size)
    {
        Txt_Text.text = "";
        int Tmpsize = size - 1;
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                Txt_Text.text += "a[" + Tmpsize + "] = " + arr[i].ToString();
            }
            else if (i > 0)
            {
                Txt_Text.text += ", " + arr[i].ToString();
            }
        }
    }

    public IEnumerator Selection(List<int> arr, int size)
    {
        running = true;
        m_selectionAni.ShowGraph(arr);
        int i, j;
        int iMin;
        for (i = 0; i < size - 1; i++)
        {
            iMin = i;
            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            Step.text = "increment i = " + i + ", reset iMin to i = " + iMin;
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
            for (j = i + 1; j < size; j++)
            {
                if (arr[j] < arr[iMin])
                {
                    iMin = j;
                }
                Step.text = "i = " + arr[i] + ", j = " + arr[j] + ", min = " + iMin;
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
                int temp = arr[i];
                arr[i] = arr[iMin];
                arr[iMin] = temp;
                Step.text = "Swap " + arr[i] + " and " + arr[iMin];
                m_selectionAni.ShowGraph(arr);
                Display(arr, size);
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
        Display(arr, size); 
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        m_selectionAni.ShowGraph(arr);
        yield return new WaitForSeconds(speed);
        running = false;
    }
}
