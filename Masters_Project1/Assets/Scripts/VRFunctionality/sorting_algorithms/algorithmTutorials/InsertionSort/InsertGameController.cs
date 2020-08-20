using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertGameController : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;

    private bool sorted;

    private int IndexToSwap;
    private int step;
    public int NextSmallesIndex;
    //public int nextToSort;

    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;
    public Material Transparent;

    public Text Step;
    public Text Correctanswerstxt;
    public Text IncorrectAnswerstxt;
    public Text NumOfGamestxt;

    public VRController_1 m_vRController_1;

    public int CorrectAnswers;
    public int wrongAnswers;
    private void OnEnable()
    {
        Begin();
    }

    void Update()
    {
        if (sorted)
        {
            EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            IndexToSwap = 0;
            NextSmallesIndex = 9;
        }
        else
        {
            float dist1, dist2;
            for (int i = 0; i < b.Length; i++)
            {

                dist1 = Vector3.Distance(b[i].transform.position, pos[NextSmallesIndex].transform.position);
                dist2 = Vector3.Distance(pos[i].transform.position, b[NextSmallesIndex].transform.position);
                if (dist1 < .5 && dist2 < .5 && i != NextSmallesIndex)
                {
                    Debug.Log("Wrong");
                    wrongAnswers++;
                    updatePos();
                }

                dist1 = Vector3.Distance(b[IndexToSwap].transform.position, pos[i].transform.position);
                dist2 = Vector3.Distance(pos[IndexToSwap].transform.position, b[i].transform.position);
                if (dist1 < .5 && dist2 < .5 && i != IndexToSwap)
                {
                    Debug.Log("Wrong");
                    wrongAnswers++;
                    updatePos();
                }

            }

            dist1 = Vector3.Distance(b[IndexToSwap].transform.position, pos[NextSmallesIndex].transform.position);
            dist2 = Vector3.Distance(pos[IndexToSwap].transform.position, b[NextSmallesIndex].transform.position);
            if (dist1 < .5 && dist2 < .5)
            {
                Debug.Log("Swap");
                CorrectAnswers++;
                SwapValues();
                updatePos();
                checksort();
            }
        }
    }

    public void Begin()
    {
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\n\nIf the block is Green it is Sorted and can not be interacted with." + "\n\nIf a block is white It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
            b[i].GetComponent<BlockParent>().PairedPos = pos[i];
            b[i].GetComponent<BlockParent>().gravity = true;
        }
        b[0].GetComponentInChildren<Text>().text = "1";
        updatePos();
        sorted = false;
        checksort();
    }

    public void checksort()
    {
        for (int i = 0; i < b.Length - 1; i++)
        {
            int j = i + 1;
            Text ti = b[i].GetComponentInChildren<Text>();
            Text tj = b[j].GetComponentInChildren<Text>();
            int ni = Convert.ToInt32(ti.text);
            int mi = Convert.ToInt32(tj.text);
            if (ni > mi)
            {
                NextSmallesIndex = j;
                FindSwap();
                EnableTrigger();
                sorted = false;
                return;
            }
        }
        sorted = true;
    }
    public void EnableTrigger()
    {
        for (int i = 0; i < b.Length; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BoxCollider>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if(i < NextSmallesIndex)
                {
                    b[i].GetComponent<BlockParent>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<Rigidbody>().isKinematic = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    b[i].GetComponent<BlockParent>().gravity = true;
                }
                else if (i == NextSmallesIndex)
                {
                    b[i].GetComponent<BlockParent>().enabled = true;
                    b[i].GetComponent<BoxCollider>().enabled = true;
                    b[i].GetComponent<Rigidbody>().isKinematic = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                    b[i].GetComponent<BlockParent>().gravity = true;
                }
                else if (i > NextSmallesIndex)
                {
                    b[i].GetComponent<BoxCollider>().enabled = false;
                    b[i].GetComponent<Rigidbody>().isKinematic = true;
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().enabled = false;
                    pos[i].GetComponentInChildren<MeshRenderer>().material = Transparent;
                    b[i].GetComponent<BlockParent>().gravity = false;
                }

            }
        }
    }

    public void FindSwap()
    {
        int tempindex = 0;
        for (int i = NextSmallesIndex - 1; i >= 0; i--)
        {
            Text ti = b[i].GetComponentInChildren<Text>();
            Text tj = b[NextSmallesIndex].GetComponentInChildren<Text>();
            int ni = Convert.ToInt32(ti.text);
            int mi = Convert.ToInt32(tj.text);

            if (ni > mi)
            {
                tempindex = i;
            }
            if (ni < mi)
            {
                break;
            }
        }
        IndexToSwap = tempindex;
    }

    public void SwapValues()
    {
        string tempstringvalue = b[NextSmallesIndex].GetComponentInChildren<Text>().text;
        for (int i = NextSmallesIndex; i > IndexToSwap - 1; i--)
        {
            if (i == IndexToSwap)
            {
                b[i].GetComponentInChildren<Text>().text = tempstringvalue;
            }
            else
            {
                b[i].GetComponentInChildren<Text>().text = b[i - 1].GetComponentInChildren<Text>().text;
            }
        }
        updatePos();
    }

    public void updatePos()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            pos[i].GetComponent<MeshRenderer>().material = Transparent;
        }
    }
}
