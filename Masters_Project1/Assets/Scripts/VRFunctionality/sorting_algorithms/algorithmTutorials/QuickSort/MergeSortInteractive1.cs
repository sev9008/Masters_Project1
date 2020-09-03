using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortInteractive1 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;
    public Material nextLine;

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

    public int currentSmallestIndex;
    public int nextToSort;
    public float dist1;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;
    public GameObject arrow;

    public bool waitingforswap;

    private GameObject[] L;
    private GameObject[] R;
    private bool Larray;

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
        //b[0].GetComponentInChildren<Text>().text = "1";
        updatePos();
        sorted = false;
        StartCoroutine(Mergechecksort());
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
        Text tj;
        if (Larray)
        {
            tj = L[j].GetComponentInChildren<Text>();
        }
        else 
        {
            tj = R[j].GetComponentInChildren<Text>();
        }

        string n = ti.text;

        ti.text = tj.text;
        //tj.text = n;
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

    public IEnumerator Mergechecksort()
    {
        yield return StartCoroutine(mergeSort(0, b.Count - 1));
        updatePos();
        sorted = true;
        //EnableTrigger(0, 0);
    }

    public IEnumerator mergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            yield return mergeSort(l, m);
            yield return mergeSort(m + 1, r);
            yield return merge(l, m, r);
        }
    }

    public IEnumerator merge(int l, int m, int r)
    {
        EnableTrigger(l, r);
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        L = new GameObject[n1];
        R = new GameObject[n2];
        for (i = 0; i < n1; i++)
        {
            L[i] = b[l + i];
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = b[m + 1 + j];
        }
        i = 0;
        j = 0;
        k = l;
        yield return new WaitForSeconds(speed);
        while (i < n1 && j < n2)
        {
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out float temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out float temp2);

            if (temp <= temp2)
            {
                nextToSort = k;
                b[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;

                currentSmallestIndex = i;
                Larray = true;
                waitingforswap = true;
                if (nextToSort == currentSmallestIndex)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }

                //b[k] = L[i];
                i++;
            }
            else
            {
                nextToSort = k;
                currentSmallestIndex = j;
                Larray = false;
                waitingforswap = true;
                if (nextToSort == currentSmallestIndex)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }

                //b[k] = R[j];
                j++;
            }
            k++;
        }
        while (i < n1)
        {
            nextToSort = k;
            currentSmallestIndex = k;
            Larray = true;
            waitingforswap = true;
            if (nextToSort == currentSmallestIndex)
            {
                waitingforswap = false;
            }
            while (waitingforswap)
            {
                yield return null;
            }

            //b[k] = L[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            nextToSort = k;
            currentSmallestIndex = j;
            Larray = false;
            waitingforswap = true;
            if (nextToSort == currentSmallestIndex)
            {
                waitingforswap = false;
            }
            while (waitingforswap)
            {
                yield return null;
            }

            //b[k] = R[j];
            j++;
            k++;
        }
        updatePos();
        //yield return new WaitForSeconds(speed);
    }

    public void EnableTrigger(int l, int h)
    {
        Debug.Log(l + " " + h);
        for (int i = 0; i < b.Count; i++)
        {
            if (i >= l && i <= h)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else 
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
        }
    }
}