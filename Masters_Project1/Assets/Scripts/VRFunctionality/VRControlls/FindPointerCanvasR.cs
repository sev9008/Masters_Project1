using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPointerCanvasR : MonoBehaviour
{
    public GameObject MyCamera;
    public Canvas MyCanvas1;
    // Start is called before the first frame update
    void Start()
    {
        MyCamera = GameObject.FindWithTag("PointerR");
        MyCanvas1 = GetComponent<Canvas>();

        MyCanvas1.worldCamera = MyCamera.GetComponent<Camera>();
    }
}
