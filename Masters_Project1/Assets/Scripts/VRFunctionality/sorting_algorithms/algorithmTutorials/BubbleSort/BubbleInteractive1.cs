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
        Begin();
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
        StartCoroutine(Bubblechecksort());
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
            for (int i = nextToSort + 1; i < b.Count; i++)
            {
                if (i != currentSmallestIndex)
                {
                    float dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].transform.position);
                    if (dist2 < .04)
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
                    dist1 = Vector3.Distance(b[nextToSort].transform.position, b[currentSmallestIndex].transform.position);
                    if (dist1 < .04)
                    {
                        Debug.Log("Swap");
                        SwapValues(nextToSort, currentSmallestIndex);
                        waitingforswap = false;
                    }
                }
            }
        }
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
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == j)
                {
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                else if (i >= maxsort)
                {
                    b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
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
