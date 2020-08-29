using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortInteractive2 : MonoBehaviour
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
    public int tempnumi;
    public int tempnumj;

    public bool moving;

    public Vector2 targetTransform;
    public Vector2 targetTransform2;
    public int pi;

    public int[] previouspi;

    private void Start()
    {
        previouspi = new int[9];
        moving = false;
        //Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            //EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            IndexToSwap = 0;
            NextSmallesIndex = 9;
        }
        if (moving)
        {
            if (tempnumi == tempnumj)
            {
                moving = false;
            }
            b[tempnumi].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(b[tempnumi].GetComponent<RectTransform>().anchoredPosition, targetTransform, Time.deltaTime * smooth);

            b[tempnumj].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(b[tempnumj].GetComponent<RectTransform>().anchoredPosition, targetTransform2, Time.deltaTime * smooth);

            if (b[tempnumi].GetComponent<RectTransform>().anchoredPosition == targetTransform || b[tempnumj].GetComponent<RectTransform>().anchoredPosition == targetTransform2)
            {
                moving = false;
            }
        }
    }

    public void Begin()
    {
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            previouspi[i] = 0;
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        sorted = false;
        StartCoroutine(Quick());
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

    public void EnableTrigger(int l, int h)
    {
        for (int i = 0; i < b.Count; i++)
        {
            if (sorted)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (previouspi[i] == 1)
                { }
                else if (i == pi)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    //previouspi[i] = 1;
                }
                else if (i >= l && i <=h)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = nextLine;
                }
                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
        }
    }
    

    public IEnumerator Quick()
    {
        yield return StartCoroutine(quickSort(0, b.Count - 1));
        updatePos();
        sorted = true;
        EnableTrigger(0, 0);
    }
    public IEnumerator quickSort(int l, int h)
    {
        if (l < h)
        {
            pi = h;
            EnableTrigger(l,h);
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
        for (int j = l; j <= h-1; j++)
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
        yield return StartCoroutine(SwapAnimation(i+1, h));
        GameObject temp1 = b[i + 1];
        b[i + 1] = b[h];
        b[h] = temp1;
        b[i+1].GetComponentInChildren<MeshRenderer>().material = Donesort;
        previouspi[i+1] = 1;
        pi = i + 1;
        yield return new WaitForSeconds(speed);
    }
    public IEnumerator SwapAnimation(int i, int j)
    {
        tempnumi = i;
        tempnumj = j;

        float tran3 = pos[i].GetComponent<RectTransform>().anchoredPosition.y + 50f;
        float tran4 = pos[j].GetComponent<RectTransform>().anchoredPosition.y - 50f;
        targetTransform = new Vector3(pos[i].GetComponent<RectTransform>().anchoredPosition.x, tran3);
        targetTransform2 = new Vector3(pos[j].GetComponent<RectTransform>().anchoredPosition.x, tran4);
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        tran3 = pos[i].GetComponent<RectTransform>().anchoredPosition.x;
        tran4 = pos[j].GetComponent<RectTransform>().anchoredPosition.x;
        targetTransform = new Vector3(tran4, b[i].GetComponent<RectTransform>().anchoredPosition.y);
        targetTransform2 = new Vector3(tran3, b[j].GetComponent<RectTransform>().anchoredPosition.y);
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        targetTransform = pos[tempnumj].GetComponent<RectTransform>().anchoredPosition;
        targetTransform2 = pos[tempnumi].GetComponent<RectTransform>().anchoredPosition;
        moving = true;
        while (moving == true)
        {
            yield return null;
        }
    }
}
