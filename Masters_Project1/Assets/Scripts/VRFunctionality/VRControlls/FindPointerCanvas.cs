﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FindPointerCanvas : MonoBehaviour
{
    public GameObject MyCamera;
    public Canvas MyCanvas1;
    public SteamVR_Action_Boolean GrabObj = null;

    // Start is called before the first frame update
    void Start()
    {
        MyCamera = GameObject.FindWithTag("PointerR");
        MyCanvas1 = GetComponent<Canvas>();

        MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
    }

    private void Update()
    {
        if (GrabObj.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            MyCamera = GameObject.FindWithTag("PointerR");
            MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
        }
        if (GrabObj.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            MyCamera = GameObject.FindWithTag("PointerL");
            MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
        }
    }
}
