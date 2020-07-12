using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class selsort_arrayholder : MonoBehaviour
{
    public List<int> arr;
    public Text Txt_Text;
    public int size;
    public float speed;
    public float waittime;
    public Text Step;

    public void Display()
    {
        Txt_Text.text = "";
        int Tmpsize = size - 1;
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                Txt_Text.text += "a[" + Tmpsize + "] = " + arr[i].ToString();
            }
            else if (i > 0)
            {
                Txt_Text.text += ", " + arr[i].ToString();
            }
        }
    }

    public IEnumerator Selection(List<int> arr)
    {
        int i, j;
        int iMin;
        for (j = 0; j < size - 1; j++)
        {
            iMin = j;
            for (i = j + 1; i < size; i++)
            {
                if (arr[i] < arr[iMin])
                {
                    iMin = i;
                }
            }
            if (iMin != j)
            {
                int temp = arr[j];
                arr[j] = arr[iMin];
                arr[iMin] = temp;
                Debug.Log("run");
                Step.text = "Swap " + arr[j] + " and " + arr[iMin];
                Display();
                yield return new WaitForSeconds(2);
            }
        }
        Step.text = "Finished";
        Display();
    }
}
