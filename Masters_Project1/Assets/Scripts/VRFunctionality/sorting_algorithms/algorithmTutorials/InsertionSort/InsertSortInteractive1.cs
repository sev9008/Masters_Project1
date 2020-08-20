using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertSortInteractive1 : MonoBehaviour
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

    public int correctAnswers;
    public int incorretAnswers;
    public int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;

    public VRController_1 m_vRController_1;

    public GameObject arrow;

    public int NextSmallesIndex;
    public int IndexToSwap;
    public float dist1;


    private void Start()
    {
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        incorretAnswers = 0;
        correctAnswers = 0;
        numofGames = 0;
        arrow.SetActive(false);
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            IndexToSwap = 0; 
            NextSmallesIndex = 9;
        }
        else 
        {
            dist1 = Vector3.Distance(b[IndexToSwap].transform.position, b[NextSmallesIndex].transform.position);
            if (dist1 < .04)
            {
                Debug.Log("Swap");
                correctAnswers++;
                SwapValues();
                checksort();
            }
            for (int i = 0; i < b.Count; i++)
            {
                dist1 = Vector3.Distance(b[i].transform.position, b[NextSmallesIndex].transform.position);
                if (dist1 < .04 && i != NextSmallesIndex)
                {
                    Debug.Log("Swap");
                    incorretAnswers++;
                    updatePos();
                }

            }
        }
    }

    public void Begin()
    {
        numofGames += 1;
        numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
        }
        updatePos();
        sorted = false;
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
    public void checksort()
    {
        for (int i = 0; i < b.Count-1; i++)
        {
            int j = i + 1;
            Text ti = b[i].GetComponentInChildren<Text>();
            Text tj = b[j].GetComponentInChildren<Text>();
            int ni = Convert.ToInt32(ti.text);
            int mi = Convert.ToInt32(tj.text);
            if (ni > mi)
            {
                NextSmallesIndex = j;
                FindSwap();
                EnableTrigger();
                sorted = false;
                return;
            }
        }
        sorted = true;
    }

    public void FindSwap()
    {
        int tempindex = 0;
        for (int i = NextSmallesIndex-1; i >= 0; i--)
        {
            Text ti = b[i].GetComponentInChildren<Text>();
            Text tj = b[NextSmallesIndex].GetComponentInChildren<Text>();
            int ni = Convert.ToInt32(ti.text);
            int mi = Convert.ToInt32(tj.text);

            if (ni > mi)
            {
                tempindex = i;
            }
            if (ni < mi)
            {
                break;
            }
        }
        IndexToSwap = tempindex;
    }

    public void SwapValues()
    { 
        string tempstringvalue = b[NextSmallesIndex].GetComponentInChildren<Text>().text;
        for (int i = NextSmallesIndex; i > IndexToSwap-1; i--)
        {
            if (i == IndexToSwap)
            {
                b[i].GetComponentInChildren<Text>().text = tempstringvalue;
            }
            else
            {
                b[i].GetComponentInChildren<Text>().text = b[i - 1].GetComponentInChildren<Text>().text;
            }
        }
        updatePos();
    }
    public void EnableTrigger()
    {
        for (int i = 0; i < b.Count; i++)
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
                if (i == NextSmallesIndex)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                }
                else if (i < NextSmallesIndex)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}