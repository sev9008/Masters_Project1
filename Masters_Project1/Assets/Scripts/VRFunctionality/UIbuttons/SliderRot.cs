using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderRot : MonoBehaviour
{
    public Slider slider;

    public GameObject canvas;

    void Update()
    {
        canvas.transform.localEulerAngles = new Vector3(0, slider.value, 0);
    }
}
