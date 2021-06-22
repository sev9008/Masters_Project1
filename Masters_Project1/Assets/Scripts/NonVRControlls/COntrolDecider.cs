using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using Valve.VR;

public class COntrolDecider : MonoBehaviour
{

    public GameObject VrControls;
    public GameObject NonVrControls;

    private bool FoundHMD = false;

    private void Start()
    {



        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
            FoundHMD = true;
        }
        Debug.Log("done");


        //XRGeneralSettings.Instance.Manager.loaders.Clear();

        if (FoundHMD)
        {
            VrControls.SetActive(true);
        }
        else 
        {
            NonVrControls.SetActive(true);
        }
    }
}
