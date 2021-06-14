using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActiveController : MonoBehaviour
{
    public bool algTut;
    public GameObject Algpanel;

    public bool compTut;
    public GameObject compPanel;

    // Update is called once per frame
    void Update()
    {
        if (algTut)
        {
            Algpanel.SetActive(true);
        }
        else {
            Algpanel.SetActive(false);
        }

        if (compTut)
        {
            compPanel.SetActive(true);
        }
        else
        {
            compPanel.SetActive(false);
        }
    }
}
