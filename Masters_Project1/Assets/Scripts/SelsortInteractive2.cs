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

    private void Start()
    {
        Begin();
    }

    public void Begin()
    {
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        checksort();
        StartCoroutine(SelectionInteractive2());
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
        running = true;
        int i, j;
        int iMin;
        float n, m, o;
        for (i = 0; i < b.Count - 1; i++)
        {
            iMin = i;
            int tmp = 0;
            for (j = i + 1; j < b.Count; j++)
            {
                float.TryParse(b[j].GetComponentInChildren<Text>().text, out n);
                float.TryParse(b[iMin].GetComponentInChildren<Text>().text, out m);
                if(b[j].GetComponentInChildren<MeshRenderer>().material != Nextsort)
                b[j].GetComponentInChildren<MeshRenderer>().material = checksmall;
                if (n < m)
                {
                    yield return new WaitForSeconds(speed);
                    if (tmp > 0)
                    {
                        b[iMin].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                    }
                    tmp++;
                    iMin = j;
                    b[iMin].GetComponentInChildren<MeshRenderer>().material = smallest;
                }
                yield return new WaitForSeconds(speed);
                if (j != iMin)
                {
                    b[j].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
            if (iMin != i)
            {
                yield return SwapAnimation(i, iMin);
            }
        }
        sorted = true;
        EnableTrigger(0);
        running = false;
        yield return new WaitForSeconds(speed);
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
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == j - 1)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                else if (i < j)
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
        sorted = true;
    }
}
