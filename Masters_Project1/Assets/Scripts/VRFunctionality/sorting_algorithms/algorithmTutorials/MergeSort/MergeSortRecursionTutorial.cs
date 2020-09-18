using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeSortRecursionTutorial : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] bstep;
    public int speed;
    public int currentIndex;
    public Text tmptxt;
    public Material Lmat;
    public Material Rmat;
    public Material Normalmat;

    public void Start()
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
        tmptxt.text += "Start \n";

        StartCoroutine(Mergechecksort());
    }

    public IEnumerator Mergechecksort()
    {
        yield return StartCoroutine(mergeSort(0, b.Length - 1));
        tmptxt.text = "Merge Sort is finished.";
    }

    public IEnumerator mergeSort(int l, int r)
    {
        int tempsize = 0;
        if (l < r)
        {
            yield return new WaitForSeconds(speed);
            int m = l + (r - l) / 2;
            tempsize = m - l;
            for (int i = l; i < l+tempsize+1; i++)
            {
                bstep[currentIndex].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
                bstep[currentIndex].GetComponentInChildren<MeshRenderer>().material = Lmat;
                bstep[currentIndex].SetActive(true);
                currentIndex++;
            }
            tmptxt.text = "Perform MergeSort on the left side of the array.\n" + "Left index = " + l + "\nRight index = " + m;
            yield return mergeSort(l, m);
            yield return new WaitForSeconds(speed);

            tempsize = r - m;
            for (int i = m+1; i < m+1+tempsize; i++)
            {
                bstep[currentIndex].GetComponentInChildren<Text>().text = b[i].GetComponentInChildren<Text>().text;
                bstep[currentIndex].GetComponentInChildren<MeshRenderer>().material = Rmat;
                bstep[currentIndex].SetActive(true);
                currentIndex++;
            }
            tmptxt.text = "Perform MergeSort on the right side of the array.\n" + "Left index = " + m+1 + "\nRight index = " + r;
            yield return mergeSort(m + 1, r);
            yield return new WaitForSeconds(speed);

            tmptxt.text = "Perform Merge on ";
            for (int i = l; i < r+1; i++)
            {
                tmptxt.text += b[i].GetComponentInChildren<Text>().text + " ";
            }
            tmptxt.text += "\nLeft index = " + l + "\nMiddle index = " + m + "\nRight index = " + r;
            yield return merge(l, m, r);
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator merge(int l, int m, int r)
    {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 = r - m;
        GameObject[] L = new GameObject[n1];
        GameObject[] R = new GameObject[n2];

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

        while (i < n1 && j < n2)
        {
            float.TryParse(L[i].GetComponentInChildren<Text>().text, out float temp);
            float.TryParse(R[j].GetComponentInChildren<Text>().text, out float temp2);

            if (temp <= temp2)
            {
                b[k] = L[i];
                i++;
            }
            else
            {
                b[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            b[k] = L[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            b[k] = R[j];
            j++;
            k++;

        }
        for (int n = l; n < k; n++)
        {
            bstep[currentIndex].GetComponentInChildren<Text>().text = b[n].GetComponentInChildren<Text>().text;
            bstep[currentIndex].GetComponentInChildren<MeshRenderer>().material = Normalmat;
            bstep[currentIndex].SetActive(true);
            currentIndex++;
        }
        yield return null;
    }
}
