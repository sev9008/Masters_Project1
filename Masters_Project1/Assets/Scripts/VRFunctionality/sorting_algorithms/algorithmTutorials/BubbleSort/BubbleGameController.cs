using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleGameController : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Transparent;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int NextSmallesIndex;
    public int IndexToSwap;

    public int speed;
    public int smooth;

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
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
            b[i].GetComponent<BlockParent>().gravity = true;
        }
        //b[0].GetComponentInChildren<Text>().text = "1";
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
                        incorretAnswers++;
                        incorretAnswersText.text = "Incorrect Anwserws = " + incorretAnswers;
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
        corretAnswers++;
        corretAnswersText.text = "Incorrect Anwserws = " + corretAnswers;

        updatePos();
    }

    public void updatePos()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
        }
    }

    public void EnableTrigger(int maxsort, int j)
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
                if (i == j)
                {
                    b[i].GetComponent<BlockParent>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<Rigidbody>().isKinematic = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    b[i].GetComponent<BlockParent>().gravity = true;
                }
                else if (i >= maxsort)
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
                    b[i].GetComponent<BlockParent>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<Rigidbody>().isKinematic = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    b[i].GetComponent<BlockParent>().gravity = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Transparent;
                }
            }
        }
    }
    public IEnumerator Bubblechecksort()
    {
        yield return StartCoroutine(BubbleSort());
        updatePos();
        sorted = true;
        EnableTrigger(-1, -1);
    }
    public IEnumerator BubbleSort()
    {
        bool swapped = false;
        int i, j;
        for (i = 0; i < b.Length - 1; i++)
        {
            for (j = 0; j < b.Length - i - 1; j++)
            {
                float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
                float.TryParse(b[j + 1].GetComponentInChildren<Text>().text, out float temp2);
                if (temp > temp2)
                {
                    EnableTrigger(b.Length - i, j);

                    nextToSort = j;
                    currentSmallestIndex = j + 1;
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
}
