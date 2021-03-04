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

    public int corretAnswers;
    public int incorretAnswers;
    public int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;

    public VRController_1 m_vRController_1;

    public float currentSmallest;

    public GameObject arrow;

    public int currentSmallestIndex;
    public int nextToSort;
    public float dist1;
    public bool IsTestMode;

    private void Start()
    {
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        incorretAnswers = 0;
        corretAnswers = 0;
        numofGames = 0;
        arrow.SetActive(false);
        //Begin();
    }

    private void OnEnable()
    {
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            EnableTrigger(0);
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            sorted = false;
        }

        //this works
        dist1 = Vector3.Distance(b[nextToSort].transform.position, b[currentSmallestIndex].transform.position);
        if (dist1 < .04)
        {
            Debug.Log("Swap");
            SwapValues(nextToSort, currentSmallestIndex);
        }
        else
        {
            for (int i = 0; i < b.Count; i++)
            {
                if (i != nextToSort && i != currentSmallestIndex)
                {
                    float dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].transform.position);
                    float dist3 = Vector3.Distance(b[currentSmallestIndex].transform.position, b[i].transform.position);
                    if (dist2 < .04 || dist3 < .04)
                    {
                        Debug.Log(dist2 + "   " + dist3);
                        Debug.Log(currentSmallestIndex + nextToSort + i);

                        m_vRController_1.downR = false;
                        m_vRController_1.downL = false;
                        Step.text = "Incorrect.  The block you attempted to swap was not the smallest value in the unsorted array.";
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

    public void Begin()
    {
        if (IsTestMode)
        {
            numofGames += 1;
            numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        }
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
            b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;

        }
        //b[0].GetComponentInChildren<Text>().text = "1";
        updatePos();
        sorted = false;
        checksort();
    }

    public void checksort()//might want to change this in the future to amtch the actual algorithm, even though it takes less compuation.
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
                    nextToSort = step;
                    return;
                }
            }
            step++;
        }
        sorted = true;
        EnableTrigger(1);
    }

    public void EnableTrigger(int j)
    {
        j += 1;
        for (int i = 0; i < 9; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == j - 1)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    if (!IsTestMode)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    }
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;

                    //reset arrow position
                    arrow.transform.position = b[i].transform.position;
                    arrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(arrow.GetComponent<RectTransform>().anchoredPosition.x, arrow.GetComponent<RectTransform>().anchoredPosition.y + 40);
                }
                else if (i < j)
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
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
        }
        if (!sorted)
        {
            //get the currect smallest value in the unsorted array
            currentSmallest = 101;
            for (int i = j; i < 9; i++)
            {
                float ti;
                float.TryParse(b[i].GetComponentInChildren<Text>().text, out ti);

                if (currentSmallest > ti)
                {
                    currentSmallest = ti;
                    currentSmallestIndex = i;
                }
            }
            if (!IsTestMode)
            {
                b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            }
        }
    }

    public void SwapValues(int i, int j)
    {
        b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        m_vRController_1.downR = false;
        m_vRController_1.downL = false;

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

        updatePos();

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

