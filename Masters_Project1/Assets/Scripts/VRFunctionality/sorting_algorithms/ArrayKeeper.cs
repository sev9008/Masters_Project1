﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayKeeper : MonoBehaviour
{
    public List<int> arr;
    public Text Txt_Text;
    public int size;
    public selsort_arrayholder m_selsort_Arrayholder;

    public void push(int txt_to_push)
    {
        if (size < 30)
        {
            arr.Add(txt_to_push);
            size += 1;
            Display();
        }
        //if (!m_selsort_Arrayholder.running)
        //{

        //}
    }

    public void pop()
    {
        if (size > 0)
        {
            arr.RemoveAt(size - 1);
            size -= 1;
            Display();
        }
        //if (!m_selsort_Arrayholder.running)
        //{

        //}
    }

    public void randomNum()
    {
        arr.Clear();
        size = 30;
        for (int i = 0; i < size; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            arr.Add(n);
        }
        Display();
        //if (!m_selsort_Arrayholder.running)
        //{

        //}
    }

    public void Display()
    {
        Txt_Text.text = "";
        int Tmpsize = size - 1;
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                Txt_Text.text += "a["  + Tmpsize + "] = " + arr[i].ToString();
            }
            else if (i > 0)
            {
                Txt_Text.text += ", " + arr[i].ToString();
            }
        }
    }
}
