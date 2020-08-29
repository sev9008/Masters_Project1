using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertGameTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] pos;

    public Material DoneSort;
    public Material NextSort;
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


    private void OnEnable()
    {
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
        EnableTrigger();
        updatePos();
        sorted = false;
        co = StartCoroutine(insertionSort());
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

    public IEnumerator insertionSort()
    {
        yield return new WaitForSeconds(1);

        steptxt.text = "In this minigame you, the employee, must organize boxes by their value.  \n\nThis value is displayed above the boxes. \n\nYour boss wants you to organize them using Selection Sort!";

        yield return new WaitForSeconds(1);

        steptxt.text = "This tutorial will go through the execution of the game with the correct moves.  \n\nTo swap a box, simply palce both boxes in their respective, correct, positions!";

        yield return new WaitForSeconds(1);
        running = true;

        int i, j;
        GameObject key;
        for (i = 1; i < b.Length; i++)
        {
            NextSmallesIndex = i;
            EnableTrigger();
            updatePos();
            yield return new WaitForSeconds(speed);
            key = b[i];
            j = i - 1;

            float.TryParse(b[j].GetComponentInChildren<Text>().text, out float tempj);
            float.TryParse(key.GetComponentInChildren<Text>().text, out float tempkey);
            //Debug.Log(tempj + ", " + tempkey);
            while (j >= 0 && tempj > tempkey)
            {
                EnableTrigger();
                yield return sho = StartCoroutine(SwapAnimation(j + 1, j));
                NextSmallesIndex--;
                b[j + 1] = b[j];
                b[j] = key;
                j--;
                if (j >= 0)
                {
                    float.TryParse(b[j].GetComponentInChildren<Text>().text, out tempj);
                }
                //EnableTrigger();
                updatePos();
                yield return new WaitForSeconds(speed);
            }
            b[j + 1] = key;
            updatePos();

        }
        updatePos();
        sorted = true;
        yield return new WaitForSeconds(speed);
    }
    public void Update()
    {
        if (sorted)
        {
            EnableTrigger();
            //Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            IndexToSwap = 0;
            NextSmallesIndex = 0;
        }
        if (moving)
        {

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

    public void EnableTrigger()
    {
        Debug.Log("hit" + NextSmallesIndex);
        for (int i = 0; i < b.Length; i++)
        {
            if (sorted)
            {
                b[i].GetComponent<BlockParent>().enabled = false;
                pos[i].GetComponent<BoxCollider>().enabled = false;

                pos[i].GetComponentInChildren<MeshRenderer>().enabled = true;
                pos[i].GetComponentInChildren<MeshRenderer>().material = DoneSort;
            }
            else
            {
                Debug.Log("hit2");
                if (i < NextSmallesIndex)
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = false;
                    pos[i].GetComponent<MeshRenderer>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().material = DoneSort;
                    Debug.Log("hit3");
                }
                else if (i == NextSmallesIndex)
                {
                    b[i].GetComponent<BlockParent>().enabled = false;
                    pos[i].GetComponent<BoxCollider>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().enabled = true;
                    pos[i].GetComponent<MeshRenderer>().material = NextSort;
                }

                else if (i > NextSmallesIndex)
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
