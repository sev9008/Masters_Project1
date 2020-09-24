using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortInteractive1 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> bclone;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Rmat;
    public Material Unsorted;
    public Material Lmat;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int speed;
    public int smooth;

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

    public bool waitingforswap2;
    public int MergeLIndex;
    public int MergeRIndex;

    public GameObject[] L;
    public GameObject[] R;
    public bool Larray;

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
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Merge Sort." + "\nIf a block is green It must be swapped with the other green block.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
            bclone[i].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
        }
        updatePos();
        sorted = false;
        StartCoroutine(Mergechecksort());
    }

    public void Update()
    {
        if (sorted)
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Merge Sort Operates." + "\nThe Algorithm splits the array into halves and sorts each half one at a time.";
            sorted = false;
        }

        if (waitingforswap2)
        {
            for (int i = 0; i < b.Count; i++)
            {
                if (i == MergeLIndex || i == MergeRIndex)//test to see if the palyer clicked the correct indexes
                {
                    if (b[MergeLIndex].GetComponent<MergeInter1_RecursionBlockTEst>().pressed && b[MergeRIndex].GetComponent<MergeInter1_RecursionBlockTEst>().pressed)//correct
                    {
                        b[MergeLIndex].GetComponent<MergeInter1_RecursionBlockTEst>().pressed = false;
                        b[MergeRIndex].GetComponent<MergeInter1_RecursionBlockTEst>().pressed = false;
                        EnableTrigger(-1, -1);
                        corretAnswers += 1;
                        Step.text = "The block you attempted to selected was correct.";
                        waitingforswap2 = false;
                        return;
                    }
                }
                else if(i != MergeLIndex && i != MergeRIndex)//test to see if the palyer clicked the incorrect indexes
                {
                    if (b[i].GetComponent<MergeInter1_RecursionBlockTEst>().pressed)//incorrect
                    {
                        b[i].GetComponent<MergeInter1_RecursionBlockTEst>().pressed = false;
                        EnableTrigger(-1, -1);
                        incorretAnswers += 1;
                        Step.text = "The block you attempted selected was incorrect.";
                        waitingforswap2 = false;
                        return;
                    }
                }
            }
        }

        if (waitingforswap && ((L.Length > 0 && Larray) || (R.Length > 0 && !Larray)))
        {
            float dist2;
            if (Larray)
            {
                Step.text = "The Algorithm is ready to swap values.  Perform the swap.";
                dist1 = Vector3.Distance(b[nextToSort].transform.position, L[currentSmallestIndex].transform.position);
                if (dist1 < .04)
                {
                    Debug.Log("Swap");
                    m_vRController_1.downR = false;
                    m_vRController_1.downL = false;
                    Step.text = "The block you attempted to swap was correct.";
                    corretAnswers += 1;
                    corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();
                    b[nextToSort].GetComponentInChildren<Text>().text = L[currentSmallestIndex].GetComponentInChildren<Text>().text;
                    waitingforswap = false;
                    updatePos();
                    return;
                }
                else 
                {
                    for (int i = 0; i < b.Count; i++)
                    {
                        dist2 = Vector3.Distance(b[i].transform.position, L[currentSmallestIndex].transform.position);
                        if (dist2 < .04)
                        {
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
                            incorretAnswers += 1;
                            incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                            updatePos();
                            return;
                        }
                    }
                }
            }
            else
            {
                dist1 = Vector3.Distance(b[nextToSort].transform.position, R[currentSmallestIndex].transform.position);
                if (dist1 < .04)
                {
                    Debug.Log("Swap");
                    m_vRController_1.downR = false;
                    m_vRController_1.downL = false;
                    corretAnswers += 1;
                    corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();
                    b[nextToSort].GetComponentInChildren<Text>().text = R[currentSmallestIndex].GetComponentInChildren<Text>().text;
                    waitingforswap = false;
                    updatePos();
                    return;
                }
                else
                {
                    for (int i = 0; i < b.Count; i++)
                    {
                        dist2 = Vector3.Distance(b[i].transform.position, R[currentSmallestIndex].transform.position);
                        if (dist2 < .04)
                        {
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "Incorrect.  The block you attempted to swap was incorrect.";
                            incorretAnswers += 1;
                            incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                            updatePos();
                            return;
                        }
                    }
                }
            }
        }
    }

    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].GetComponent<RectTransform>().anchoredPosition = pos[i].GetComponent<RectTransform>().anchoredPosition;
            bclone[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(pos[i].GetComponent<RectTransform>().anchoredPosition.x, pos[i].GetComponent<RectTransform>().anchoredPosition.y + 50);
            b[i].transform.position = pos[i].transform.position;
            //bclone[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            bclone[i].transform.rotation = pos[i].transform.rotation;
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
        }
    }

    public IEnumerator Mergechecksort()
    {
        yield return StartCoroutine(mergeSort(0, b.Count - 1));
        for (int i = 0; i < 9; i++)
        {
            b[i].GetComponent<RectTransform>().anchoredPosition = pos[i].GetComponent<RectTransform>().anchoredPosition;
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
        }
        sorted = true;
    }

    public IEnumerator mergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            //Debug.Log("l= " + l + "m= " + m + "r= " + r + "");

            EnableTrigger(-1, -1);
            MergeLIndex = l;
            MergeRIndex = r;
            waitingforswap2 = true;
            for (int i = 0; i < bclone.Count; i++)//disable all bclones since they are not needed here
            {
                bclone[i].SetActive(false);
                bclone[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            while (waitingforswap2)
            {
                yield return null;
            }
            yield return mergeSort(l, m);

            EnableTrigger(-1, -1);
            MergeLIndex = l;
            MergeRIndex = m;
            waitingforswap2 = true;
            for (int i = 0; i < bclone.Count; i++)//disable all bclones since they are not needed here
            {
                bclone[i].SetActive(false);
                bclone[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            while (waitingforswap2)
            {
                yield return null;
            }
            yield return mergeSort(m + 1, r);

            EnableTrigger(-1, -1);
            MergeLIndex = -l;
            MergeRIndex = -r;
            waitingforswap2 = true;
            for (int i = 0; i < bclone.Count; i++)//disable all bclones since they are not needed here
            {
                bclone[i].SetActive(false);
                bclone[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            while (waitingforswap2)
            {
                yield return null;
            }
            yield return merge(l, m, r);
        }
    }

    public IEnumerator merge(int l, int m, int r)
    {
        EnableTrigger(l, r);
        float temp, temp2;
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        L = new GameObject[n1];
        R = new GameObject[n2];
        for (i = 0; i < n1; i++)
        {
            L[i] = bclone[l + i];
            bclone[l + i].SetActive(true);
            bclone[l + i].GetComponentInChildren<MeshRenderer>().material = Lmat;
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = bclone[m + 1 + j];
            bclone[m + 1 + j].SetActive(true);
            bclone[m + 1 + j].GetComponentInChildren<MeshRenderer>().material = Rmat;
        }
        i = 0;
        j = 0;
        k = l;
        yield return new WaitForSeconds(speed);
        while (i < n1 && j < n2)
        {
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out temp2);

            if (temp <= temp2)
            {
                nextToSort = k;
                currentSmallestIndex = i;
                Larray = true;
                waitingforswap = true;

                float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
                float.TryParse(L[i].GetComponentInChildren<Text>().text, out temp2);
                if (temp == temp2)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }

                i++;
            }
            else if (temp != temp2)
            {
                nextToSort = k;
                currentSmallestIndex = j;
                Larray = false;
                waitingforswap = true;

                float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
                float.TryParse(R[j].GetComponentInChildren<Text>().text, out temp2);
                if (temp == temp2)
                {
                    waitingforswap = false;
                }
                while (waitingforswap)
                {
                    yield return null;
                }
                j++;
            }
            EnableTrigger(l, r);
            updatePos();
            k++;
        }
        while (i < n1)
        {
            nextToSort = k;
            currentSmallestIndex = i;
            Larray = true;
            waitingforswap = true;

            float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out temp2);
            if (temp == temp2)
            {
                waitingforswap = false;
            }
            while (waitingforswap)
            {
                yield return null;
            }

            i++;
            k++;
            EnableTrigger(l, r);
            updatePos();
        }
        while (j < n2)
        {
            nextToSort = k;
            currentSmallestIndex = j;
            Larray = false;
            waitingforswap = true;

            float.TryParse(b[k].GetComponentInChildren<Text>().text, out temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out temp2);
            if (temp == temp2)
            {
                waitingforswap = false;
            }
            while (waitingforswap)
            {
                yield return null;
            }

            j++;
            k++;
            EnableTrigger(l, r);
            updatePos();
        }
        yield return new WaitForSeconds(speed);

        for (int x = 0; x < bclone.Count; x++)
        {
            //b[x].GetComponentInChildren<Text>().text = bclone[x].GetComponentInChildren<Text>().text;
            bclone[x].GetComponentInChildren<Text>().text = b[x].GetComponentInChildren<Text>().text;
            bclone[x].SetActive(false);
        }
        updatePos();
    }

    public void EnableTrigger(int l, int h)
    {
        //Debug.Log(l + " " + h);
        for (int i = 0; i < b.Count; i++)
        {
            if (i >= l && i <= h)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else if(i == -1 && h == -1)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            else
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponent<MoveInteractionBLock>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
        }
    }
}