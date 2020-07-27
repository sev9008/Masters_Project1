using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePanel : MonoBehaviour
{
    public GameObject[] arr;

    public int currentlyselected;
    void Start()
    {
        currentlyselected = -1;
        updatepanels();
    }

    void Update()
    {

    }

    public void updatepanels()
    {
        if (currentlyselected == -1)
        {
            foreach (GameObject panel in arr)
            {
                panel.SetActive(false);
            }
        }

        if (currentlyselected != -1)
        {
            int i = 0;
            foreach (GameObject panel in arr)
            {
                panel.SetActive(false);
                i++;
                if (i == currentlyselected)
                {
                    panel.SetActive(true);
                }
            }
        }
    }

}
