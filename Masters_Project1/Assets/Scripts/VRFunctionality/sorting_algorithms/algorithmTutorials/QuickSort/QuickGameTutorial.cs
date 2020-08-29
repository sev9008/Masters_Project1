using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickGameTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;

    public Material Donesort;
    public Material Nextsort;
    public Material nextLine;
    public Material Transparent;

    public Text steptxt;

    public bool sorted;
    public bool moving;
    public bool running;

    public int NextSmallesIndex;
    public int IndexToSwap;

    Coroutine co;
    Coroutine sho;

    public int speed;
    public int smooth;
    private int tempnumi;
    private int tempnumj;

    private Vector3 targetTransform;
    private Vector3 targetTransform2;

    private int pi;
    private int[] previouspi;

    private void OnEnable()
    {
        previouspi = new int[9];
        moving = false;
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
            previouspi[i] = 0;

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
        co = StartCoroutine(Quick());
    }
    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            //pos[i].GetComponent<MeshRenderer>().material = Transparent;
        }
    }

    public void Update()
    {
        if (sorted)
        {
            //EnableTrigger();
            //Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            IndexToSwap = 0;
            NextSmallesIndex = 0;
        }
        if (moving)
        {
            if (tempnumi == tempnumj)
            {
                moving = false;
            }
            b[tempnumi].transform.position = Vector3.MoveTowards(b[tempnumi].transform.position, targetTransform, Time.deltaTime * smooth);

            b[tempnumj].transform.position = Vector3.MoveTowards(b[tempnumj].transform.position, targetTransform2, Time.deltaTime * smooth);

            if (b[tempnumi].transform.position == targetTransform || b[tempnumj].transform.position == targetTransform2)
            {
                moving = false;
            }
        }
    }
    public IEnumerator SwapAnimation(int i, int j)
    {
        tempnumi = i;
        tempnumj = j;

        float tran3 = pos[i].transform.position.y + 1f;
        float tran4 = pos[j].transform.position.y - 1f;
        targetTransform = new Vector3(pos[i].transform.position.x, tran3, pos[i].transform.position.z);
        targetTransform2 = new Vector3(pos[j].transform.position.x, tran4, pos[i].transform.position.z);
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        tran3 = pos[i].transform.position.x;
        tran4 = pos[j].transform.position.x;
        targetTransform = new Vector3(tran4, b[i].transform.position.y, b[i].transform.position.z);
        targetTransform2 = new Vector3(tran3, b[j].transform.position.y, b[i].transform.position.z);
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        targetTransform = pos[tempnumj].transform.position;
        targetTransform2 = pos[tempnumi].transform.position;
        moving = true;
        while (moving == true)
        {
            yield return null;
        }
    }

    public IEnumerator Quick()
    {
        yield return StartCoroutine(quickSort(0, b.Length- 1));
        updatePos();
        sorted = true;
        EnableTrigger(0, 0);
    }
    public IEnumerator quickSort(int l, int h)
    {
        if (l < h)
        {
            pi = h;
            EnableTrigger(l, h);
            //yield return new WaitForSeconds(speed);

            yield return StartCoroutine(partition(l, h));
            yield return StartCoroutine(quickSort(l, pi - 1));
            yield return StartCoroutine(quickSort(pi + 1, h));
        }
    }

    public IEnumerator partition(int l, int h)
    {
        float.TryParse(b[h].GetComponentInChildren<Text>().text, out float pivot);
        int i = (l - 1);
        for (int j = l; j <= h - 1; j++)
        {
            float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
            if (temp < pivot)
            {
                i++;

                yield return StartCoroutine(SwapAnimation(i, j));
                GameObject temp3 = b[i];
                b[i] = b[j];
                b[j] = temp3;
            }
        }
        yield return StartCoroutine(SwapAnimation(i + 1, h));
        GameObject temp1 = b[i + 1];
        b[i + 1] = b[h];
        b[h] = temp1;

        b[i+1].GetComponent<BlockParent>().enabled = false;
        pos[i+1].GetComponent<BoxCollider>().enabled = false;
        pos[i+1].GetComponent<MeshRenderer>().enabled = true;
        pos[i+1].GetComponent<MeshRenderer>().material = Donesort;

        previouspi[i + 1] = 1;
        pi = i + 1;
        yield return new WaitForSeconds(speed);
    }

    public void EnableTrigger(int l, int h)
    {
        for (int i = 0; i < b.Length; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponent<MeshRenderer>().enabled = true;
                pos[i].GetComponent<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (previouspi[i] == 1)
                { }
                else if (i == pi)
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().material = Nextsort;
                }
                else if (i >= l && i <= h)
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().material = nextLine;
                }
                else
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = false;
                    pos[i].GetComponent<MeshRenderer>().material = Transparent;
                }
            }
        }
    }
}
