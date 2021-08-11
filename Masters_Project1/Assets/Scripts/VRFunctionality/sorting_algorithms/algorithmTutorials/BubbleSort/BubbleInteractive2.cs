using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This tutorial is designed to show the user the rythm of the sorting algorithm.
/// it will automatically sort the array. but its a unique way from looking at the algorithm as it performs.
/// </summary>
public class BubbleInteractive2 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;

    public bool sorted;

    public VRController_1 m_vRController_1;

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
        co = StartCoroutine(Bubblechecksort());
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
    public void EnableTrigger(int maxsort, int j)//will cahnge the material of certain blocks depending on their values
    {
        for (int i = 0; i < b.Count; i++)
        {
            if (sorted)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == j || i == j+1)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                else if (i >= maxsort)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                }
                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
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
    public IEnumerator Bubblechecksort()//begins the algorithm //this is my response to the halting problem.  just put it in a coroutine 
    {
        yield return sho = StartCoroutine(BubbleSort());//this will stop this coroutine until the new function has finished.  It is extremely helpful for when you want to know exactly when a function ends
        updatePos();
        sorted = true;
        EnableTrigger(-1, -1);
    }
    
    public IEnumerator BubbleSort()//this is the holy grail.  jk its just the bubble sort algorithm.  with some added code
    {
        bool swapped = false;
        int i, j;
        for (i = 0; i < b.Count - 1; i++)
        {
            for (j = 0; j < b.Count - i - 1; j++)
            {
                EnableTrigger(b.Count - i, j);
                yield return new WaitForSeconds(speed);

                float.TryParse(b[j].GetComponentInChildren<Text>().text, out float temp);
                float.TryParse(b[j + 1].GetComponentInChildren<Text>().text, out float temp2);
                if (temp > temp2)
                {
                    EnableTrigger(b.Count - i, j);
                    yield return  lo = StartCoroutine(SwapAnimation(j, j+1));//stop the algorithm until the swap has been made.  in this case the swap occurs automatically.  however another function eprforms it
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
