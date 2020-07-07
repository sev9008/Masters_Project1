using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class ArrayKeeper : MonoBehaviour
{
    public List<int> arr;
    public Text Txt_Text;
    public int size;

    public void push(int txt_to_push)
    {
        arr.Add(txt_to_push);
        //arr.Sort();
        size += 1;
        Display();
    }

    public void pop()
    {
        arr.RemoveAt(size - 1);
        size -= 1;
        Display();
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
