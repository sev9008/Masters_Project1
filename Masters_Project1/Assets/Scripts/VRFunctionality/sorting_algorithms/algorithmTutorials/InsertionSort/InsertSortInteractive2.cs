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
    public Material prevsort;
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

    private GameObject tempGo;

    public bool moving;

    private Vector2 targetTransform;
    private Vector2 targetTransform2;

    public GameObject keyGo;

    private void Start()
    {
        //Begin();
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
            tempGo.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(tempGo.GetComponent<RectTransform>().anchoredPosition, targetTransform, Time.deltaTime * smooth);
            if (tempGo.GetComponent<RectTransform>().anchoredPosition == targetTransform)
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

        keyGo.SetActive(true);

        int i, key, j;
        for (i = 1; i < b.Count; i++)
        {
            key = int.Parse(b[i].GetComponentInChildren<Text>().text);
            b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            keyGo.GetComponentInChildren<Text>().text = key.ToString();
            keyGo.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[i].GetComponent<RectTransform>().anchoredPosition.x, b[i].GetComponent<RectTransform>().anchoredPosition.y - 50);

            j = i - 1;

            int tempval = int.Parse(b[j].GetComponentInChildren<Text>().text);
            while (j >= 0 && tempval > key)
            {
                b[j].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                b[j+1].GetComponentInChildren<MeshRenderer>().material = prevsort;
                yield return StartCoroutine(SwapAnimation(j + 1, j));
                updatePos();
                b[j + 1].GetComponentInChildren<Text>().text = b[j].GetComponentInChildren<Text>().text;
                j = j - 1;

                if (j >= 0)//use this otherwise tmepval will throw an error
                {
                    tempval = int.Parse(b[j].GetComponentInChildren<Text>().text);
                    Debug.Log(tempval);
                }
                yield return new WaitForSeconds(speed);
            }
            yield return StartCoroutine(SwapAnimation(j+1, -1));

            b[j + 1].GetComponentInChildren<Text>().text = key.ToString();
            yield return new WaitForSeconds(speed);
            for (int p = 0; p < b.Count; p++)
            {
                b[p].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
        }
        keyGo.SetActive(false);
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
        if (j == -1)
        {
            tempGo = keyGo;
            float tran = tempGo.GetComponent<RectTransform>().anchoredPosition.y;
            targetTransform = new Vector3(b[i].GetComponent<RectTransform>().anchoredPosition.x, tran);
            moving = true;
            while (moving == true)
            {
                yield return null;
            }

            targetTransform = new Vector3(tempGo.GetComponent<RectTransform>().anchoredPosition.x, b[i].GetComponent<RectTransform>().anchoredPosition.y);
            moving = true;
            while (moving == true)
            {
                yield return null;
            }
        }
        else 
        {
            tempGo = b[j];
            float tran = tempGo.GetComponent<RectTransform>().anchoredPosition.y + 50f;//move up 50 units
            targetTransform = new Vector3(tempGo.GetComponent<RectTransform>().anchoredPosition.x, tran);
            moving = true;
            while (moving == true)
            {
                yield return null;
            }

            targetTransform = new Vector3(b[i].GetComponent<RectTransform>().anchoredPosition.x, tempGo.GetComponent<RectTransform>().anchoredPosition.y);
            moving = true;
            while (moving == true)
            {
                yield return null;
            }

            targetTransform = new Vector3(b[i].GetComponent<RectTransform>().anchoredPosition.x, b[i].GetComponent<RectTransform>().anchoredPosition.y);
            moving = true;
            while (moving == true)
            {
                yield return null;
            }
        }
    }
}