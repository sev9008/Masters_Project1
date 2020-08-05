using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem.Sample;

public class SlesortInteractive1 : MonoBehaviour
{
    //public List<MyStruct> arr;
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;

    public bool sorted;

    public int step;

    public Text Step;


    public float currentSmallest;

    private void Start()
    {
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            EnableTrigger(0);
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates."  + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            sorted = false;
        }
    }

    public void Begin()
    {
        Step.text = "Welcome!  This interacvtive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        sorted = false;
        checksort();
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
            if (sorted)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == j - 1)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                }
                else if (i < j)
                {
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                }
                else
                {
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
        }

        //get the currect smallest value in the unsorted array
        currentSmallest = 101;
        for (int i = j; i < 9; i++)
        {
            float ti;
            float.TryParse(b[i].GetComponentInChildren<Text>().text, out ti);

            if ( currentSmallest > ti)
            {
                currentSmallest = ti;
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
        Step.text = "Correct! " + ti.text + " and " + tj.text + "will swap and index" + j + " will be locked.";

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

