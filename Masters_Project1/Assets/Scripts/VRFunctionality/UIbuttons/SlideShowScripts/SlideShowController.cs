using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShowController : MonoBehaviour
{
    public int currslide;
    public GameObject[] selectorArr;

    void Start()
    {
        currslide = 0;
        UpdateSlides();
    }

    public void NextSlide()
    {
        if (currslide == selectorArr.Length-1)
        {
            currslide = 0;
        }
        else
        {
            currslide++;
        }
        UpdateSlides();
    }    
    
    public void PrevSlide()
    {
        if (currslide == 0)
        {
            currslide = selectorArr.Length-1;
        }
        else
        {
            currslide--;
        }
        UpdateSlides();
    }

    public void UpdateSlides()
    {
        for (int i = 0; i < selectorArr.Length; i++)
        {
            if (i == currslide)
            {
                selectorArr[i].SetActive(true);
            }
            else 
            {
                selectorArr[i].SetActive(false);
            }
        }
    }
}
