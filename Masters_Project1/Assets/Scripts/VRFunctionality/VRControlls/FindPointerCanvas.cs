using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
/// <summary>
/// This script will get the two event cameras attached to the vr controller.
/// Without event cameras, VR controller can not interact with UI
/// In additon only one event camera can be used at any given time for UI
/// Thus another script is needed to decide when each event camera will be in use
/// </summary>
public class FindPointerCanvas : MonoBehaviour
{
    public GameObject MyCamera;
    public Canvas MyCanvas1;
    public SteamVR_Action_Boolean GrabObj = null;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            MyCamera = GameObject.FindWithTag("PointerR");
            MyCanvas1 = GetComponent<Canvas>();

            MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
        }
        catch { }
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
        //if(Input.GetAxis("Fire1") != 0)
        //{
        //    MyCamera = GameObject.FindWithTag("FPCamera");
        //    MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
        //}
    }
}
