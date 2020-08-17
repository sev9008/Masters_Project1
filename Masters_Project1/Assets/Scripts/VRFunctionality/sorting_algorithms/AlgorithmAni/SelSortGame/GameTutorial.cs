using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTutorial : MonoBehaviour
{
    public GameObject[] block;
    public GameObject[] pos;

    private float currentSmallest;
    private int step;
    public int currentSmallestIndex;
    public int nextToSort;
    public int dontchange;
    public int speed;
    public float smooth;
    private int tempnumi;
    private int tempnumj;

    private bool sorted;

    public Material DoneSort;
    public Material NextSort;
    public Material Transparent;

    public Text steptxt;

    public bool moving;
    public bool running;


    public Coroutine co1;
    public Coroutine co2;

    private Vector3 targetTransform;
    private Vector3 targetTransform2;

    private void OnEnable()
    {
        Begin();
    }

    public void Begin()
    {
        try
        {
            moving = false;
            StopCoroutine(co1);
            StopCoroutine(co2);
            updatePos();
        }
        catch { }


        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = block[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            block[i].GetComponent<BlockParent>().PairedPos = pos[i];
            block[i].GetComponent<BlockParent>().gravity = false;
            block[i].GetComponent<Rigidbody>().isKinematic = true;
        }
        steptxt.text = "Welcome!  This tutorial is designed to teach you play this interactive minigame." + "\n\nIf the block is Green it is Sorted and can not be interacted with." + "\n\nIf a block is white It must be swapped with the smallest value from the unsorted array.";
        updatePos();
        sorted = false;
        checksort();
        co1 = StartCoroutine(SelectionInteractive2());

    }
    public void updatePos()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            block[i].transform.position = pos[i].transform.position;
            block[i].transform.rotation = pos[i].transform.rotation;
            pos[i].GetComponent<MeshRenderer>().material = Transparent;
        }
    }
    public IEnumerator SelectionInteractive2()
    {
        yield return new WaitForSeconds(10);

        steptxt.text = "In this minigame you, the employee, must organize boxes by their value.  \n\nThis value is displayed above the boxes. \n\nYour boss wants you to organize them using Selection Sort!";

        yield return new WaitForSeconds(10);

        steptxt.text = "This tutorial will go through the execution of the game with the correct moves.  \n\nTo swap a box, simply palce both boxes in their respective, correct, positions!";

        yield return new WaitForSeconds(5);



        running = true;
        int i, j;
        float n, m;
        int iMin = 0;
        for (i = 0; i < block.Length - 1; i++)
        {
            iMin = i;
            for (j = i + 1; j < block.Length; j++)
            {
                float.TryParse(block[j].GetComponentInChildren<Text>().text, out n);
                float.TryParse(block[iMin].GetComponentInChildren<Text>().text, out m);
                if (j != dontchange)
                {
                    pos[j].GetComponentInChildren<MeshRenderer>().material = Transparent;
                    //step.text = "Checking for new smallest";
                }
                if (n < m)
                {
                    if (iMin != i)
                    {
                        if (iMin != dontchange)
                        {
                            pos[iMin].GetComponentInChildren<MeshRenderer>().material = Transparent;
                        }
                    }
                    iMin = j;
                    pos[j].GetComponentInChildren<MeshRenderer>().material = NextSort;
                    //step.text = "Replace the current smallest value";
                }
                //yield return new WaitForSeconds(speed);
                if (pos[j].GetComponentInChildren<MeshRenderer>().material == Transparent)
                {
                    pos[j].GetComponentInChildren<MeshRenderer>().material = Transparent;
                }
            }
            if (iMin != i)
            {
                //step.text = "Swap the smallest value with our left most unsorted index.";
                co2 = StartCoroutine(SwapAnimation(i, iMin));
                yield return co2;
            }
        }
        sorted = true;
        //EnableTrigger(0);
        yield return new WaitForSeconds(speed);
        running = false;
        steptxt.text = "See! That was quite easy.  \n\nSelect Tutorial again if you wish to see the steps.  Otherwise try your hand at the game! \n\nIf you want more specific instructions on how Selection Sort Works, try out the other tutorials first.";
    }

    public void Update()
    {
        if (moving)
        {

            block[tempnumi].transform.position = Vector3.MoveTowards(block[tempnumi].transform.position, targetTransform, Time.deltaTime * smooth);

            block[tempnumj].transform.position = Vector3.MoveTowards(block[tempnumj].transform.position, targetTransform2, Time.deltaTime * smooth);

            if (block[tempnumi].transform.position == targetTransform || block[tempnumj].transform.position == targetTransform2)
            {
                moving = false;
            }
        }

        if (sorted)
        {
            EnableTrigger(0);
            //step.text = "The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            sorted = false;
        }
    }

    public IEnumerator SwapAnimation(int i, int j)
    {
        tempnumi = i;
        tempnumj = j;

        float tran3 = pos[i].transform.position.y + 1f;
        float tran4 = pos[j].transform.position.y + 1f;
        targetTransform = new Vector3(pos[i].transform.position.x, tran3, pos[i].transform.position.z);
        targetTransform2 = new Vector3(pos[j].transform.position.x, tran4, pos[j].transform.position.z);
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        tran3 = pos[i].transform.position.x;
        tran4 = pos[j].transform.position.x;
        targetTransform = new Vector3(tran4, block[i].transform.position.y, block[i].transform.position.z);
        targetTransform2 = new Vector3(tran3, block[j].transform.position.y, block[j].transform.position.z);
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
        GameObject tempgo = block[i];
        block[i] = block[j];
        block[j] = tempgo;
        checksort();
        yield return new WaitForSeconds(speed);
    }

    public void checksort()
    {
        step = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = i + 1; j < 9; j++)
            {
                Text ti = block[i].GetComponentInChildren<Text>();
                Text tj = block[j].GetComponentInChildren<Text>();
                int n = Convert.ToInt32(ti.text);
                int m = Convert.ToInt32(tj.text);

                if (n > m)
                {
                    sorted = false;
                    EnableTrigger(step);
                    nextToSort = step;
                    return;
                }
            }
            step++;
        }
        sorted = true;
    }

    public void EnableTrigger(int j)
    {
        j += 1;
        for (int i = 0; i < 9; i++)
        {
            if (sorted)
            {
                block[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;

                pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                pos[i].GetComponentInChildren<MeshRenderer>().material = DoneSort;
            }
            else
            {
                if (i == j - 1)
                {
                    block[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;

                    pos[i].GetComponentInChildren<MeshRenderer>().material = NextSort;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    dontchange = i;
                }
                else if (i < j)
                {
                    block[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;

                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = DoneSort;
                }
                else
                {
                    block[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;

                    pos[i].GetComponentInChildren<MeshRenderer>().material = Transparent;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }
        }
    }
}
    
