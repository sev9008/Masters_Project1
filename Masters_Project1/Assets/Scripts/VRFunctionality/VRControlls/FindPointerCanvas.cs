using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FindPointerCanvas : MonoBehaviour
{
    public GameObject MyCamera;
    public Canvas MyCanvas1;

    // Start is called before the first frame update
    void Start()
    {
        MyCamera = GameObject.FindWithTag("Pointer");
        MyCanvas1 = GetComponent<Canvas>();

        MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
    }
}
