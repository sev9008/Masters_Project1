using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InheritancePause : MonoBehaviour
{
    public InheritanceController inheritanceController;
    public Text txt;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (inheritanceController.manual)
        {
            inheritanceController.manual = true;
            txt.text = "Resume";
        }

        else if (!inheritanceController.manual)
        {
            inheritanceController.manual = false;
            txt.text = "Pause";
        }
    }
}
