using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertSort_arrayHolder : MonoBehaviour
{
    public GameObject[] b;

    public Text i_Text;
    public Text j_Text;
    public Text j2_Text;
    public Text key_Text;
    public Text Txt_Text;
    public float waittime;
    public Text Step;
    public float speed;

    public Slider slider;

    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public bool paused;
    public bool running;

    public bool previous;
    public bool next;

    public SelectionAni m_selectionAni;

    public List<MyStruct> structarr;

    public List<int> arr3;

    public int currentstrucstep;
    public int maxstrucstep;

    public Material imat;
    public Material jmat;
    public Material jnextmat;
    public Material Normalmat;

    public GameObject iholder;
    public GameObject jholder;
    public GameObject jnextholder;

    public bool manual;


    private void Start()
    {
        paused = false;
        structarr = new List<MyStruct>();
    }
    [Serializable] public class MyStruct
    {
        public List<int> oldarr;
        public int i_value;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size, int i, int j)//need to edit
    {
        iholder.SetActive(false);
        jholder.SetActive(false);
        jnextholder.SetActive(false);
        Txt_Text.text = "";
        for (int n = 0; n < size; n++)
        {
            b[n].SetActive(true);
            if (n == i)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = imat;
                iholder.SetActive(true);
                iholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
            }
            else if (n == j)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
                jholder.SetActive(true);
                jholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
            }             
            else if (n == j+1 && j != -1)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = jnextmat;
                jnextholder.SetActive(true);
                jnextholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
            }            
            else
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = Normalmat;
            }
        }
    }

    public IEnumerator Insertion()
    {
        if (manual)
        {
            paused = true;
        }
        for (int k = 0; k < b.Length; k++)
        {
            b[k].SetActive(false);
        }

        for (int n = 0; n < structarr.Count; n++)
        {
            structarr[n].oldarr.Clear();
            structarr[n].i_value = -1;
        }

        structarr.Clear();
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;
        running = true;
        m_selectionAni.ShowGraph(arr3);

        //int curSize = structarr.Count;
        int j, key;
        for (int i = 1; i < arr3.Count; ++i)
        {
            i_Text.text = "i = " + i.ToString();
            Display(arr3, arr3.Count, i, -1);
            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (currentstrucstep == maxstrucstep)
            {
                currentstrucstep++;
                maxstrucstep++;
                Debug.Log("addnew");
                var c = new MyStruct();
                structarr.Add(c);
                structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
                }
                structarr[currentstrucstep].i_value = i;
            }
            resume:

            if (previous && next)
            {
                previous = false;
                next = false;
            }
            if (previous && currentstrucstep > 0)
            {
                currentstrucstep--;
                PressedPrevious();
                previous = false;
                i = structarr[currentstrucstep].i_value;
            }
            else if (previous && currentstrucstep <= 0)
            {
                Step.text = "Cant go back any further";
                if (!manual)
                {
                    yield return new WaitForSeconds(speed);
                }
            }

            if (next && currentstrucstep < maxstrucstep)
            {
                currentstrucstep++;
                PressedNext();
                next = false;
                i = structarr[currentstrucstep].i_value;
            }
            else if (next && currentstrucstep >= maxstrucstep)
            {
                Step.text = "Cant go forward any further";
                if (!manual)
                {
                    yield return new WaitForSeconds(speed);
                }
            }
            previous = false;
            next = false;
            m_selectionAni.ShowGraph(arr3);
            Display(arr3, arr3.Count, i, -1);

            key = arr3[i];
            j = i - 1;

            j_Text.text = "j = " + j.ToString();
            Step.text = "Save " + arr3[i] + " as a key";
            key_Text.text = "key = " + arr3[i].ToString();

            image1.SetActive(true);
            image2.SetActive(false);
            image3.SetActive(false);
            Display(arr3, arr3.Count, i, j);

            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (previous && currentstrucstep > 0)
                {
                    goto resume;
                }
                if (next && currentstrucstep < maxstrucstep)
                {
                    goto resume;
                }
                else if (next && currentstrucstep >= maxstrucstep)
                {
                    next = false;
                    paused = false;
                }
                yield return null;
            }

            while (j >= 0 && arr3[j] > key)
            {
                j_Text.text = "j = " + j.ToString();
                Step.text = "if " + arr3[j] + " > " + key + " \nThen set " + arr3[j + 1] + " to " + arr3[j];

                arr3[j + 1] = arr3[j];
                j--;
                j_Text.text = "j = " + j.ToString();

                image1.SetActive(false);
                image2.SetActive(true);
                image3.SetActive(false);
                Display(arr3, arr3.Count, i, j);

                yield return new WaitForSeconds(speed);
                if (manual)
                {
                    //Step.text = "Please wait...";
                    paused = true;
                }                
                if (!manual)
                {
                    //Step.text = "Please wait...";
                    paused = false;
                }
                if (previous)
                {
                    Step.text = "Please wait...";
                    previous = true;
                }
                if (next)
                {
                    Step.text = "Please wait...";
                    next = true;
                }
            }

            Step.text = "set " + arr3[j+1] + " to " + key;
            arr3[j + 1] = key;
            Display(arr3, arr3.Count, i, j);//fix
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(true);
            if (!manual)
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (previous && currentstrucstep > 0)
                {
                    goto resume;
                }
                if (next && currentstrucstep < maxstrucstep)
                {
                    goto resume;
                }
                else if (next && currentstrucstep >= maxstrucstep)
                {
                    next = false;
                    paused = false;
                }
                yield return null;
            }
        }
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        Step.text = "Finished";
        Display(arr3, arr3.Count, -1,-1);//fix
        m_selectionAni.ShowGraph(arr3);
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
        }
        running = false;
    }
    public void PressedPrevious()
    {
        arr3.Clear();
        for (int i = 0; i < structarr[currentstrucstep].oldarr.Count; i++)
        {
            arr3.Add(structarr[currentstrucstep].oldarr[i]);
        }
    }

    public void PressedNext()
    {
        arr3.Clear();
        for (int i = 0; i < structarr[currentstrucstep].oldarr.Count; i++)
        {
            arr3.Add(structarr[currentstrucstep].oldarr[i]);
        }
    }
    /*
    void insertionSort(int arr[], int n)
    {
        int i, key, j;
        for (i = 1; i < n; i++)
        {
            key = arr[i];
            j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }
    */
}
