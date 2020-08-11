using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelSortGameController : MonoBehaviour
{
    public GameObject[] block;
    public GameObject[] pos;

    private bool sorted;

    private float currentSmallest;
    private int step;
    public int currentSmallestIndex;
    public int nextToSort;

    public Material DoneSort;
    public Material NextSort;
    public Material Transparent;

    public VRController_1 m_vRController_1;
    // Start is called before the first frame update
    void Start()
    {
        Begin();
    }

    // Update is called once per frame
    void Update()
    {
        float dist1, dist2;
        dist1 = Vector3.Distance(block[nextToSort].transform.position, pos[currentSmallestIndex].transform.position);
        dist2 = Vector3.Distance(block[currentSmallestIndex].transform.position, pos[nextToSort].transform.position);
        if (dist1 < .5 && dist2 < .5)
        {
            Debug.Log("Swap");
            SwapValues(nextToSort, currentSmallestIndex);
        }
    }
    public void Begin()
    {
        //Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = block[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            block[i].GetComponent<BlockParent>().PairedPos = pos[i];
        }
        block[0].GetComponentInChildren<Text>().text = "1";
        updatePos();
        sorted = false;
        checksort();
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
                block[i].GetComponent<BoxCollider>().enabled = false;
                block[i].GetComponent<Rigidbody>().isKinematic = true;
                block[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                pos[i].GetComponentInChildren<MeshRenderer>().material = DoneSort;
            }
            else
            {
                if (i == j - 1)
                {
                    block[i].GetComponent<BlockParent>().enabled = true;
                    block[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = NextSort;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                }
                else if (i < j)
                {
                    block[i].GetComponent<BoxCollider>().enabled = false;
                    block[i].GetComponent<Rigidbody>().isKinematic = true;
                    block[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = DoneSort;
                }
                else
                {
                    block[i].GetComponent<BlockParent>().enabled = true;
                    block[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Transparent;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                    
                }
            }
        }

        //get the currect smallest value in the unsorted array
        currentSmallest = 101;
        for (int i = j; i < 9; i++)
        {
            float ti;
            float.TryParse(block[i].GetComponentInChildren<Text>().text, out ti);

            if (currentSmallest > ti)
            {
                currentSmallest = ti;
                currentSmallestIndex = i;
            }
        }
    }

    public void SwapValues(int i, int j)
    {
        m_vRController_1.down = false;
        GameObject temppos = block[i].GetComponent<BlockParent>().PairedPos;
        block[i].GetComponent<BlockParent>().PairedPos = block[j].GetComponent<BlockParent>().PairedPos;
        block[j].GetComponent<BlockParent>().PairedPos = temppos;

        Text ti = block[i].GetComponentInChildren<Text>();
        Text tj = block[j].GetComponentInChildren<Text>();

        string n = ti.text;

        ti.text = tj.text;
        tj.text = n;

        block[i].GetComponent<BlockParent>().isGrabbed = false;
        block[j].GetComponent<BlockParent>().isGrabbed = false;
        updatePos();
        for (int t = 0; t < pos.Length; t++)
        {
            pos[t].GetComponent<MeshRenderer>().material = Transparent;
        }
        checksort();
    }
    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            block[i].transform.position = pos[i].transform.position;
            block[i].transform.rotation = pos[i].transform.rotation;
        }
    }
}
