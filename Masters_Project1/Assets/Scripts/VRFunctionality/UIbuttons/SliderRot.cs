using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This script will allow UI to rotate with a sldier that t was attached to.
/// It is no longer in use.
/// </summary>
public class SliderRot : MonoBehaviour
{
    public Slider slider;

    public GameObject canvas;

    void Update()
    {
        canvas.transform.localEulerAngles = new Vector3(0, slider.value, 0);
    }
}
