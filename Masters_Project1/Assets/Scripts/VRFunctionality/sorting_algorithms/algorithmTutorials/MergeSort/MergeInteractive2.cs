using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeInteractive2 : MonoBehaviour
{
    public List<GameObject> b;
    public List<GameObject> bclone;
    public List<GameObject> pos;
    public Material Donesort;
    public Material Rmat;
    public Material Unsorted;
    public Material Lmat;
    public Material Goodmat;

    public bool sorted;

    public Text Step;

    public VRController_1 m_vRController_1;

    public int currentSmallestIndex;
    public int nextToSort;

    public int speed;
    public int smooth;
    public int tempnumi;
    public int tempnumj;

    public bool moving;
    public bool moving2;

    Coroutine co;
    Coroutine sho;
    Coroutine lo;

    public Vector2 targetTransform;
    public Vector2 targetTransform2;

    public bool waitingforswap;

    private GameObject[] L;
    private GameObject[] R;
    private bool Larray;

    private void Start()
    {
        moving = false;
        moving2 = false;
        Begin();
    }

    public void Update()
    {
        if (sorted)
        {
            //EnableTrigger();
            Step.text = "Congrats!  The array is now Sorted!" + "\nThis is Generally how Merge Sort Operates." + "\nThe Algorithm splits the array into halves and sorts each half one at a time.";
        }
        if (moving)
        {
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
            if (temp == temp2)
            {
                moving = false;
                return;
            }

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
                if (L[tempnumj].GetComponent<RectTransform>().anchoredPosition == targetTransform2)
                {
                    moving = false;
                }
            }
            else 
            {
                if (R[tempnumj].GetComponent<RectTransform>().anchoredPosition == targetTransform2)
                {
                    moving = false;
                }
            }
        }
        //if (moving2)
        //{
        //    b[tempnumj].GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(b[tempnumj].GetComponent<RectTransform>().anchoredPosition, targetTransform2, Time.deltaTime * smooth);
        //    if (b[tempnumj].GetComponent<RectTransform>().anchoredPosition.y == targetTransform2.y)
        //    {
        //        moving2 = false;
        //    }
        //}
    }

    public void Begin()
    {
        try
        {
            moving = false;
            StopCoroutine(co);
            StopCoroutine(sho);
            StopCoroutine(lo);
            updatePos();
        }
        catch { }
        Step.text = "Welcome!  This interactive minigame is designed to teach you how to perform Merge Sort.";
        for (int i = 0; i < 9; i++)
        {
            int n = UnityEngine.Random.Range(1, 99);
            Text t = b[i].GetComponentInChildren<Text>();
            t.text = n.ToString();

            bclone[i].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
        }
        updatePos();
        sorted = false;
        co = StartCoroutine(Mergechecksort());
    }


    public void updatePos()
    {
        for (int i = 0; i < 9; i++)
        {
            b[i].GetComponent<RectTransform>().anchoredPosition = pos[i].GetComponent<RectTransform>().anchoredPosition;
            bclone[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(pos[i].GetComponent<RectTransform>().anchoredPosition.x, pos[i].GetComponent<RectTransform>().anchoredPosition.y + 50);
            b[i].transform.position = pos[i].transform.position;
            //bclone[i].transform.position = pos[i].transform.position;
            b[i].transform.rotation = pos[i].transform.rotation;
            bclone[i].transform.rotation = pos[i].transform.rotation;
            b[i].GetComponent<MoveInteractionBLock>().PairedPos = pos[i];
        }
    }

    public IEnumerator SwapAnimation(int i, int j)
    {
        tempnumj = j;
        tempnumi = i;
        float tran1;

        //move blocks down
        if (Larray)
        {
            tran1 = L[j].GetComponent<RectTransform>().anchoredPosition.y + 50f;
            targetTransform2 = new Vector3(L[j].GetComponent<RectTransform>().anchoredPosition.x, tran1);
        }
        else 
        {
            tran1 = R[j].GetComponent<RectTransform>().anchoredPosition.y + 50f;
            targetTransform2 = new Vector3(R[j].GetComponent<RectTransform>().anchoredPosition.x, tran1);
        }
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        //move blocks into hover position over their traded block
        if (Larray)
        {
            GameObject p = b[i].GetComponent<MoveInteractionBLock>().PairedPos;
            tran1 = p.GetComponent<RectTransform>().anchoredPosition.x;
            targetTransform2 = new Vector3(tran1, L[j].GetComponent<RectTransform>().anchoredPosition.y);
        }
        else
        {
            GameObject p = b[i].GetComponent<MoveInteractionBLock>().PairedPos;
            tran1 = p.GetComponent<RectTransform>().anchoredPosition.x;
            targetTransform2 = new Vector3(tran1, R[j].GetComponent<RectTransform>().anchoredPosition.y);
        }
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        if (Larray)
        {
            GameObject p = b[i].GetComponent<MoveInteractionBLock>().PairedPos;
            targetTransform2 = new Vector3(L[j].GetComponent<RectTransform>().anchoredPosition.x, p.GetComponent<RectTransform>().anchoredPosition.y);
        }
        else
        {
            GameObject p = b[i].GetComponent<MoveInteractionBLock>().PairedPos;
            targetTransform2 = new Vector3(R[j].GetComponent<RectTransform>().anchoredPosition.x, p.GetComponent<RectTransform>().anchoredPosition.y);
        }
        moving = true;
        while (moving == true)
        {
            yield return null;
        }

        updatePos();
    }
    /*
    public IEnumerator Fallin()
    {
        float p = pos[0].GetComponent<RectTransform>().anchoredPosition.y;
        for (int i = 0; i < 9; i++)
        {
            tempnumj = i;
            float s = b[i].GetComponent<RectTransform>().anchoredPosition.y;
            if (s != p)
            {
                targetTransform2 = new Vector3(b[i].GetComponent<RectTransform>().anchoredPosition.x, p);
                moving2 = true;
                while (moving2 == true)
                {
                    yield return null;
                }
            }
        }
    }
    */

    public IEnumerator Mergechecksort()
    {
        Step.text = "First merge sort will divide the array into multiple parts." + "\nFirst we will work on the first 2 elements.  Slowly progressign through the first half of the array.";
        yield return new WaitForSeconds(speed);
        Step.text = "Next we will find the middle point and right most idnex.  This wills erve as the second half of our array." + "\nWe will use the same steps as before and slowly swap elements that are out of place";
        yield return new WaitForSeconds(speed);
        Step.text = "Finally we will finish by merging both the left and right halves of the array and sorting them." + "\nLets see how this works";
        yield return new WaitForSeconds(speed);

        yield return lo = StartCoroutine(mergeSort(0, b.Count - 1));
        updatePos();
        sorted = true;
        EnableTrigger(0, 9);
    }

    public IEnumerator mergeSort(int l, int r)
    {
        if (l < r)
        {
            int m = l + (r - l) / 2;

            EnableTrigger(-1, -1);
            for (int i = 0; i < bclone.Count; i++)//disable all bclones since they are not needed here
            {
                bclone[i].SetActive(false);
                bclone[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (i >= l && i <= m)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Goodmat;
                }
                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
            yield return new WaitForSeconds(2);
            yield return mergeSort(l, m);

            EnableTrigger(-1, -1);
            for (int i = 0; i < bclone.Count; i++)//disable all bclones since they are not needed here
            {
                bclone[i].SetActive(false);
                bclone[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (i >= m+1 && i <= r)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Goodmat;
                }
                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
            yield return new WaitForSeconds(2);
            yield return mergeSort(m + 1, r);

            EnableTrigger(-1, -1);
            for (int i = 0; i < bclone.Count; i++)//disable all bclones since they are not needed here
            {
                bclone[i].SetActive(false);
                bclone[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            for (int i = 0; i < b.Count; i++)
            {
                if (i >= l && i <= r)
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Goodmat;
                }
                else
                {
                    b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
                }
            }
            yield return new WaitForSeconds(2);
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
            L[i] = bclone[l + i];
            bclone[l + i].SetActive(true);
            bclone[l + i].GetComponentInChildren<MeshRenderer>().material = Lmat;
        }
        for (j = 0; j < n2; j++)
        {
            R[j] = bclone[m + 1 + j];
            bclone[m + 1 + j].SetActive(true);
            bclone[m + 1 + j].GetComponentInChildren<MeshRenderer>().material = Rmat;
        }
        updatePos();
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
                //L[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                yield return  sho = StartCoroutine(SwapAnimation(k, i));
                b[k].GetComponentInChildren<Text>().text = L[i].GetComponentInChildren<Text>().text;
                b[k].GetComponentInChildren<MeshRenderer>().material = Goodmat;
                //b[k] = L[i];
                i++;
            }
            else
            {
                Larray = false;
                //R[j].GetComponentInChildren<MeshRenderer>().material = Nextsort;
                yield return sho = StartCoroutine(SwapAnimation(k, j));
                b[k].GetComponentInChildren<Text>().text = R[j].GetComponentInChildren<Text>().text;
                b[k].GetComponentInChildren<MeshRenderer>().material = Goodmat;

                //b[k] = R[j];
                j++;
            }
            k++;
        }
        //EnableTrigger(l, r);
        while (i < n1)
        {
            Larray = true;
            //L[i].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            yield return sho = StartCoroutine(SwapAnimation(k, i));
            b[k].GetComponentInChildren<Text>().text = L[i].GetComponentInChildren<Text>().text;
            b[k].GetComponentInChildren<MeshRenderer>().material = Goodmat;
            //b[k] = L[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            Larray = false;
            //R[j].GetComponentInChildren<MeshRenderer>().material = Nextsort;
            yield return sho = StartCoroutine(SwapAnimation(k, j));
            b[k].GetComponentInChildren<Text>().text = R[j].GetComponentInChildren<Text>().text;
            b[k].GetComponentInChildren<MeshRenderer>().material = Goodmat;
            //b[k] = R[j];
            j++;
            k++;
        }
        //yield return sho = StartCoroutine(Fallin());
        updatePos();
        yield return new WaitForSeconds(speed);
        for (int x = 0; x < bclone.Count; x++)
        {
            //b[x].GetComponentInChildren<Text>().text = bclone[x].GetComponentInChildren<Text>().text;
            bclone[x].GetComponentInChildren<Text>().text = b[x].GetComponentInChildren<Text>().text;
            bclone[x].SetActive(false);
        }
        updatePos();
    }

    public void EnableTrigger(int l, int h)
    {
        //Debug.Log(l + " " + h);
        for (int i = 0; i < b.Count; i++)
        {
            if (i >= l && i <= h)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Donesort;
            }
            else if (l == -1 && h == -1)
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
            else
            {
                b[i].GetComponentInChildren<MeshRenderer>().material = Unsorted;
            }
        }
    }
}
