using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool IsTestMode;

    public bool waitingforswap;

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
        if (sorted)
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Bubble Sort Operates." + "\nThe Algorithm Swaps the largest vaues to mvoe them to the right and locks their positions.";
            sorted = false;
        }

        if (waitingforswap)//works
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
    public void SwapValues(int i, int j)
    {
        m_vRController_1.downR = false;
        m_vRController_1.downL = false;

        if (!IsTestMode)
        {
            b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        }

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

    public IEnumerator Bubblechecksort()
    {
        Step.text = "To perform Bubble Sort go through the array from elft to right.  If the left value is larger than the right value swap them, and perform the same steps for the next index.";

        yield return StartCoroutine(BubbleSort());
        updatePos();
        sorted = true;
        EnableTrigger(-1,-1);
    }
    public IEnumerator BubbleSort()
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
                    EnableTrigger(b.Count - i, j);
                    nextToSort = j;
                    currentSmallestIndex = j+1;
                    waitingforswap = true;
                    if (nextToSort == currentSmallestIndex)
                    {
                        waitingforswap = false;
                    }
                    else if (!IsTestMode)
                    {
                        b[currentSmallestIndex].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                        b[nextToSort].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    }
                    while (waitingforswap)
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
    public void EnableTrigger(int maxsort , int j)
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
