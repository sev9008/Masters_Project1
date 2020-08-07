using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelsortInteractive2 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;

    public bool running;

    [SerializeField] public int speed;
    [SerializeField] public float smooth;
    public bool moving;

    private Vector2 targetTransform;

    private Vector2 targetTransform2;
    private int tempnumi;
    private int tempnumj;

    public Material Donesort;
    public Material Nextsort;
    public Material smallest;
    public Material checksmall;
    public Material Unsorted;

    private bool sorted;

    public GameObject arrow;

    public Text step;
    public int dontchange;

    public Coroutine co1;
    public Coroutine co2;

    private void Start()
    {
        //Begin();
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
        arrow.SetActive(true);
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        checksort();
        co1 = StartCoroutine(SelectionInteractive2());
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

    public IEnumerator SelectionInteractive2()
    {
        //structarr = new List<MyStruct>();
        running = true;
        int i, j;
        float n, m;
        int iMin = 0;
        for (i = 0; i < b.Count - 1; i++)
        {
            iMin = i;
            for (j = i + 1; j < b.Count; j++)
            {
                float.TryParse(b[j].GetComponentInChildren<Text>().text, out n);
                float.TryParse(b[iMin].GetComponentInChildren<Text>().text, out m);
                if (j != dontchange)
                {
                    b[j].GetComponentInChildren<MeshRenderer>().material = checksmall;
                    step.text = "Checking for new smallest";
                }
                if (n < m)
                {
                    if (iMin != i)
                    {
                        if (iMin != dontchange)
                        {
                            b[iMin].GetComponentInChildren<MeshRenderer>().material = checksmall;
                        }
                    }
                    iMin = j;
                    b[j].GetComponentInChildren<MeshRenderer>().material = smallest;
                    step.text = "Replace the current smallest value";
                }
                yield return new WaitForSeconds(speed);
                if (b[j].GetComponentInChildren<MeshRenderer>().material == checksmall)
                {
                    b[j].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
            if (iMin != i)
            {
                step.text = "Swap the smallest value with our left most unsorted index.";
                co2 = StartCoroutine(SwapAnimation(i, iMin));
                yield return co2;
            }
        }
        sorted = true;
        //EnableTrigger(0);
        yield return new WaitForSeconds(speed);
        running = false;
        arrow.SetActive(false);
}

    public void Update()
    {
        if (moving)
        {

            b[tempnumi].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(b[tempnumi].GetComponent<RectTransform>().anchoredPosition, targetTransform, Time.deltaTime * smooth);

            b[tempnumj].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(b[tempnumj].GetComponent<RectTransform>().anchoredPosition, targetTransform2, Time.deltaTime * smooth);

            if (b[tempnumi].GetComponent<RectTransform>().anchoredPosition == targetTransform || b[tempnumj].GetComponent<RectTransform>().anchoredPosition == targetTransform2)
            {
                moving = false;
            }
        }

        if (sorted)
        {
            EnableTrigger(0);
            step.text = "The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
            sorted = false;
        }
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
        GameObject tempgo = b[i];
        b[i] = b[j];
        b[j] = tempgo;
        checksort();
        yield return new WaitForSeconds(speed);
    }

    public void EnableTrigger(int j)
    {
        j += 1;
        for (int i = 0; i < 9; i++)
        {
            if (sorted)
            {
                arrow.SetActive(false);
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i < j)
                {
                    if (i == j - 1)
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                        dontchange = i;
                        arrow.transform.position = b[i].transform.position;
                        arrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(arrow.GetComponent<RectTransform>().anchoredPosition.x, arrow.GetComponent<RectTransform>().anchoredPosition.y + 40);
                    }
                    else 
                    {
                        b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
                    }
                }
                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
        }
    }
    public void checksort()
    {
        int step = 0;
        for (int i = 0; i < 9; i++)
        {
            for (int j = i + 1; j < 9; j++)
            {
                Text ti = b[i].GetComponentInChildren<Text>();
                Text tj = b[j].GetComponentInChildren<Text>();
                int n = Convert.ToInt32(ti.text);
                int m = Convert.ToInt32(tj.text);

                if (n > m)
                {
                    sorted = false;
                    EnableTrigger(step);
                    return;
                }
            }
            step++;
        }
    }
}
