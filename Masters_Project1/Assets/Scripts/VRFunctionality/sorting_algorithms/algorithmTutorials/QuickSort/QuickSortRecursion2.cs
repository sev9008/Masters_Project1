using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSortRecursion2 : MonoBehaviour
{
    public GameObject[] b;
    public GameObject[] bstep;
    public float speed;
    public int currentIndex;
    public int MaxIndex;
    public Text tmptxt;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    Coroutine SHO;
    public Slider slider;
    public bool running;
    public bool manual;
    public bool next;
    public bool previous;
    public bool paused;
    public List<MyStruct> structarr;

    [Serializable]
    public class MyStruct
    {
        //public int currentstep;
        public GameObject Activeimage;
    }
    private void Start()
    {
        manual = false;
        currentIndex = 0;
        MaxIndex = 0;
        //Begin();
    }

    void Update()
    {
        //speed = slider.value;
    }

    public void Begin()
    {
        StopAllCoroutines();

        for (int i = 0; i < bstep.Length; i++)
        {
            bstep[i].SetActive(false);
        }
      
        tmptxt.text = "Start \n";
        StartCoroutine(Step());
    }

    public IEnumerator Step()
    {
        currentIndex = 0;
        MaxIndex = bstep.Length;
        running = true;
        int temp = 0;
        for (int i = currentIndex; i < currentIndex+8; i++)//27,21,15,18,30,45,32
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex + 6; i++)//27,21,15,18,30
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex + 6; i++)//15,18,27,21,30
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex+3; i++)//15,18
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex+4; i++)//27,21,30
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex+4; i++)//21,27,30
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex + 3; i++)//45,32
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex + 3; i++)//32,45
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }

        for (int i = currentIndex; i < currentIndex + 8; i++)//15,18,21,27,30,32,45
        {
            bstep[i].SetActive(true);
            temp++;
        }
        currentIndex = temp;
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
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (next || previous)
            {
                yield return StartCoroutine(changeStep());
            }
            yield return null;
        }
    }
    public IEnumerator changeStep()
    {
        resume:
        if (previous && currentIndex > 0)
        {
            currentIndex--;
            previous = false;
        }
        else if ((next && currentIndex != MaxIndex) || currentIndex != MaxIndex)
        {
            currentIndex++;
            next = false;
        }
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);

        structarr[currentIndex].Activeimage.SetActive(true);

        for (int i = 0; i < currentIndex; i++)
        {
            bstep[i].SetActive(true);
            currentIndex++;
        }
        if (manual)
        {
            paused = true;
        }
        while (paused && manual)
        {
            if (next && currentIndex >= MaxIndex)
            {
                next = false;
                paused = false;
            }
            else if (previous && currentIndex > 0)
            {
                goto resume;
            }
            else if (next && currentIndex < MaxIndex)
            {
                goto resume;
            }
            yield return null;
        }
        if (!manual)
        {
            yield return new WaitForSeconds(speed);
            if (currentIndex >= MaxIndex)
            {
                paused = false;
            }
            else if (currentIndex >= 0 && currentIndex < MaxIndex)
            {
                goto resume;
            }
        }
    }
}
