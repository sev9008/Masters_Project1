using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

public class BubbleInteractive1 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int speed;
    public int smooth;

    public int currentSmallestIndex;
    public int nextToSort;
    private float dist1;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;
    public GameObject arrow;

    public bool IsTestMode;//controls if the game is in test mode or not

    public bool waitingforswap;

    //reset or initalize some variables on start
    private void Start()
    {
        corretAnswersText.GetComponentInChildren<Text>().text = "0";
        incorretAnswersText.GetComponentInChildren<Text>().text = "0";
        numofGamesText.GetComponentInChildren<Text>().text = "0";
        incorretAnswers = 0;
        corretAnswers = 0;
        numofGames = 0;
        arrow.SetActive(false);
    }

    //when the player enters the room generate an array.
    private void OnEnable()
    {
        Begin();
    }

    //begin the pre algorithm phase.  this is where we will generate the array and reset the gamestate.  THis will also be called when restart or begin test is called
    public void Begin()
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
        StartCoroutine(Bubblechecksort());
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
                        Debug.Log(dist2 + "   " + dist3);
                        Debug.Log(currentSmallestIndex + nextToSort + i);

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
    public void updatePos()//reset the block positions if they have been moved
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].GetComponent<RectTransform>().anchoredPosition = pos[i].GetComponent<RectTransform>().anchoredPosition;
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
        }
    }

    public IEnumerator Bubblechecksort()//this is my response to the halting problem.  just put it in a coroutine 
    {
        Step.text = "To perform Bubble Sort go through the array from elft to right.  If the left value is larger than the right value swap them, and perform the same steps for the next index.";

        yield return StartCoroutine(BubbleSort());//this will stop this coroutine until the new function has finished.  It is extremely helpful for when you want to know exactly when a function ends
        updatePos();
        sorted = true;
        EnableTrigger(-1,-1);
    }
    public IEnumerator BubbleSort()//this is the holy grail.  jk its just the bubble sort algorithm.  with some added code
    {
        bool swapped = false;
        int i, j;
        for (i = 0; i < b.Count - 1; i++)
        {
            for (j = 0; j < b.Count - i - 1; j++)
            {
                float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
                float.TryParse(b[j+1].GetComponentInChildren<Text>().text, out float temp2);
                if (temp > temp2)
                {
                    EnableTrigger(b.Count - i, j);//this will change the material of the objects to swap
                    nextToSort = j;
                    currentSmallestIndex = j+1;
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
                    swapped = true;
                }
            }

            if (swapped == false)
            {
                break;
            }
        }
    }
    public void EnableTrigger(int maxsort , int j)//this function honestly still confuses me sometimes.  This will change specific components based on their values.  
    {//let it be noted this function used to ahve  amuch more important role.  however now it most likely doesnt do anything too crazy.
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
                if (i == j)
                {
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    if (!IsTestMode)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    }
                }
                else if (i >= maxsort)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    if (!IsTestMode)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                    }
                }
                else
                {
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
        }
    }
}
