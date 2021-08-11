﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortInteractive1 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;

    public Material Donesort;//blue
    public Material NextSort;//red for swaps
    public Material Unsorted;//white
    public Material Recurs;//yellow for recursion
    public Material Goodmat;//green for pivot

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;
    public NonVRControlls m_vRController_2;

    public int NextSmallesIndex;
    public int IndexToSwap;

    Coroutine co;
    Coroutine sho;
    Coroutine go;

    public int speed;
    public int smooth;

    private Vector2 targetTransform;
    private Vector2 targetTransform2;
    private int pi;

    public int currentSmallestIndex;
    public int nextToSort;
    public float dist1;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;
    //public GameObject arrow;

    public bool waitingforswap;
    public bool IsTestMode;
    
    public GameObject igo;
    public GameObject jgo;
    public GameObject pigo;

    private void Start()
    {
        //previouspi = new int[9];
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        incorretAnswers = 0;
        corretAnswers = 0;
        numofGames = 0;
        //arrow.SetActive(false);
        //Begin();
    }

    private void OnEnable()
    {
        //Begin();
    }

    public void Begin()
    {
        try
        {
            StopCoroutine(co);
            StopCoroutine(sho);
            StopCoroutine(go);
            updatePos();
        }
        catch { }
        if (IsTestMode)
        {
            numofGames += 1;
            numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        }
        Step.text = "Welcome!  This tutorial is designed to teach you play this interactive minigame." + "\n\nIf the block is green it is the pivot and will be used to test our array for swaps.";
        for (int i = 0; i < 9; i++)
        {
            int n = Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
            b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        }
        //b[0].GetComponentInChildren<Text>().text = "1";
        updatePos();
        sorted = false;
        co = StartCoroutine(Quickchecksort());
    }

    public void Update()
    {
        if (sorted)
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Quick Sort Operates." + "\nThe Algorithm repeatedly sorts the array around the pivot.  If a value is less than the pviot it will remain on the left branch, otherwise values are compared and swapped until the array is sorted";
            sorted = false;
        }

        if(waitingforswap)//works
        {
            dist1 = Vector3.Distance(b[nextToSort].transform.position, b[currentSmallestIndex].transform.position);
            if (dist1 < .04)
            {
                Debug.Log("Swap");
                SwapValues(nextToSort, currentSmallestIndex);
                waitingforswap = false;
            }

            for (int i = 0; i < b.Count; i++)
            {
                if (i != nextToSort && i != currentSmallestIndex)
                {
                    float dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].transform.position);
                    float dist3 = Vector3.Distance(b[currentSmallestIndex].transform.position, b[i].transform.position);
                    if (dist2 < .04 || dist3 < .04)
                    {
                        m_vRController_1.downR = false;
                        m_vRController_1.downL = false;
                        m_vRController_2.down = false;
                        Step.text = "Incorrect.  The block you attempted to swap was Incorrect.";
                        if (IsTestMode)
                        {
                            incorretAnswers += 1;
                            incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                        }
                        updatePos();
                    }

                }
            }
        }
    }

    public void SwapValues(int i, int j)
    {
        m_vRController_1.downR = false;
        m_vRController_1.downL = false;
        m_vRController_2.down = false;

        Text ti = b[i].GetComponentInChildren<Text>();
        Text tj = b[j].GetComponentInChildren<Text>();

        string n = ti.text;

        ti.text = tj.text;
        tj.text = n;
        Step.text = "Correct! " + ti.text + " and " + tj.text + " will swap and index " + i + " will be locked.";
        if (IsTestMode)
        {
            corretAnswers += 1;
            corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();
        }
        if (!IsTestMode)
        {
            b[i].GetComponentInChildren<MeshRenderer>().material = Recurs;
            b[j].GetComponentInChildren<MeshRenderer>().material = Recurs;
        }
        updatePos();
    }

    public IEnumerator Quickchecksort()
    {
        yield return go = StartCoroutine(quickSort(0, b.Count - 1));
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

            yield return sho = StartCoroutine(partition(l, h));
            yield return sho = StartCoroutine(quickSort(l, pi - 1));
            yield return sho = StartCoroutine(quickSort(pi + 1, h));
        }
    }

    public IEnumerator partition(int l, int h)
    {
        if (!IsTestMode)
        {
            igo.SetActive(true);
            jgo.SetActive(true);
            pigo.SetActive(true);
        }
        else
        {
            igo.SetActive(false);
            jgo.SetActive(false);
            pigo.SetActive(false);
        }    
        float.TryParse(b[h].GetComponentInChildren<Text>().text, out float pivot);
        int i = (l - 1);
        for (int j = l; j <= h - 1; j++)
        {
            if (!IsTestMode)
            {
                if (i >= 0)
                {
                    igo.transform.position = new Vector3(b[i].transform.position.x, b[i].transform.position.y + .2f, b[i].transform.position.z);
                }
                else
                {
                    igo.transform.position = new Vector3(b[0].transform.position.x - .2f, b[0].transform.position.y + .2f, b[0].transform.position.z);
                }
                jgo.transform.position = new Vector3(b[j].transform.position.x, b[j].transform.position.y + .2f, b[j].transform.position.z);
                pigo.transform.position = new Vector3(b[h].transform.position.x, b[h].transform.position.y + .2f, b[h].transform.position.z);
            }

            //yield return new WaitForSeconds(1);
            float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
            if (temp < pivot)
            {
                i++;

                if (!IsTestMode)
                {
                    igo.transform.position = new Vector3(b[i].transform.position.x, b[i].transform.position.y + .2f, b[i].transform.position.z);
                }

                nextToSort = i;
                currentSmallestIndex = j;
                waitingforswap = true;
                if (nextToSort == currentSmallestIndex)
                {
                    waitingforswap = false;    
                }

                if (!IsTestMode && waitingforswap)
                {
                    b[nextToSort].GetComponentInChildren<MeshRenderer>().material = NextSort;
                    b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = NextSort;
                }
                while (waitingforswap)
                {
                    yield return null;
                }
                if (!IsTestMode)
                {
                    b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Recurs;
                    b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Recurs;
                }
            }
        }

        nextToSort = i+1;
        currentSmallestIndex = h;
        waitingforswap = true;
        if (nextToSort == currentSmallestIndex)
        {
            waitingforswap = false;
        }
        if (!IsTestMode && waitingforswap)
        {
            b[nextToSort].GetComponentInChildren<MeshRenderer>().material = NextSort;
            b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = NextSort;
        }
        while (waitingforswap)
        {
            yield return null;
        }
        if (!IsTestMode)
        {
            b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Recurs;
            b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Recurs;
        }
        pi = i + 1;
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

    public void EnableTrigger(int l, int h)
    {
        for (int i = 0; i < b.Count; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                if (!IsTestMode)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                }
            }
            else
            {
                if (i == pi)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    if (!IsTestMode)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Goodmat;
                    }
                    //previouspi[i] = 1;
                }
                else if (i >= l && i <= h)
                {
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    if (!IsTestMode)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Recurs;
                    }
                }
                else
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
        }
    }
}
