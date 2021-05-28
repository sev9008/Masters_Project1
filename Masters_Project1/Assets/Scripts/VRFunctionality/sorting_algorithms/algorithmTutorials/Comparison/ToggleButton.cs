using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IPointerDownHandler
{
    public Toggle toggle;
    public Toggle toggle2;
    public Toggle toggle3;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (toggle.isOn == true)
        {
            toggle.isOn = false;
        }        
        else
        {
            toggle.isOn = true;
            toggle2.isOn = false;
            toggle3.isOn = false;
        }
    }
}
