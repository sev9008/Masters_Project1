using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InheritanceStart : MonoBehaviour
{
    public InheritanceController inheritanceController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inheritanceController.begin)
        {
            inheritanceController.begin = true;
            txt.text = "Start";
        }

        else if (!inheritanceController.begin)
        {
            inheritanceController.begin = false;
            txt.text = "Stop";
        }
    }
}
