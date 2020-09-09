using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertSortInteractive2 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material Unsorted;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int NextSmallesIndex;
    public int IndexToSwap;

    public Coroutine co;
    public Coroutine sho;

    public int speed;
    public int smooth;
    private int tempnumi;
    private int tempnumj;

    public bool moving;

    private Vector2 targetTransform;
    private Vector2 targetTransform2;

    private void Start()
    {
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Insertion Sort Operates." + "\nThe Algorithm iterates thorugh the array and shifts the smallest values to the front.";
            IndexToSwap = 0;
            NextSmallesIndex = 9;
        }
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
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Insertion Sort." + "\nIf the block is Green it is Sorted and can not be interacted with." + "\nIf a block is White it must be shifted to the elft until it is in its sorted position.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        sorted = false;
        co = StartCoroutine(insertionSort());
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
    public IEnumerator insertionSort()
    {
        Step.text = "Insertion Sort will sort the array by locking the elft msot values until it is adjacent to a smaller value.";
        yield return new WaitForSeconds(speed);
        Step.text = "First iterate through the array until the left index is larger than the right index 'M'.";
        yield return new WaitForSeconds(speed);
        Step.text = "Swap these values, and continue to swap until 'M' is in its sorted position.";
        yield return new WaitForSeconds(speed);
        Step.text = "Resume swapping at the index where the previous swap began and repeat these steps until the array is sorted.";
        yield return new WaitForSeconds(speed);
        int i, j;
        GameObject key;
        for (i = 1; i < b.Count; i++)
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
                yield return sho = StartCoroutine(SwapAnimation(j+1,j));
                b[j + 1] = b[j];
                b[j] = key;
                j--;
                if (j >= 0)
                {
                    float.TryParse(b[j].GetComponentInChildren<Text>().text, out tempj);
                }
                updatePos();
                yield return new WaitForSeconds(speed);
            }
            b[j+1] = key;
            updatePos();
  
        }
        updatePos();
        sorted = true;
        yield return new WaitForSeconds(speed);

    }

    public void EnableTrigger()
    {
        for (int i = 0; i < b.Count; i++)
        {
            if (sorted)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else
            {
                if (i == NextSmallesIndex)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                else if (i < NextSmallesIndex)
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