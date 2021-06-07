using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortInteractive2 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;

    public Material Donesort;//blue
    public Material NextSort;//red for swaps
    public Material Unsorted;//white
    public Material Recurs;//yellow for recursion
    public Material Goodmat;//green for pivot

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int NextSmallesIndex;
    public int IndexToSwap;

    Coroutine co;
    Coroutine sho;
    Coroutine lo;
    Coroutine bo;

    public int speed;
    public int smooth;
    public int tempnumi;
    public int tempnumj;

    public bool moving;

    public Vector2 targetTransform;
    public Vector2 targetTransform2;
    public int pi;

    public GameObject igo;
    public GameObject jgo;
    

    private void Start()
    {
        moving = false;
    }

    public void Update()
    {
        if (sorted)
        {
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Quick Sort Operates." + "\nThe Algorithm repeatedly sorts the array around the pivot.  If a value is less than the pviot it will remain on the left branch, otherwise values are compared and swapped until the array is sorted";
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
        try
        {
            moving = false;
            StopCoroutine(co);
            StopCoroutine(sho);
            StopCoroutine(lo);
            StopCoroutine(bo);
            updatePos();
        }
        catch { }
        //Step.text = "Welcome!  This tutorial is designed to teach you play this interactive minigame." + "\n\nIf the block is red it is the pivot and will be used to test our array for swaps.";
        for (int i = 0; i < 9; i++)
        {
            //previouspi[i] = 0;
            int n = Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        sorted = false;
        co = StartCoroutine(Quick());
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
                if (i == pi)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Goodmat;
                    //previouspi[i] = 1;
                }
                else if (i >= l && i <=h)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Recurs;
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
        //Step.text = "First Quick sort will choose the last element as our pivot." + "\nThen our algorithm will swap values on the laft with any value smaller than our pivot.  It does this until all elements greater than the pivot are in the right half of our array";
        //yield return new WaitForSeconds(speed);
        //Step.text = "Quicksort will now swap the first value greater than it, from the left most index first, with the pivot." + "\n It will nowe repeat the above steps to the left and the to the right of the pivot we jsut swapped.";
        //yield return new WaitForSeconds(speed);        
        //Step.text = "It will repeat these steps until the eniter array is sorted.";
        //yield return new WaitForSeconds(speed);


        yield return sho = StartCoroutine(quickSort(0, b.Count - 1));
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

            yield return lo = StartCoroutine(partition(l, h));
            yield return sho = StartCoroutine(quickSort(l, pi - 1));
            yield return sho = StartCoroutine(quickSort(pi + 1, h));
        }
    }

    public IEnumerator partition(int l, int h)
    {
        float.TryParse(b[h].GetComponentInChildren<Text>().text, out float pivot);
        int i = (l - 1);
        for (int j = l; j <= h-1; j++)
        {
            if (i >= 0)
            {
                igo.transform.position = new Vector3(b[i].transform.position.x, b[i].transform.position.y + .2f, b[i].transform.position.z);
            }
            else
            {
                igo.transform.position = new Vector3(b[0].transform.position.x - .2f, b[0].transform.position.y + .2f, b[0].transform.position.z);
            }

            jgo.transform.position = new Vector3(b[j].transform.position.x, b[j].transform.position.y + .2f, b[j].transform.position.z);
            b[j].GetComponentInChildren<MeshRenderer>().material = NextSort;
            yield return new WaitForSeconds(speed);

            float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
            if (temp < pivot)
            {
                i++;
                igo.transform.position = new Vector3(b[i].transform.position.x, b[i].transform.position.y + .2f, b[i].transform.position.z);
                b[i].GetComponentInChildren<MeshRenderer>().material = NextSort;

                yield return bo = StartCoroutine(SwapAnimation(i, j));
                GameObject temp3 = b[i];
                b[i] = b[j];
                b[j] = temp3;
                EnableTrigger(l, h);
                yield return new WaitForSeconds(speed);
            }
            b[j].GetComponentInChildren<MeshRenderer>().material = Recurs;

        }
        yield return new WaitForSeconds(speed);

        b[i+1].GetComponentInChildren<MeshRenderer>().material = NextSort;
        yield return bo = StartCoroutine(SwapAnimation(i+1, h));
        GameObject temp1 = b[i + 1];
        b[i + 1] = b[h];
        b[h] = temp1;

        pi = i + 1;
        EnableTrigger(l, h);
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
