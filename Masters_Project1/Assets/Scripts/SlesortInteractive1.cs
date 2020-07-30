using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem.Sample;

public class SlesortInteractive1 : MonoBehaviour
{
    public List<MyStruct> arr;
    public List<GameObject> b;
    public List<GameObject> pos;

    public bool sorted;

    public int step;

    public class MyStruct
    {
        public GameObject obj;
        public int value;
    }

    public void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();

        sorted = false;
        checksort();
        if (sorted)
        {
            Start();
        }
    }

    public void checksort()
    {
        step = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = i + 1; j < 9; j++)
            {
                Text ti = b[i].GetComponentInChildren<Text>();
                Text tj = b[j].GetComponentInChildren<Text>();
                int n = Convert.ToInt32(ti.text);
                int m = Convert.ToInt32(tj.text);

                if (n > m)
                {
                    sorted = false;
                    EnableTrigger(step);
                    return;
                }
            }
            step++;
        }
        sorted = true;
    }

    public void EnableTrigger(int j)
    {
        j += 1;
        for (int i = 0; i < 9; i++)
        {
            if (i == j - 1)
            {
                b[i].GetComponent<MoveInteractionBLock>().enabled = false;
            }
            else if (i < j)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                b[i].GetComponent<BoxCollider>().enabled = true;
                b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void SwapValues(int i, int j)
    {
        Text ti = b[i].GetComponentInChildren<Text>();
        Text tj = b[j].GetComponentInChildren<Text>();

        string n = ti.text;

        ti.text = tj.text;
        tj.text = n;
        checksort();
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

