using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem.Sample;
/// <summary>
/// this is the first of our interactive tutorials
/// this tutoiral is designed to test the user.
/// The tutorial will generate a 9 index long array and the user must perform the algorithm to the best of his ability.
/// 
/// this tutorial has a guidance mode and a test mode
/// 
/// guidance mode will tell the user what steps to take and give different information
/// 
/// test mode will not show the user anything.  the user must make the correct swaps and moves utilizing his own knowledge of the algorithm. 
/// </summary>

public class SlesortInteractive1 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material GoodSort;
    public Material Unsorted;

    public bool sorted;
    public Text Step;

    public VRController_1 m_vRController_1;
    public int currentSmallestIndex;
    public int nextToSort;
    private float dist1;

    Coroutine co;
    Coroutine sho;
    Coroutine lo;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;

    public int speed;
    public int smooth;
    public int tempnumi;
    public int tempnumj;

    public bool IsTestMode;//controls if the game is in test mode or not

    public bool waitingforswap;

    private void Start()
    {
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        incorretAnswers = 0;
        corretAnswers = 0;
        numofGames = 0;
    }
    private void OnEnable()
    {
        //Begin();
    }

    public void Update()//needs testing
    {
        if (sorted)//game is over you win.
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Bubble Sort Operates." + "\nThe Algorithm Swaps the largest vaues to mvoe them to the right and locks their positions.";
            sorted = false;
        }

        if (waitingforswap)//this will wait for the player to make a swap.  It is probably not the best implementation, but it seems to work almost flawlessly.
        {
            dist1 = Vector3.Distance(b[nextToSort].transform.position, b[currentSmallestIndex].transform.position);
            if (dist1 < .04)//measure the distance between the correct blocks.  if they touch then perform a swap
            {
                Debug.Log("Swap");
                SwapValues(nextToSort, currentSmallestIndex);
                waitingforswap = false;
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (i != nextToSort && i != currentSmallestIndex)//measure the distance between all incrorect blocks.  if they touch then it was an incorrect move.
                {
                    float dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].transform.position);
                    float dist3 = Vector3.Distance(b[currentSmallestIndex].transform.position, b[i].transform.position);
                    if (dist2 < .04 || dist3 < .04)
                    {
                        m_vRController_1.downR = false;
                        m_vRController_1.downL = false;//this will force the player to drop the block
                        Step.text = "Incorrect.  The block you attempted to swap was not the smallest value in the unsorted array.";
                        if (IsTestMode)
                        {
                            incorretAnswers += 1;//if test mode is on the increment incorrect answer
                            incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                        }
                        updatePos();//this will reset the block position
                    }
                }
            }
        }
    }

    public void Begin()//this will begin the algorithm and start swapping
    {
        StopAllCoroutines();
        if (IsTestMode)
        {
            numofGames += 1;
            numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        }
        Step.text = "Welcome!  This tutorial is designed to teach you play this interactive minigame." + "\nIf the block is Green it is Sorted and can not be interacted with." + "\nIf a block is white or non colored, It must be swapped with the smallest adjacent value from the unsorted array.";

        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
            b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        }
        updatePos();
        sorted = false;
        StartCoroutine(Selectionchecksort());
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
    public void EnableTrigger(int jval, int ival, int iMinVal)//will cahnge the material of certain blocks depending on their values
    {
        if (!IsTestMode)
        {

            for (int i = 0; i < b.Count; i++)
            {
                if (sorted)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                }
                else
                {
                    if (i == iMinVal || i == ival)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = GoodSort;//set imin to green
                    }

                    else if (i == jval)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;//set j and i to red
                    }

                    else if (i < ival && ival != -1)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;//set every sorted value to blue
                    }

                    else
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;//set unsorted to white
                    }
                }
            }
        }
    }
    public void SwapValues(int i, int j)//this will perform the swap if a correct swap ahs occured
    {
        m_vRController_1.downR = false;
        m_vRController_1.downL = false;

        if (!IsTestMode)//change the materials jsut in case 
        {
            b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        }

        Text ti = b[i].GetComponentInChildren<Text>();//swap the values of the clocks.
        Text tj = b[j].GetComponentInChildren<Text>();
        //i could ahve probably done something more intuitive here.  Instead of giving blocks the valuse and forcing them to be read ever time, it might have been a much better deciion to have an array that stores the values and then writes to the blocks whenever a swap is made.
        //if i ever make this game more effiecinet this is one of the things I should change.


        string n = ti.text;

        ti.text = tj.text;
        tj.text = n;
        Step.text = "Correct! " + ti.text + " and " + tj.text + " will swap and index " + i + " will be locked.";// give a little message to the player saying good job
        if (IsTestMode)
        {
            corretAnswers += 1;
            corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();
        }

        updatePos();
    }

    public IEnumerator Selectionchecksort()//begins the algorithm //this is my response to the halting problem.  just put it in a coroutine 
    {
        yield return sho = StartCoroutine(SelectionSort());//this will stop this coroutine until the new function has finished.  It is extremely helpful for when you want to know exactly when a function ends
        updatePos();
        sorted = true;
        EnableTrigger(-1, -1, -1);
    }

    public IEnumerator SelectionSort()//this is the holy grail.  jk its just the bubble sort algorithm.  with some added code
    {
        int i, j, iMin;
        for (i = 0; i < b.Count - 1; i++)
        {
            iMin = i;
            for (j = i + 1; j < b.Count; j++)
            {

                float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp1);
                float.TryParse(b[iMin].GetComponentInChildren<Text>().text, out float temp2);
                if (temp1 < temp2)
                {
                    iMin = j;
                }
            }
            if (iMin != i)
            {

                EnableTrigger(-1, i, iMin);
                nextToSort = i;
                currentSmallestIndex = iMin;
                waitingforswap = true;
                if (nextToSort == currentSmallestIndex)//this will tell our update function that its time to start checking for swaps
                {
                    waitingforswap = false;
                }
                else if (!IsTestMode)//if we arent in test mode then just tell the user what to swap
                {
                    b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                while (waitingforswap)//this will pause the coroutine until the correct swap is made.
                {
                    yield return null;
                }
            }
        }
    }
}
