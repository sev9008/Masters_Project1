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

    public GameObject[] image;
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
        public int activeImage;
        public string steptxt;
        public int i_value;
        public int j_value;
        public int key_value;
    }

    public void Update()
    {
        speed = slider.value;
    }

    public void Display(List<int> arr2, int size, int i, int j)//need to edit
    {
        iholder.SetActive(true);
        jholder.SetActive(true);
        jnextholder.SetActive(true);
        Txt_Text.text = "";
        for (int n = 0; n < size; n++)
        {
            b[n].SetActive(true);
            if (n == i)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = imat;
                iholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
            }
            else if (n == j)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = jmat;
                jholder.GetComponent<RectTransform>().anchoredPosition = new Vector2(b[n].GetComponent<RectTransform>().anchoredPosition.x, b[n].GetComponent<RectTransform>().anchoredPosition.y + 12);
            }             
            else if (n == j+1 && j != -1)
            {
                b[n].GetComponentInChildren<Text>().text = arr2[n].ToString();
                b[n].GetComponentInChildren<MeshRenderer>().material = jnextmat;
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
        running = true;
        for (int n = 0; n < structarr.Count; n++)//reset all the data in the struct
        {
            structarr[n].oldarr.Clear();
            structarr[n].activeImage = -1;
            structarr[n].steptxt = "";
            structarr[n].i_value = -20;
            structarr[n].j_value = -20;
            structarr[n].key_value = -20;
        }
        for (int k = 0; k < b.Length; k++)//set all blocks to false
        {
            b[k].SetActive(false);
        }
        structarr.Clear();//seems redundant.  dont actually know if the above is needed
        structarr = new List<MyStruct>();
        currentstrucstep = -1;
        maxstrucstep = -1;
        m_selectionAni.ShowGraph(arr3);//display the current array on our graph
        Display(arr3, arr3.Count, -20, 100);//activate the blocks and set their mats
        if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
        {
            yield return new WaitForSeconds(speed);
        }
        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
            {
                next = false;
                paused = false;
            }
            else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        int j, key;
        for (int i = 1; i < arr3.Count; ++i)
        {
            key = arr3[i];
            j = i - 1;

            Step.text = "Set key to arr[i], and set j to i-1";
            i_Text.text = "i = " + i.ToString();
            j_Text.text = "j = " + j.ToString();
            key_Text.text = key.ToString();
            imageController(0);
            Display(arr3, arr3.Count, i, j);
            m_selectionAni.ShowGraph(arr3);

            currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
            maxstrucstep++;
            var a = new MyStruct();
            structarr.Add(a);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].j_value = j;
            structarr[currentstrucstep].i_value = i;
            structarr[currentstrucstep].key_value = key;
            structarr[currentstrucstep].activeImage = 0;
            if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
                {
                    next = false;
                    paused = false;
                }
                else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
                {
                    yield return StartCoroutine(changeStep());
                }
                yield return null;
            }

            while (j >= 0 && arr3[j] > key)
            {
                arr3[j + 1] = arr3[j];
                j--;

                Step.text = "While j>=0 and arr[j] > key set arr[j+1] to arr[j] and decrement j";
                i_Text.text = "i = " + i.ToString();
                j_Text.text = "j = " + j.ToString();
                key_Text.text = key.ToString();
                imageController(1);
                Display(arr3, arr3.Count, i, j);
                m_selectionAni.ShowGraph(arr3);

                currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
                maxstrucstep++;
                var b = new MyStruct();
                structarr.Add(b);
                structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
                for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
                {
                    structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
                }
                structarr[currentstrucstep].steptxt = Step.text;
                structarr[currentstrucstep].j_value = j;
                structarr[currentstrucstep].i_value = i;
                structarr[currentstrucstep].key_value = key;
                structarr[currentstrucstep].activeImage = 1;
                if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
                {
                    yield return new WaitForSeconds(speed);
                }
                if (manual)
                {
                    paused = true;
                }
                while (paused && manual)
                {
                    if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
                    {
                        next = false;
                        paused = false;
                    }
                    else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
                    {
                        yield return StartCoroutine(changeStep());
                    }
                    yield return null;
                }
            }
            arr3[j + 1] = key;

            Step.text = "Set arr[j+1] to key";
            i_Text.text = "i = " + i.ToString();
            j_Text.text = "j = " + j.ToString();
            key_Text.text = key.ToString();
            imageController(2);
            Display(arr3, arr3.Count, i, j);
            m_selectionAni.ShowGraph(arr3);
            currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
            maxstrucstep++;
            var c = new MyStruct();
            structarr.Add(c);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].j_value = j;
            structarr[currentstrucstep].i_value = i;
            structarr[currentstrucstep].key_value = key;
            structarr[currentstrucstep].activeImage = 2;
            if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
                {
                    next = false;
                    paused = false;
                }
                else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
                {
                    yield return StartCoroutine(changeStep());
                }
                yield return null;
            }

            Step.text = "Increment i and repeat the above steps";
            i_Text.text = "i = " + i.ToString();
            j_Text.text = "j = " + j.ToString();
            key_Text.text = key.ToString();
            imageController(-1);
            Display(arr3, arr3.Count, i, j);
            m_selectionAni.ShowGraph(arr3);
            currentstrucstep++;//this will add the current step to the array.  its a wall of code but you will see it alot
            maxstrucstep++;
            var d = new MyStruct();
            structarr.Add(d);
            structarr[currentstrucstep].oldarr = new List<int>(arr3.Count);
            for (int tempnum = 0; tempnum < arr3.Count; tempnum++)
            {
                structarr[currentstrucstep].oldarr.Add(arr3[tempnum]);
            }
            structarr[currentstrucstep].steptxt = Step.text;
            structarr[currentstrucstep].j_value = j;
            structarr[currentstrucstep].i_value = i;
            structarr[currentstrucstep].key_value = key;
            structarr[currentstrucstep].activeImage = -1;
            if (!manual)//you will see these next three case statements alot.  thses control whether the animation is paused or unpaused.  then you can resume, press next, or press previous
            {
                yield return new WaitForSeconds(speed);
            }
            if (manual)
            {
                paused = true;
            }
            while (paused && manual)
            {
                if (next && currentstrucstep >= maxstrucstep)//if the current step is the the last step we ahve saved in ours truct then just unpause and proceed
                {
                    next = false;
                    paused = false;
                }
                else if (next || previous)//if sturct arr has moves stored and we are not on the last step in out sturct, then transition to change step.
                {
                    yield return StartCoroutine(changeStep());
                }
                yield return null;
            }
        }

        imageController(-1);
        Step.text = "Finished";
        Display(arr3, arr3.Count, -1,-1);
        m_selectionAni.ShowGraph(arr3);
        for (int n = 0; n < arr3.Count; n++)
        {
            b[n].GetComponentInChildren<MeshRenderer>().material = jnextmat;
        }
        running = false;
    }

    public IEnumerator changeStep()//if next or previous is pressed then this algorithm takes over.  it will display the current step and attempt to continue the animation 
    {
        resume:
        if (previous && currentstrucstep > 0)
        {
            currentstrucstep--;
            previous = false;
        }
        else if (next && currentstrucstep != maxstrucstep || currentstrucstep != maxstrucstep)
        {
            currentstrucstep++;
            next = false;
        }

        Step.text = structarr[currentstrucstep].steptxt;
        i_Text.text = structarr[currentstrucstep].i_value.ToString();
        j_Text.text = structarr[currentstrucstep].j_value.ToString();
        key_Text.text = structarr[currentstrucstep].key_value.ToString();
        imageController(structarr[currentstrucstep].activeImage);
        Display(structarr[currentstrucstep].oldarr, structarr[currentstrucstep].oldarr.Count, structarr[currentstrucstep].i_value, structarr[currentstrucstep].j_value);
        m_selectionAni.ShowGraph(structarr[currentstrucstep].oldarr);

        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentstrucstep >= maxstrucstep)
            {
                next = false;
                paused = false;
            }
            else if (previous && currentstrucstep > 0)
            {
                goto resume;
            }
            else if (next && currentstrucstep < maxstrucstep)
            {
                goto resume;
            }
            yield return null;
        }
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
            if (currentstrucstep >= maxstrucstep)
            {
                paused = false;
            }
            else if (currentstrucstep >= 0 && currentstrucstep < maxstrucstep)
            {
                goto resume;
            }
        }
    }

    public void imageController(int k)//control the active image
    {
        for (int i = 0; i < image.Length; i++)
        {
            if (i == k)
            {
                image[i].SetActive(true);
            }
            else
            {
                image[i].SetActive(false);
            }
        }
    }

}
