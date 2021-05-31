using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelsortInteractive2 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material GoodSort;
    public Material Unsorted;

    public bool sorted;

    //public VRController_1 m_vRController_1;

    public int NextSmallesIndex;
    public int IndexToSwap;

    Coroutine co;
    Coroutine sho;
    Coroutine lo;

    public int speed;
    public int smooth;
    public int tempnumi;
    public int tempnumj;

    public bool moving;

    public Vector2 targetTransform;
    public Vector2 targetTransform2;

    private void Start()
    {
        moving = false;
    }

    public void Update()
    {
        if (sorted)
        {
            IndexToSwap = 0;
            NextSmallesIndex = 9;
        }
        if (moving)//this will move the blocks when the corutine tells us to move them
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

    public void Begin()//this will begin the algorithm and start swapping
    {
        try//attempt to stop the coroutines if they are currently running.  It gets wierd if multiple coroutines start running.  Also takes alot out of the pc.  be careful with this.
        {
            moving = false;
            StopCoroutine(co);
            StopCoroutine(sho);
            StopCoroutine(lo);
            updatePos();
        }
        catch { }

        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        sorted = false;
        co = StartCoroutine(Selectionchecksort());
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
    public void EnableTrigger(int jval, int ival, int iMinVal)//will cahnge the material of certain blocks depending on their values
    {
        for (int i = 0; i < b.Count; i++)
        {
            if (sorted)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == iMinVal || i == ival)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = GoodSort;//set imin to green
                }

                else if (i == jval)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;//set j and i to red
                }

                else if (i < ival && ival != -1)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;//set every sorted value to blue
                }

                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;//set unsorted to white
                }
            }
        }
    }
    public IEnumerator SwapAnimation(int i, int j)//performs the sawp.  simple gives them a new tranform position.  update() performs the actual movement.
    {
        tempnumi = i;
        tempnumj = j;

        float tran3 = pos[i].GetComponent<RectTransform>().anchoredPosition.y + 50f;
        float tran4 = pos[j].GetComponent<RectTransform>().anchoredPosition.y - 50f;
        targetTransform = new Vector3(pos[i].GetComponent<RectTransform>().anchoredPosition.x, tran3);
        targetTransform2 = new Vector3(pos[j].GetComponent<RectTransform>().anchoredPosition.x, tran4);
        Debug.Log("run swap");
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
    public IEnumerator Selectionchecksort()//begins the algorithm //this is my response to the halting problem.  just put it in a coroutine 
    {
        yield return sho = StartCoroutine(SelectionSort());//this will stop this coroutine until the new function has finished.  It is extremely helpful for when you want to know exactly when a function ends
        updatePos();
        sorted = true;
        EnableTrigger(-1,-1,-1);
    }

    public IEnumerator SelectionSort()//this is the holy grail.  jk its just the bubble sort algorithm.  with some added code
    {
        int i, j, iMin;
        for (i = 0; i < b.Count - 1; i++)
        {
            iMin = i;
            for (j = i + 1; j < b.Count; j++)
            {

                float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp1);
                float.TryParse(b[iMin].GetComponentInChildren<Text>().text, out float temp2);
                if (temp1 < temp2)
                {
                    iMin = j;
                }
                EnableTrigger(j, i ,iMin);

                yield return new WaitForSeconds(speed);
            }
            if (iMin != i)
            {
                EnableTrigger(-1, i, iMin);
                yield return lo = StartCoroutine(SwapAnimation(i, iMin));//stop the algorithm until the swap has been made.  in this case the swap occurs automatically.  however another function eprforms it
                GameObject temp3 = b[i];
                b[i] = b[iMin];
                b[iMin] = temp3;

                yield return new WaitForSeconds(speed);
            }
        }
    }
}
