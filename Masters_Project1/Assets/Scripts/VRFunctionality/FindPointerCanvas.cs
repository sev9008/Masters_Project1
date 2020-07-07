using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPointerCanvas : MonoBehaviour
{
    public GameObject MyCamera;
    public Canvas MyCanvas;
    // Start is called before the first frame update
    void Start()
    {
        MyCamera = GameObject.FindWithTag("Pointer");
        MyCanvas = GetComponent<Canvas>();

        MyCanvas.worldCamera = MyCamera.GetComponent<Camera>();
    }
}
