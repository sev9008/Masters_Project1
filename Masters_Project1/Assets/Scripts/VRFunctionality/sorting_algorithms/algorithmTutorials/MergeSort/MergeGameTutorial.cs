using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeGameTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;

    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;
    public Material nextLine;

    public bool sorted;

    public Text steptxt;

    public bool running;

    public int NextSmallesIndex;
    public int IndexToSwap;

    Coroutine co;
    Coroutine sho;

    public VRController_1 m_vRController_1;

    public int currentSmallestIndex;
    public int nextToSort;

    public int speed;
    public int smooth;
    public int tempnumi;
    public int tempnumj;

    public bool moving;
    public bool moving2;

    public Vector3 targetTransform;
    public Vector3 targetTransform2;

    public bool waitingforswap;

    private GameObject[] L;
    private GameObject[] R;
    private bool Larray;

    private void OnEnable()
    {
        moving = false;
        moving2 = false;
        Begin();
    }

    public void Begin()
    {
        try
        {
            moving = false;
            StopCoroutine(co);
            StopCoroutine(sho);
            updatePos();
        }
        catch { }

        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
            b[i].GetComponent<BlockParent>().gravity = false;
            b[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        steptxt.text = "Welcome!  This tutorial is designed to teach you play this interactive minigame." + "\n\nIf the block is Green it is Sorted and can not be interacted with." + "\n\nIf a block is white It must be swapped with the smallest value from the unsorted array.";
        updatePos();
        sorted = false;
        co = StartCoroutine(Mergechecksort());
    }

    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
        }
    }

    public void Update()
    {
        if (sorted)
        {
            //EnableTrigger();
            steptxt.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            IndexToSwap = 0;
            NextSmallesIndex = 9;
        }
        if (moving)
        {
            float.TryParse(b[tempnumi].GetComponentInChildren<Text>().text, out float temp);
            float temp2;
            if (Larray)
            {
                float.TryParse(L[tempnumj].GetComponentInChildren<Text>().text, out temp2);
            }
            else
            {
                float.TryParse(R[tempnumj].GetComponentInChildren<Text>().text, out temp2);
            }
            if (temp == temp2)
            {
                moving = false;
                return;
            }

            if (Larray)
            {
                L[tempnumj].transform.position = Vector3.MoveTowards(L[tempnumj].transform.position, targetTransform2, Time.deltaTime * smooth);

            }
            else
            {
                R[tempnumj].transform.position = Vector3.MoveTowards(R[tempnumj].transform.position, targetTransform2, Time.deltaTime * smooth);

            }

            if (Larray)
            {
                if (L[tempnumj].transform.position == targetTransform2)
                {
                    moving = false;
                }
            }
            else
            {
                if (R[tempnumj].transform.position == targetTransform2)
                {
                    moving = false;
                }
            }
        }
        if (moving2)
        {
            b[tempnumj].transform.position = Vector3.MoveTowards(b[tempnumj].transform.position, targetTransform2, Time.deltaTime * smooth);
            if (b[tempnumj].transform.position.y == targetTransform2.y)
            {
                moving2 = false;
            }
        }
    }

    public IEnumerator SwapAnimation(int i, int j)
    {
        tempnumj = j;
        tempnumi = i;
        float tran1;

        //move blocks down
        if (Larray)
        {
            tran1 = L[j].transform.position.y + 1f;
            targetTransform2 = new Vector3(L[j].transform.position.x, tran1, L[j].transform.position.z);
        }
        else
        {
            tran1 = R[j].transform.position.y + 1f;
            targetTransform2 = new Vector3(R[j].transform.position.x, tran1, R[j].transform.position.z);
        }
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        //move blocks into hover position over their traded block
        if (Larray)
        {
            GameObject p = b[i].GetComponent<BlockParent>().PairedPos;
            tran1 = p.transform.position.x;
            targetTransform2 = new Vector3(tran1, L[j].transform.position.y, L[j].transform.position.z);
        }
        else
        {
            GameObject p = b[i].GetComponent<BlockParent>().PairedPos;
            tran1 = p.transform.position.x;
            targetTransform2 = new Vector3(tran1, R[j].transform.position.y, R[j].transform.position.z);
        }
        moving = true;
        while (moving == true)
        {
            yield return null;
        }
    }
    public IEnumerator Fallin()
    {
        float p = pos[0].transform.position.y;
        for (int i = 0; i < 9; i++)
        {
            tempnumj = i;
            float s = b[i].transform.position.y;
            if (s != p)
            {
                targetTransform2 = new Vector3(b[i].transform.position.x, p, b[i].transform.position.z);
                moving2 = true;
                while (moving2 == true)
                {
                    yield return null;
                }
            }
        }
    }

    public IEnumerator Mergechecksort()
    {
        steptxt.text = "In this minigame you, the employee, must organize boxes by their value.  \n\nThis value is displayed above the boxes. \n\nYour boss wants you to organize them using Merge Sort!";

        yield return new WaitForSeconds(speed);

        steptxt.text = "This tutorial will go through the execution of the game with the correct moves.  \n\nTo swap a box, simply palce both boxes in their respective, correct, positions!";

        yield return new WaitForSeconds(speed);
        running = true;

        yield return StartCoroutine(mergeSort(0, b.Length - 1));
        updatePos();
        sorted = true;
        EnableTrigger(0, 9);
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
        updatePos();
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
                Larray = true;
                //L[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;

                yield return StartCoroutine(SwapAnimation(k, i));
                b[k] = L[i];
                i++;
            }
            else
            {
                Larray = false;
                //R[j].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;

                yield return StartCoroutine(SwapAnimation(k, j));
                b[k] = R[j];
                j++;
            }
            k++;
        }
        //EnableTrigger(l, r);
        while (i < n1)
        {
            Larray = true;
            //L[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            L[i].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;

            yield return StartCoroutine(SwapAnimation(k, i));
            b[k] = L[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            Larray = false;
            //R[j].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            R[j].GetComponent<BlockParent>().PairedPos.GetComponentInChildren<MeshRenderer>().material = Nextsort;
            yield return StartCoroutine(SwapAnimation(k, j));
            b[k] = R[j];
            j++;
            k++;
        }
        yield return StartCoroutine(Fallin());
        updatePos();
        yield return new WaitForSeconds(speed);
    }

    public void EnableTrigger(int l, int h)
    {
        //Debug.Log(l + " " + h);
        for (int i = 0; i < b.Length; i++)
        {
            if (i >= l && i <= h)
            {
                b[i].GetComponent<BlockParent>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponent<MeshRenderer>().enabled = true;
                pos[i].GetComponent<MeshRenderer>().material = Donesort;
            }
            else
            {
                b[i].GetComponent<BlockParent>().enabled = true;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponent<MeshRenderer>().enabled = false;
                pos[i].GetComponent<MeshRenderer>().material = Unsorted;
            }
        }
    }
}
