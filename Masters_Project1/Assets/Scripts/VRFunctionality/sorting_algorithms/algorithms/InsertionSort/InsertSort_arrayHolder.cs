using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertSort_arrayHolder : MonoBehaviour
{
    public List<int> arr;
    public Text Txt_Text;
    public int size;
    public float waittime;
    public Text Step;
    public float speed;

    public Slider slider;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public bool paused;

    private void Start()
    {
        paused = false;
    }

    public void Update()
    {
        speed = slider.value;
    }

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

    public IEnumerator Insertion(List<int> arr)
    {
        int n = size;
        int j, key;
        for (int i = 1; i < n; ++i)
        {
            key = arr[i];
            j = i - 1;

            Step.text = "Save " + arr[i] + " as a key";
            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);

            while (j >= 0 && arr[j] > key)
            {
                Step.text = "if " + arr[j] + " > " + key + " \nThen set " + arr[j + 1] + " to " + arr[j];

                arr[j + 1] = arr[j];
                j--;

                image1.SetActive(false);
                image2.SetActive(true);
                image3.SetActive(false);
                Display();
                while (paused)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(speed);
            }

            Step.text = "set " + arr[j+1] + " to " + key;
            arr[j + 1] = key;
            Display();
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
            while (paused)
            {
                yield return null;
            }
            yield return new WaitForSeconds(speed);
        }
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        Step.text = "Finished";

    }
}
