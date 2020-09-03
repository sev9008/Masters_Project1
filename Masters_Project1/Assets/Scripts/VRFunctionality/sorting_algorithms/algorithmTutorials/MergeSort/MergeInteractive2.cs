using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeInteractive2 : MonoBehaviour
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

    //public int NextSmallesIndex;
    //public int IndexToSwap;
    public int currentSmallestIndex;
    public int nextToSort;

    Coroutine co;
    Coroutine sho;

    public int speed;
    public int smooth;
    public int tempnumi;
    public int tempnumj;

    public bool moving;

    public Vector2 targetTransform;
    public Vector2 targetTransform2;

    public bool waitingforswap;

    private GameObject[] L;
    private GameObject[] R;
    private bool Larray;


    private void Start()
    {
        moving = false;
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            //EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Selction Sort Operates." + "\nThe Algorithm locks the positions that have already been sorted, and chooses the next smallest element to swap.";
        }
        if (moving)
        {
            //if (tempnumi == tempnumj)
            //{
            //    moving = false;
            //    return;
            //}
            float.TryParse(b[tempnumi].GetComponentInChildren<Text>().text, out float temp);
            float temp2;
            if (Larray)
            {
                float.TryParse(L[tempnumj].GetComponentInChildren<Text>().text, out temp2);
            }
            else
            {
                float.TryParse(R[tempnumj].GetComponentInChildren<Text>().text, out temp2);
            }
            Debug.Log(temp2 + " " + temp);
            if (temp == temp2)
            {
                moving = false;
                return;
            }

            b[tempnumi].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(b[tempnumi].GetComponent<RectTransform>().anchoredPosition, targetTransform, Time.deltaTime * smooth);

            if (Larray)
            {
                L[tempnumj].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(L[tempnumj].GetComponent<RectTransform>().anchoredPosition, targetTransform2, Time.deltaTime * smooth);

            }
            else
            {
                R[tempnumj].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(R[tempnumj].GetComponent<RectTransform>().anchoredPosition, targetTransform2, Time.deltaTime * smooth);

            }

            if (Larray)
            {
                if (b[tempnumi].GetComponent<RectTransform>().anchoredPosition == targetTransform || L[tempnumj].GetComponent<RectTransform>().anchoredPosition == targetTransform2)
                {
                    moving = false;
                }
            }
            else 
            {
                if (b[tempnumi].GetComponent<RectTransform>().anchoredPosition == targetTransform || R[tempnumj].GetComponent<RectTransform>().anchoredPosition == targetTransform2)
                {
                    moving = false;
                }
            }

        }
    }

    public void Begin()
    {
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Selection Sort." + "\nIf the block is blue it is Sorted and can not be interacted with." + "\nIf a block is red It must be swapped with the smallest value from the unsorted array.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();
        }
        updatePos();
        sorted = false;
        StartCoroutine(Mergechecksort());
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

    public IEnumerator SwapAnimation(int i, int j)
    {
        tempnumi = i;
        tempnumj = j;

        float tran3 = b[i].GetComponent<RectTransform>().anchoredPosition.y + 50f;
        float tran4;
        if (Larray)
        {
            tran4 = L[j].GetComponent<RectTransform>().anchoredPosition.y - 50f;
            targetTransform2 = new Vector3(L[j].GetComponent<RectTransform>().anchoredPosition.x, tran4);
        }
        else 
        {
            tran4 = R[j].GetComponent<RectTransform>().anchoredPosition.y - 50f;
            targetTransform2 = new Vector3(R[j].GetComponent<RectTransform>().anchoredPosition.x, tran4);
        }
        targetTransform = new Vector3(b[i].GetComponent<RectTransform>().anchoredPosition.x, tran3);
        moving = true;
        while (moving == true)
        {
            Debug.Log("stuck1");
            yield return null;
        }

        float tran5 = b[i].GetComponent<RectTransform>().anchoredPosition.x;
        float tran6;
        if (Larray)
        {
            tran6 = L[j].GetComponent<RectTransform>().anchoredPosition.x;
            targetTransform2 = new Vector3(tran5, L[j].GetComponent<RectTransform>().anchoredPosition.y);
        }
        else
        {
            tran6 = R[j].GetComponent<RectTransform>().anchoredPosition.x;
            targetTransform2 = new Vector3(tran5, R[j].GetComponent<RectTransform>().anchoredPosition.y);
        }
        targetTransform = new Vector3(tran6, b[i].GetComponent<RectTransform>().anchoredPosition.y);
        moving = true;
        while (moving == true)
        {
            Debug.Log("stuck2");
            yield return null;
        }

        if (Larray)
        {
            targetTransform2 = new Vector3(L[j].GetComponent<RectTransform>().anchoredPosition.x, (L[j].GetComponent<RectTransform>().anchoredPosition.y + 50));
        }
        else
        {
            targetTransform2 = new Vector3(R[j].GetComponent<RectTransform>().anchoredPosition.x, (R[j].GetComponent<RectTransform>().anchoredPosition.y + 50));
        }
        targetTransform = new Vector3(b[i].GetComponent<RectTransform>().anchoredPosition.x, (b[i].GetComponent<RectTransform>().anchoredPosition.y - 50));
        moving = true;
        while (moving == true)
        {
            Debug.Log("stuck3");
            yield return null;
        }
        yield return new WaitForSeconds(speed);
    }

    public IEnumerator Mergechecksort()
    {
        yield return StartCoroutine(mergeSort(0, b.Count - 1));
        updatePos();
        sorted = true;
        //EnableTrigger(0, 0);
    }

    public IEnumerator mergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;
            yield return mergeSort(l, m);
            yield return mergeSort(m + 1, r);
            yield return merge(l, m, r);
        }
    }

    public IEnumerator merge(int l, int m, int r)
    {
        EnableTrigger(l, r);
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        L = new GameObject[n1];
        R = new GameObject[n2];
        for (i = 0; i < n1; i++)
        {
            L[i] = b[l + i];
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = b[m + 1 + j];
        }
        i = 0;
        j = 0;
        k = l;
        yield return new WaitForSeconds(speed);
        while (i < n1 && j < n2)
        {
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out float temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out float temp2);

            if (temp <= temp2)
            {
                Larray = true;
                b[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                yield return StartCoroutine(SwapAnimation(k, i));
                b[k] = L[i];
                i++;
            }
            else
            {
                Larray = false;
                b[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                yield return StartCoroutine(SwapAnimation(k, j));
                b[k] = R[j];
                j++;
            }
            k++;
            updatePos();
        }
        EnableTrigger(l, r);
        while (i < n1)
        {
            Larray = true;
            b[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            yield return StartCoroutine(SwapAnimation(k, i));
            b[k] = L[i];
            i++;
            k++;
            updatePos();
        }
        while (j < n2)
        {
            Larray = false;
            b[k].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            yield return StartCoroutine(SwapAnimation(k, j));
            b[k] = R[j];
            j++;
            k++;
            updatePos();
        }
        updatePos();
        yield return new WaitForSeconds(speed);
    }

    public void EnableTrigger(int l, int h)
    {
        Debug.Log(l + " " + h);
        for (int i = 0; i < b.Count; i++)
        {
            if (i >= l && i <= h)
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
