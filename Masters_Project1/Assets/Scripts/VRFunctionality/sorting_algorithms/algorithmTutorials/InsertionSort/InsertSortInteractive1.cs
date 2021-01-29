using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertSortInteractive1 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Nextsort;
    public Material prevsort;
    public Material Unsorted;

    public bool sorted;
    public bool waitingforswap;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int currentSmallestIndex;
    public int nextToSort;
    public float dist1;
    public float dist2;

    public Coroutine co;
    public Coroutine sho;

    private int corretAnswers;
    private int incorretAnswers;
    private int numofGames;
    public Text corretAnswersText;
    public Text incorretAnswersText;
    public Text numofGamesText;

    public int speed;
    public int keypos;

    private GameObject tempGo;

    public GameObject keyGo;
    public bool IsTestMode;

    private void Start()
    {
        //Begin();
    }

    private void OnEnable()
    {
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Insertion Sort Operates." + "\nThe Algorithm iterates thorugh the array and shifts the smallest values to the front.";
        }
        if (waitingforswap && currentSmallestIndex != 2000)
        {
            Step.text = "The Algorithm is ready to swap values.  Perform the swap.";
            dist1 = Vector3.Distance(b[nextToSort].transform.position, b[currentSmallestIndex].transform.position);
            if (dist1 < .04)
            {
                m_vRController_1.downR = false;
                m_vRController_1.downL = false;
                Step.text = "The block you attempted to swap was correct.";
                if (IsTestMode)
                {
                    corretAnswers += 1;
                    corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();
                }
                b[nextToSort].GetComponentInChildren<Text>().text = b[currentSmallestIndex].GetComponentInChildren<Text>().text;
                waitingforswap = false;
                updatePos();
            }
            else
            {
                for (int i = 0; i < b.Count; i++)
                {
                    if (i != currentSmallestIndex && i != nextToSort)
                    {
                        dist1 = Vector3.Distance(b[i].transform.position, b[currentSmallestIndex].transform.position);
                        dist2 = Vector3.Distance(b[nextToSort].transform.position, b[i].transform.position);

                        if (dist1 < .04 || dist2 < .04)
                        {
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "The block you attempted to swap was incorrect.";
                            if (IsTestMode)
                            {
                                incorretAnswers += 1;
                                incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                            }
                            updatePos();
                        }
                    }
                }
            }
        }
        else if (waitingforswap && currentSmallestIndex == 2000)
        {
            Step.text = "The Algorithm is ready to swap values.  Perform the swap.";
            dist1 = Vector3.Distance(b[nextToSort].transform.position, keyGo.transform.position);
            if (dist1 < .04)
            {
                m_vRController_1.downR = false;
                m_vRController_1.downL = false;
                Step.text = "The block you attempted to swap was correct.";
                corretAnswers += 1;
                corretAnswersText.GetComponent<Text>().text = corretAnswers.ToString();
                b[nextToSort].GetComponentInChildren<Text>().text = keyGo.GetComponentInChildren<Text>().text;
                waitingforswap = false;
                updatePos();
            }
            else
            {
                for (int i = 0; i < b.Count; i++)
                {
                    if (i != nextToSort)
                    {
                        dist1 = Vector3.Distance(b[i].transform.position, keyGo.transform.position);
                        if (dist1 < .04)
                        {
                            m_vRController_1.downR = false;
                            m_vRController_1.downL = false;
                            Step.text = "The block you attempted to swap was incorrect.";
                            incorretAnswers += 1;
                            incorretAnswersText.GetComponent<Text>().text = incorretAnswers.ToString();
                            updatePos();
                        }
                    }
                }
            }
        }
    }

    public void Begin()
    {
        try
        {
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
            b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
        }
        updatePos();
        sorted = false;
        if (IsTestMode)
        {
            numofGames += 1;
            numofGamesText.GetComponent<Text>().text = numofGames.ToString();
        }
        co = StartCoroutine(insertionSort());
    }

    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].GetComponent<RectTransform>().anchoredPosition = pos[i].GetComponent<RectTransform>().anchoredPosition;
            b[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            keyGo.transform.parent = this.gameObject.transform;
            keyGo.transform.rotation = pos[keypos].transform.rotation;
            keyGo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(b[keypos].GetComponent<RectTransform>().anchoredPosition3D.x, b[keypos].GetComponent<RectTransform>().anchoredPosition3D.y + 50, b[keypos].GetComponent<RectTransform>().anchoredPosition3D.z);
        }
    }
    public IEnumerator insertionSort()
    {
        keyGo.SetActive(true);

        int i, key, j;
        for (i = 1; i < b.Count; i++)
        {
            key = int.Parse(b[i].GetComponentInChildren<Text>().text);
            if (!IsTestMode)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            keyGo.GetComponentInChildren<Text>().text = key.ToString();
            keypos = i;
            updatePos();

            j = i - 1;

            int tempval = int.Parse(b[j].GetComponentInChildren<Text>().text);
            while (j >= 0 && tempval > key)
            {
                if (!IsTestMode)
                {
                    b[j].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                    b[j + 1].GetComponentInChildren<MeshRenderer>().material = prevsort;
                }
                updatePos();
                nextToSort = j + 1;
                currentSmallestIndex = j;
                waitingforswap = true;
                while (waitingforswap)
                {
                    yield return null;
                }
                //b[j + 1].GetComponentInChildren<Text>().text = b[j].GetComponentInChildren<Text>().text;
                j = j - 1;

                if (j >= 0)//use this otherwise tmepval will throw an error
                {
                    tempval = int.Parse(b[j].GetComponentInChildren<Text>().text);
                    Debug.Log(tempval);
                }
                yield return new WaitForSeconds(speed);
            }
            nextToSort = j + 1;
            currentSmallestIndex = 2000;
            waitingforswap = true;
            while (waitingforswap)
            {
                yield return null;
            }
           // b[j + 1].GetComponentInChildren<Text>().text = key.ToString();
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
                if (i == currentSmallestIndex && !IsTestMode)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                }
                else if (i < nextToSort && !IsTestMode)
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
}