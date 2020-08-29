﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameController : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;
    public Material Donesort;
    public Material Nextsort;
    public Material nextLine;
    public Material Transparent;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int NextSmallesIndex;
    public int IndexToSwap;

    Coroutine co;
    Coroutine sho;

    public int speed;
    public int smooth;

    private Vector2 targetTransform;
    private Vector2 targetTransform2;
    private int pi;

    private int[] previouspi;

    public int currentSmallestIndex;
    public int nextToSort;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;

    public bool waitingforswap;
    private void Start()
    {
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        corretAnswers = 0;
        incorretAnswers = 0;
        numofGames = 0;
    }

    private void OnEnable()
    {
        previouspi = new int[9];
        Begin();
    }

    public void Begin()
    {
        numofGames += 1;
        numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            previouspi[i] = 0;
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
            b[i].GetComponent<BlockParent>().gravity = true;
        }
        //b[0].GetComponentInChildren<Text>().text = "1";
        updatePos();
        sorted = false;
        StartCoroutine(Quickchecksort());
    }

    public void Update()
    {
        if (sorted)
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            sorted = false;
        }

        if (waitingforswap)
        {
            for (int i = nextToSort + 1; i < b.Length; i++)
            {
                if (i != currentSmallestIndex)
                {
                    float dist3 = Vector3.Distance(b[nextToSort].transform.position, b[i].transform.position);
                    float dist4 = Vector3.Distance(b[i].transform.position, b[nextToSort].transform.position);
                    float dist5 = Vector3.Distance(b[i].transform.position, b[currentSmallestIndex].transform.position);
                    float dist6 = Vector3.Distance(b[currentSmallestIndex].transform.position, b[i].transform.position);
                    if ((dist3 < .5 && dist4 < .5) || (dist5 < .5 && dist6 < .5))
                    {
                        m_vRController_1.downR = false;
                        m_vRController_1.downL = false;
                        Step.text = "Incorrect.  The block you attempted to swap was not the smallest value in the unsorted array.";
                        incorretAnswers += 1;
                        incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                        updatePos();
                    }
                }
                else
                {
                    float dist1 = Vector3.Distance(b[nextToSort].transform.position, pos[currentSmallestIndex].transform.position);
                    float dist2 = Vector3.Distance(b[nextToSort].transform.position, pos[currentSmallestIndex].transform.position);
                    if (dist1 < .5 && dist2 < .5)
                    {
                        Debug.Log("Swap");
                        SwapValues(nextToSort, currentSmallestIndex);
                        waitingforswap = false;
                    }
                }
            }
        }
        corretAnswersText.text = corretAnswers.ToString();
        incorretAnswersText.text = incorretAnswers.ToString();
        numofGamesText.text = numofGames.ToString();
    }
    public void SwapValues(int i, int j)
    {
        m_vRController_1.downR = false;
        m_vRController_1.downL = false;

        Text ti = b[i].GetComponentInChildren<Text>();
        Text tj = b[j].GetComponentInChildren<Text>();

        string n = ti.text;

        ti.text = tj.text;
        tj.text = n;
        Step.text = "Correct! " + ti.text + " and " + tj.text + " will swap and index " + i + " will be locked.";
        corretAnswers += 1;
        corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();

        updatePos();
    }

    public IEnumerator Quickchecksort()
    {
        yield return StartCoroutine(quickSort(0, b.Length - 1));
        updatePos();
        sorted = true;
        EnableTrigger(0, 0);
    }

    public IEnumerator quickSort(int l, int h)
    {
        if (l < h)
        {
            pi = h;
            EnableTrigger(l, h);

            yield return StartCoroutine(partition(l, h));
            yield return StartCoroutine(quickSort(l, pi - 1));
            yield return StartCoroutine(quickSort(pi + 1, h));
        }
    }

    public IEnumerator partition(int l, int h)
    {
        float.TryParse(b[h].GetComponentInChildren<Text>().text, out float pivot);
        int i = (l - 1);
        for (int j = l; j <= h - 1; j++)
        {
            float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
            if (temp < pivot)
            {
                i++;

                nextToSort = i;
                currentSmallestIndex = j;
                waitingforswap = true;
                if (nextToSort == currentSmallestIndex)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }
            }
        }

        nextToSort = i + 1;
        currentSmallestIndex = h;
        waitingforswap = true;
        if (nextToSort == currentSmallestIndex)
        {
            waitingforswap = false;
        }
        while (waitingforswap)
        {
            yield return null;
        }

        b[i + 1].GetComponent<BoxCollider>().enabled = false;
        b[i + 1].GetComponent<Rigidbody>().isKinematic = true;
        b[i + 1].GetComponent<BlockParent>().enabled = false;
        pos[i + 1].GetComponent<BoxCollider>().enabled = false;
        pos[i + 1].GetComponentInChildren<MeshRenderer>().enabled = false;
        pos[i + 1].GetComponentInChildren<MeshRenderer>().material = Donesort;
        b[i+1].GetComponent<BlockParent>().gravity = false;

        previouspi[i + 1] = 1;
        pi = i + 1;
    }

    public void updatePos()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
        }
    }

    public void EnableTrigger(int l, int h)
    {
        for (int i = 0; i < b.Length; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<Rigidbody>().isKinematic = true;
                b[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                b[i].GetComponent<BlockParent>().gravity = false;
            }
            else
            {
                if (previouspi[i] == 1)
                { }
                else if (i == pi)
                {
                    b[i].GetComponent<BlockParent>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<Rigidbody>().isKinematic = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    b[i].GetComponent<BlockParent>().gravity = true;
                }
                else if (i >= l && i <= h)
                {
                    b[i].GetComponent<BlockParent>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<Rigidbody>().isKinematic = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = nextLine;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    b[i].GetComponent<BlockParent>().gravity = true;
                }
                else
                {
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<Rigidbody>().isKinematic = true;
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Transparent;
                    b[i].GetComponent<BlockParent>().gravity = false;
                }
            }
        }
    }
}
