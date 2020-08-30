using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleGameTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;

    public Material Donesort;
    public Material Nextsort;
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
        co = StartCoroutine(Bubblechecksort());
    }
    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
        }
    }

    public void Update()
    {
        if (sorted)
        {
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
    public void EnableTrigger(int maxsort, int j)
    {
        for (int i = 0; i < b.Length; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponent<MeshRenderer>().enabled = true;
                pos[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == j)
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                else if (i >= maxsort)
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                }
                else
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = false;
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
                    yield return StartCoroutine(SwapAnimation(j, j + 1));
                    GameObject temp3 = b[j];
                    b[j] = b[j + 1];
                    b[j + 1] = temp3;
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
